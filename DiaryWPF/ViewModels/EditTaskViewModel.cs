using DiaryWPF.Commands;
using DiaryWPF.Forms;
using DiaryWPF.Models;
using System.Windows;

namespace DiaryWPF.ViewModels
{
    /// <summary>
    /// View model for EditTaskView.
    /// </summary>
    public class EditTaskViewModel
    {
        public DiaryTask OriginalTask { get; set; }

        public DiaryTask Task { get; set; }

        public EditTaskCommand EditTaskCommand { get; set; }

        public CancelTaskCommand CancelTaskCommand { get; set; }

        public DeleteTaskCommand DeleteTaskCommand { get; set; }


        public EditTaskViewModel(DiaryTask clickedTask)
        {
            OriginalTask = clickedTask.Clone();
            Task = clickedTask;

            EditTaskCommand = new EditTaskCommand(this);
            EditTaskCommand.TaskEditedSuccessfully += OnTaskEditedSuccessfully;
            CancelTaskCommand = new CancelTaskCommand(this);
            CancelTaskCommand.TaskCancelledSuccessfully += OnTaskCancelledSuccessfully;
            DeleteTaskCommand = new DeleteTaskCommand(this);
            DeleteTaskCommand.TaskDeletedSuccessfully += OnTaskDeletedSuccessfully;
        }

        public void OnTaskEditedSuccessfully(object? sender, EventArgs e)
        {
            CloseModal();
        }

        public void OnTaskCancelledSuccessfully(object? sender, EventArgs e)
        {
            CloseModal();
        }

        private void OnTaskDeletedSuccessfully(object sender, EventArgs e)
        {
            CloseModal();
        }

        /// <summary>
        /// Closes the current modal window.
        /// </summary>
        private void CloseModal()
        {
            EditTaskView.IsProgrammaticClose = true;
            if (Application.Current.Windows.OfType<EditTaskView>()
                .SingleOrDefault(w => w.DataContext == this) is Window window)
            {
                window.DialogResult = true;
                CalendarViewModel.LoadDays();
                window.Close();
                EditTaskView.IsProgrammaticClose = false;
            }
            EditTaskView.IsProgrammaticClose = false;
        }
    }
}
