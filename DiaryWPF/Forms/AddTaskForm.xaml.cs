using DiaryWPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DiaryWPF.Forms
{
    /// <summary>
    /// Interaction logic for AddTaskForm.xaml
    /// </summary>
    public partial class AddTaskForm : Window, IDataErrorInfo, INotifyPropertyChanged
    {
        public AddTaskForm()
        {
            DataContext = this;
            InitializeComponent();
        }

        private string taskTitle;

        private DateTime taskDateTime = DateTime.Now;

        private DateTime taskTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 0, 0);

        private TimeSpan taskDuration = TimeSpan.FromMinutes(30);

        private string taskLocation;

        private string taskDescription;

        private bool isProgrammaticClose = false;

        public string Error { get { return null; } }

        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        public event PropertyChangedEventHandler? PropertyChanged;

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
                            result = "Task must be a maximum of 32 characters";
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

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //private void SetDefaultInputFields()
        //{
        //    TaskTitle = "";
        //    OnPropertyChanged(nameof(TaskTitle));

        //    TaskLocation = "";
        //    OnPropertyChanged(nameof(TaskLocation));

        //    TaskDescription = "";
        //    OnPropertyChanged(nameof(TaskDescription));

        //    TaskDateTime = DateTime.Now;
        //    OnPropertyChanged(nameof(TaskDateTime));

        //    TaskTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 0, 0);
        //    OnPropertyChanged(nameof(TaskTime));

        //    TaskDuration = TimeSpan.FromMinutes(30);
        //    OnPropertyChanged(nameof(TaskDuration));
        //}

        private void AddTask()
        {
            if (ErrorCollection.Count > 0 && ErrorCollection.Any(e => e.Value != null))
            {
                MessageBox.Show("Not all input fields are filled or input text is invalid", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Models.Task newTask = new Models.Task()
                {
                    Title = TaskTitle,
                    Date = TaskDateTime,
                    Time = TaskTime,
                    Duration = TaskDuration,
                    Location = TaskLocation,
                    Description = TaskDescription,
                    IsCompleted = false
                };

                MainWindow.currentUser.Tasks.Add(newTask);
                
                //SetDefaultInputFields();
                isProgrammaticClose = true;
                Close();

                UpdateViewData.LoadData();
                MessageBox.Show("Task added successfully!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddTask();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to exit?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.Yes)
            {
                isProgrammaticClose = true;
                Close();
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!isProgrammaticClose)
            {
                MessageBoxResult result = MessageBox.Show(
                    "Are you sure you want to exit?",
                    "Confirmation",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
