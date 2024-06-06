using System.Collections.ObjectModel;

namespace Diary.Models
{
    /// <summary>
    /// Class which stores all the users and has some utility methods.
    /// </summary>
    public class UserManager
    {
        private static ObservableCollection<User> users { get; set; } = new ObservableCollection<User>() { };

        public static User CurrentUser;

        public static ObservableCollection<User> GetUsers()
        {
            return users;
        }

        public static ObservableCollection<User> Users
        {
            set { users = value; }
        }

        /// <summary>
        /// Mehtod which add a user to a collection of
        /// all users.
        /// </summary>
        /// <param name="user">User to be added.</param>
        public static void AddUser(User user)
        {
            users.Add(user);
        }
    }
}
