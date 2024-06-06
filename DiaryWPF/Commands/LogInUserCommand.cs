using Diary.Models;
using Diary.ViewModels;
using System.Windows;

namespace Diary.Commands
{
    public class LogInUserCommand : CommandBase
    {
        private readonly RegistrationFormViewModel viewModel;

        public event EventHandler? UserLoggedInSuccessfully;

        public LogInUserCommand(RegistrationFormViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            bool userFound = false;

            if (UserManager.GetUsers().Count <= 0)
            {
                MessageBox.Show("The user doesn't exist", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                foreach (User user in UserManager.GetUsers())
                {
                    if (user.Email == viewModel.Email
                        && user.UserName == viewModel.UserName
                        && user.Password == viewModel.Password)
                    {
                        UserManager.CurrentUser = user;
                        MessageBox.Show("You've logged in successfully!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                        UserLoggedInSuccessfully?.Invoke(this, EventArgs.Empty);

                        userFound = true; 
                        break;
                    }
                }

                if (!userFound)
                {
                    MessageBox.Show("The user doesn't exist", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
