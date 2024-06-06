using DiaryWPF.Models;
using System.Collections.ObjectModel;

namespace Diary.Models
{
    /// <summary>
    /// Class which stores all the information about 
    /// the user.
    /// </summary>
    public class User
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int Id { get; set; }

        public ObservableCollection<DiaryTask> Tasks { get; set; }

        public User() 
        {
            Tasks = new ObservableCollection<DiaryTask>();
        }
    }
}
