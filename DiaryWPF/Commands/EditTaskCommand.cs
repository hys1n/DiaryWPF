using Diary.Commands;
using DiaryWPF.ViewModels;
using System.Windows;

namespace DiaryWPF.Commands
{
    /// <summary>
    /// Command which edits an existing task.
    /// </summary>
    public class EditTaskCommand : CommandBase
    {
        private readonly EditTaskViewModel viewModel;

        public event EventHandler? TaskEditedSuccessfully;

        public EditTaskCommand(EditTaskViewModel viewModel) 
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            if (string.IsNullOrWhiteSpace(viewModel.Task.Title) 
                || viewModel.Task.Title.Length > 32
                || string.IsNullOrWhiteSpace(viewModel.Task.Location) 
                || AddTaskFormViewModel.IsValidStreetName(viewModel.Task.Location))
            {
                MessageBox.Show("Not all input fields are filled or input text is invalid", 
                    "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show(
                                    "Are you sure you want to edit?",
                                    "Confirmation",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Question
                            );
                if (result == MessageBoxResult.Yes)
                {
                    TaskEditedSuccessfully?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
