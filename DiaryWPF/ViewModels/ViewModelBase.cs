using Diary.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Diary.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SaveUsersToJson(ObservableCollection<User> users)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(users, options);
            File.WriteAllText("users.json", json);
        }

        protected void LoadUsersFromJson(ObservableCollection<User> users)
        {
            if (File.Exists("users.json"))
            {
                string json = File.ReadAllText("users.json");
                users = JsonSerializer.Deserialize<ObservableCollection<User>>(json) ?? new ObservableCollection<User>();
            }
            else
            {
                users = new ObservableCollection<User>();
            }
        }
    }
}
