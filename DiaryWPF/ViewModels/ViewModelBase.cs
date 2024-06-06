using Diary.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Diary.ViewModels
{
    /// <summary>
    /// Base view model which implements INotifyPropertyChanged and 
    /// saving from and loading to json.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Stores users' data in json file.
        /// </summary>
        /// <param name="users">Users to be stored</param>
        protected void SaveUsersToJson(ObservableCollection<User> users)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(users, options);
            File.WriteAllText("users.json", json);
        }

        /// <summary>
        /// Upload the users' data from json file.
        /// </summary>
        protected void LoadUsersFromJson()
        {
            if (File.Exists("users.json"))
            {
                string json = File.ReadAllText("users.json");
                UserManager.Users = JsonSerializer.Deserialize<ObservableCollection<User>>(json) 
                    ?? new ObservableCollection<User>();
            }
            else
            {
                UserManager.Users = new ObservableCollection<User>();
            }
        }
    }
}
