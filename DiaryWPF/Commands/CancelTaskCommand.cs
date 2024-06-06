using Diary.Commands;
using DiaryWPF.Models;
using DiaryWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiaryWPF.Commands
{
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
