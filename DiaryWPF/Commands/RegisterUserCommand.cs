using Diary.Models;
using Diary.ViewModels;
using System.Windows;

namespace Diary.Commands
{
    /// <summary>
    /// Command which registers a user in case there are
    /// no validation errors.
    /// </summary>
    public class RegisterUserCommand : CommandBase
    {
        private readonly RegistrationFormViewModel viewModel;

        public event EventHandler? CanExecuteChanged;

        public event EventHandler? UserRegisteredSuccessfully;

        public RegisterUserCommand(RegistrationFormViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            if (viewModel.ErrorCollection.Count > 0 && viewModel.ErrorCollection.Any(e => e.Value != null))
            {
                MessageBox.Show("Not all input fields are filled or input text is invalid", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            } else if (UserManager.GetUsers().Any(u => u.UserName == viewModel.UserName || u.Email == viewModel.Email))
            {
                MessageBox.Show("User with the same username or email already exists", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                User newUser = new User()
                {
                    UserName = viewModel.UserName,
                    Email = viewModel.Email,
                    Password = viewModel.Password,
                    //Tasks = new ObservableCollection<>()
                };

                UserManager.AddUser(newUser);
                UserManager.CurrentUser = newUser;
                MessageBox.Show("User registered successfully!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                UserRegisteredSuccessfully?.Invoke(this, EventArgs.Empty);
            }
            //UpdateViewData.LoadData();

            
        }
    }
}
