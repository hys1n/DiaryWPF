using Diary.Commands;
using Diary.Forms;
using Diary.Models;
using DiaryWPF.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace Diary.ViewModels
{
    public class RegistrationFormViewModel : ViewModelBase, IDataErrorInfo
    {
        public RegisterUserCommand RegisterUserCommand { get; set; }

        public LogInUserCommand LogInUserCommand { get; set; }

        public string Error { get { return null; } }

        public Dictionary<string, string> ErrorCollection { get; set; } = new Dictionary<string, string>();

        private string userName;

        private string email;

        private string password;

        public RegistrationFormViewModel()
        {
            User tempUser = new User { UserName = "user", Email = "user@gmail.com", Password = "1234" };
            UserManager.AddUser(tempUser);
            UserManager.CurrentUser = tempUser;
            UserManager.CurrentUser.Tasks.Add(new DiaryTask(
                        $"Task_1",
                        new DateTime(2024, 6, 6),
                        new DateTime(2024, 6, 6, 0, 0, 0),
                        new TimeSpan(1, 0, 0),
                        "вул. Шевченка",
                        "",
                        false
                    ));

            RegisterUserCommand = new RegisterUserCommand(this);
            RegisterUserCommand.UserRegisteredSuccessfully += OnUserRegisteredSuccessfully;
            LogInUserCommand = new LogInUserCommand(this);
            LogInUserCommand.UserLoggedInSuccessfully += OnUserLoggedInSuccessfully;
        }

        public string UserName
        {
            get => userName;
            set
            {
                if (userName != value)
                {
                    userName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Email
        {
            get => email;
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged();
                }
            }
        }

        // Indexer which validates input fields
        public string this[string columnName]
        {
            get
            {
                string result = null;

                switch (columnName)
                {
                    case nameof(UserName):
                        if (string.IsNullOrWhiteSpace(UserName))
                            result = "Username cannot be empty";
                        else if (UserName.Length < 3)
                            result = "Username must be a minimum of 3 characters";
                        else if (UserName.Length > 32)
                            result = "Username must be a maximum of 32 characters";
                        break;
                    case nameof(Email):
                        if (string.IsNullOrWhiteSpace(Email))
                            result = "Email cannot be empty";
                        else if (!IsValidEmail(Email))
                            result = "Email is not valid. Email format: 'email_name@gmail.com'";
                        break;
                    case nameof(Password):
                        if (string.IsNullOrWhiteSpace(Password))
                            result = "Password cannot be empty";
                        else if (Password.Length < 4)
                            result = "Password must be a minimum of 4 characters";
                        else if (Password.Length > 64)
                            result = "Password must be a maximum of 64 characters";
                        break;
                }

                if (ErrorCollection.ContainsKey(columnName))
                    ErrorCollection[columnName] = result;
                else if (result != null)
                    ErrorCollection.Add(columnName, result);

                OnPropertyChanged("ErrorCollection");
                return result;
            }
        }

        /// <summary>
        /// Checks if the email is valid.
        /// </summary>
        /// <param name="email">Email to check.</param>
        /// <returns>True if the email is valid; otherwise false.</returns>
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        /// <summary>
        /// Opens the main window after user registered succesfully.
        /// </summary>
        private void OnUserRegisteredSuccessfully(object? sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                Application.Current.Windows
                    .OfType<RegistrationForm>()
                    .FirstOrDefault()?.Close();
            });
        }

        /// <summary>
        /// Opens the main window after user logged in succesfully.
        /// </summary>
        private void OnUserLoggedInSuccessfully(object? sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                Application.Current.Windows
                    .OfType<RegistrationForm>()
                    .FirstOrDefault()?.Close();
            });
        }
    }
}
