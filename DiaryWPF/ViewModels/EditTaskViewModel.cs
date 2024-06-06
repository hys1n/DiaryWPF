using Diary.Commands;
using DiaryWPF.Commands;
using DiaryWPF.Forms;
using DiaryWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DiaryWPF.ViewModels
{
    public class EditTaskViewModel
    {
        public DiaryTask OriginalTask { get; set; }

        public DiaryTask Task { get; set; }

        public EditTaskCommand EditTaskCommand { get; set; }

        public CancelTaskCommand CancelTaskCommand { get; set; }


        public EditTaskViewModel(DiaryTask clickedTask)
        {
            OriginalTask = clickedTask.Clone();
            Task = clickedTask;

            EditTaskCommand = new EditTaskCommand(this);
            EditTaskCommand.TaskEditedSuccessfully += OnTaskEditedSuccessfully;
            CancelTaskCommand = new CancelTaskCommand(this);
            CancelTaskCommand.TaskCancelledSuccessfully += OnTaskCancelledSuccessfully;
        }

        public void OnTaskEditedSuccessfully(object? sender, EventArgs e)
        {
            if (Application.Current.Windows.OfType<EditTaskView>()
                .SingleOrDefault(w => w.DataContext == this) is Window window)
            {
                window.DialogResult = true;
                CalendarViewModel.LoadDays();
                window.Close();
            }
        }

        public void OnTaskCancelledSuccessfully(object? sender, EventArgs e)
        {
            if (Application.Current.Windows.OfType<EditTaskView>()
                .SingleOrDefault(w => w.DataContext == this) is Window window)
            {
                window.DialogResult = true;
                CalendarViewModel.LoadDays();
                window.Close();
            }
        }
    }
}
