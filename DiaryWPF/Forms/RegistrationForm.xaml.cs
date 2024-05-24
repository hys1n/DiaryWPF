using DiaryWPF.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;

namespace DiaryWPF.Forms
{
    /// <summary>
    /// Interaction logic for RegistrationForm.xaml
    /// </summary>
    public partial class RegistrationForm : Window, IDataErrorInfo, INotifyPropertyChanged
    {
        public RegistrationForm()
        {
            DataContext = this;
            InitializeComponent();
        }

        private readonly List<User> Users = MainWindow.users;

        private bool isClicked = false;

        private string userName;

        private string email;

        private string password;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Error { get { return null; } }

        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        public bool IsClicked
        {
            get { return isClicked; }
            set { isClicked = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;

                switch(columnName)
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
                            result = "Email cannot be empty.";
                        else if (!IsValidEmail(Email))
                            result = "Email is not valid.";
                        break;
                    case nameof(Password):
                        if (string.IsNullOrWhiteSpace(Password))
                            result = "Password cannot be empty.";
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

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            bool isRegistered = RegisterUser();

            if (isRegistered)
            {
                DialogResult = true;
                Close();
            }
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            bool isLoggedIn = LogInUser();

            if (isLoggedIn)
            {
                DialogResult = true;
                Close();
            }
        }

        private bool RegisterUser()
        {
            if (ErrorCollection.Count > 0 && ErrorCollection.Any(e => e.Value != null))
            {
                MessageBox.Show("Not all input fields are filled or input text is invalid", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (Users.Any(u => u.UserName == UserName || u.Email == Email))
            {
                MessageBox.Show("User with the same username or email already exists", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            User newUser = new User
            {
                UserName = UserName,
                Email = Email,
                Password = Password
            };

            Users.Add(newUser);

            MessageBox.Show("User registered successfully!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            return true; 
        }

        private bool LogInUser()
        {
            if (Users.Count <= 0)
            {
                MessageBox.Show("The user doesn't exist", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Users.Count > 0)
            {
                foreach (User user in Users)
                {
                    if (user.Email == Email && user.UserName == UserName && user.Password == Password)
                    {
                        MessageBox.Show("You've logged in successfully!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                        return true;
                    }
                }
            }

            MessageBox.Show("The user doesn't exist", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            return false; 
        }
    }
}
