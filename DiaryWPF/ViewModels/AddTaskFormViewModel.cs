using Diary;
using Diary.Models;
using Diary.ViewModels;
using DiaryWPF.Commands;
using DiaryWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace DiaryWPF.ViewModels
{
    public class AddTaskFormViewModel : ViewModelBase, IDataErrorInfo
    {
        private string taskTitle;

        private DateTime taskDateTime = DateTime.Now;

        private DateTime taskTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 0, 0);

        private TimeSpan taskDuration = TimeSpan.FromMinutes(30);

        private string taskLocation;

        private string taskDescription;

        public AddTaskCommand AddTaskCommand { get; set; }

        public string Error => throw new NotImplementedException();

        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        public AddTaskFormViewModel()
        {
            AddTaskCommand = new AddTaskCommand(this);
            AddTaskCommand.TaskAddedSuccessfully += OnAddedTaskSuccessfully;
        }

        public string TaskTitle
        {
            get { return taskTitle; }
            set { taskTitle = value.Trim(); }
        }

        public DateTime TaskDateTime
        {
            get { return taskDateTime; }
            set { taskDateTime = value; }
        }

        public DateTime TaskTime
        {
            get { return taskTime; }
            set { taskTime = value; }
        }

        public string TaskLocation
        {
            get { return taskLocation; }
            set { taskLocation = value.Trim(); }
        }

        public TimeSpan TaskDuration
        {
            get { return taskDuration; }
            set { taskDuration = value; }
        }

        public string TaskDescription
        {
            get { return taskDescription; }
            set { taskDescription = value.Trim(); }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;

                switch (columnName)
                {
                    case nameof(TaskTitle):
                        if (string.IsNullOrEmpty(TaskTitle))
                            result = "Task title cannot be empty";
                        else if (TaskTitle.Length > 32)
                            result = "Task title must be a maximum of 32 characters";
                        break;
                    case nameof(TaskLocation):
                        if (string.IsNullOrEmpty(TaskLocation))
                            result = "Task location cannot be empty";
                        else if (IsValidStreetName(TaskLocation))
                            result = "Name is invalid. Name format: 'вул. Григорія Сковороди 23'";
                        break;
                }

                if (ErrorCollection.ContainsKey(columnName))
                    ErrorCollection[columnName] = result;
                else if (result != null)
                    ErrorCollection.Add(columnName, result);

                OnPropertyChanged("ErrorCollection");
                return result;
            }
        }

        public static bool IsValidStreetName(string streetName)
        {
            if (string.IsNullOrWhiteSpace(streetName))
            {
                return false;
            }

            string pattern = @"^[А-Яа-яЇїЄєІіҐґA-Za-z0-9]+(?:[ '-][А-Яа-яЇїЄєІіҐґA-Za-z0-9]+)*$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(streetName);
        }

        public void OnAddedTaskSuccessfully(object? sender, EventArgs e)
        {
            if (Application.Current.Windows.OfType<Window>()
                .SingleOrDefault(w => w.DataContext == this) is Window window)
            {
                window.DialogResult = true;
                window.Close();
            }
        }
    }
}
