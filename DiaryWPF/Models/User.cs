using System.Collections.ObjectModel;

namespace DiaryWPF.Models
{
    public class User
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int Id { get; set; }

        public ObservableCollection<Event> Tasks { get; set; }

        public User() { }

        public User(int id, string userName, string email, string password, ObservableCollection<Event> tasks)
        {
            Id = id;
            UserName = userName;
            Email = email;
            Password = password;
            Tasks = tasks;
        }
    }
}
