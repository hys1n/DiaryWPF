using Diary.Commands;
using DiaryWPF.Models;
using DiaryWPF.ViewModels;
using System.Windows;

namespace DiaryWPF.Commands
{
    /// <summary>
    /// Command which deletes a task.
    /// </summary>
    public class DeleteTaskCommand : CommandBase
    {
        private readonly EditTaskViewModel viewModel;

        public event EventHandler? TaskDeletedSuccessfully;

        public DeleteTaskCommand(EditTaskViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            MessageBoxResult result = MessageBox.Show(
                     "Are you sure you want to delete the task?",
                     "Confirmation",
                     MessageBoxButton.YesNo,
                     MessageBoxImage.Question
             );
            if (result == MessageBoxResult.Yes)
            {
                DiaryTask taskToDelete = viewModel.Task;

                if (taskToDelete != null)
                {
                    DiaryTask.RemoveTask(taskToDelete);
                }

                TaskDeletedSuccessfully?.Invoke(this, EventArgs.Empty);

                MessageBox.Show(
                     "The task successfully deleted!",
                     "Success!",
                     MessageBoxButton.OK,
                     MessageBoxImage.Information
                );
            }
        }
    }
}
