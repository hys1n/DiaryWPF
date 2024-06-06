using Diary.Commands;
using Diary.Models;
using DiaryWPF.Models;
using DiaryWPF.ViewModels;
using System.Windows;

namespace DiaryWPF.Commands
{
    /// <summary>
    /// Command which add a new task.
    /// </summary>
    public class AddTaskCommand : CommandBase
    {
        private readonly AddTaskFormViewModel viewModel;

        public event EventHandler? TaskAddedSuccessfully;

        public AddTaskCommand(AddTaskFormViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            if (viewModel.ErrorCollection.Count > 0 && viewModel.ErrorCollection.Any(e => e.Value != null))
            {
                MessageBox.Show("Not all input fields are filled or input text is invalid", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                DiaryTask newTask = new DiaryTask()
                {
                    Title = viewModel.TaskTitle,
                    Date = viewModel.TaskDateTime,
                    Time = viewModel.TaskTime,
                    Duration = viewModel.TaskDuration,
                    Location = viewModel.TaskLocation,
                    Description = viewModel.TaskDescription,
                    IsCompleted = false
                };

                UserManager.CurrentUser.Tasks.Add(newTask);

                MessageBox.Show("Task added successfully!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                TaskAddedSuccessfully?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
