using System.Windows;

namespace DiaryWPF.Forms
{
    /// <summary>
    /// Interaction logic for RegistrationForm.xaml
    /// </summary>
    public partial class RegistrationForm : Window
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private bool isClicked = false;

        public bool IsClicked
        {
            get { return isClicked; }
            set { isClicked = value; }
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            if (isClicked == false)
            {
                rfName.Content = "Log in";
                btnCreateAccount.Content = "Sign in";
                rfQuestion.Content = "No account yet?";
                btnLogInText.Text = "Sign up";
                isClicked = true;
            }
            else
            {
                rfName.Content = "Create account";
                btnCreateAccount.Content = "Create account";
                rfQuestion.Content = "Have an account?";
                btnLogInText.Text = "Log in";
                isClicked = false;
            }
        }
    }
}
