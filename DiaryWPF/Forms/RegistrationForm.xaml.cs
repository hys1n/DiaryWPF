using Diary.ViewModels;
using System.Windows;

namespace Diary.Forms
{
    /// <summary>
    /// Interaction logic for RegistrationForm.xaml
    /// </summary>
    public partial class RegistrationForm : Window
    {
        public RegistrationForm()
        {
            InitializeComponent();
            ViewModelBase.LoadUsersFromJson();
            RegistrationFormViewModel registrationFormViewModel = new RegistrationFormViewModel();
            DataContext = registrationFormViewModel;
        }
    }
}
