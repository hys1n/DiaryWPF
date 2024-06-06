using Diary.Commands;
using DiaryWPF.Models;
using DiaryWPF.ViewModels;
using System.Windows;

namespace DiaryWPF.Commands
{
    /// <summary>
    /// Command which cancels any changes done 
    /// to a taks.
    /// </summary>
    public class CancelTaskCommand : CommandBase
    {
        private readonly EditTaskViewModel viewModel;

        public event EventHandler? TaskCancelledSuccessfully;

        public CancelTaskCommand(EditTaskViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            MessageBoxResult result = MessageBox.Show(
                    "Are you sure you want to undo the changes?",
                    "Confirmation",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
            );
            if (result == MessageBoxResult.Yes)
            {
                DiaryTask.RestoreTask(viewModel.Task, viewModel.OriginalTask);
                CalendarViewModel.LoadDays();
                TaskCancelledSuccessfully?.Invoke(this, EventArgs.Empty);
            }
        }

    }
}
