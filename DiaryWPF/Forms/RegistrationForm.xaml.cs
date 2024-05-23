using DiaryWPF.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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

        private bool isClicked = false;

        private string userName;

        private string email;

        private string password;

        public event PropertyChangedEventHandler? PropertyChanged;

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

        public string Error { get { return null; } }

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
                        else if (UserName.Length < 2)
                            result = "Username must be a minimum of 3 characters";
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
                        else if (Password.Length < 2)
                            result = "Password must be a minimum of 3 characters";
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
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            //if (isClicked == false)
            //{
            //    rfName.Content = "Log in";
            //    btnCreateAccount.Content = "Sign in";
            //    rfQuestion.Content = "No account yet?";
            //    btnLogInText.Text = "Sign up";
            //    isClicked = true;
            //}
            //else
            //{
            //    rfName.Content = "Create account";
            //    btnCreateAccount.Content = "Create account";
            //    rfQuestion.Content = "Have an account?";
            //    btnLogInText.Text = "Log in";
            //    isClicked = false;
            //}
        }

        public void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
