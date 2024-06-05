using System.Collections.ObjectModel;
using Diary.Models;
using DiaryWPF.Models;

namespace Diary.Models
{
    /// <summary>
    /// A class which stores all the information about 
    /// the user.
    /// </summary>
    public class User
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int Id { get; set; }

        public ObservableCollection<DiaryTask> Tasks { get; set; }

        public User() { }
    }
}
