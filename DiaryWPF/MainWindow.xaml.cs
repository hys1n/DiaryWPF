﻿using DiaryWPF.Forms;
using DiaryWPF.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace DiaryWPF
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public static List<User> users;

        public static User currentUser;

        private static ObservableCollection<Models.Task> tasks;

        private ObservableCollection<Day> days;

        private ObservableCollection<Models.Task> filteredTasks;

        private DateTime chosenDate = DateTime.Now;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();

            LoadUsersFromJson();
            DataContext = this;
            RegisterForm();

            //ObservableCollection<Models.Task> dummyTasks = new ObservableCollection<Models.Task>();
            //for (int i = 0; i < 4; i++)
            //{
            //    Models.Task task = new Models.Task(
            //        $"Task_{i}",
            //        new DateTime(2024, 6, 2),
            //        new DateTime(2024, 6, 2, 17, 0, 0),
            //        new TimeSpan(1, 0, 0),
            //        "вул. Шевченка",
            //        "",
            //        false
            //    );

            //    dummyTasks.Add(task);
            //}
            //User dummyUser = new User(0, "User", "email@gmail.com", "1234", dummyTasks);
            //currentUser = dummyUser;
        }

        public ObservableCollection<Models.Task> FilteredTasks
        {
            get { return filteredTasks; }
            set
            {
                if (filteredTasks != value)
                {
                    filteredTasks = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime ChosenDate
        {
            get { return chosenDate; }
            set
            {
                if (chosenDate != value)
                {
                    chosenDate = value;
                    OnPropertyChanged();
                    LoadDays();
                }
            }
        }

        public ObservableCollection<Day> Days
        {
            get { return days; }
            set
            {
                if (days != value)
                {
                    days = value;
                    OnPropertyChanged();
                }
            }
        }

        public static ObservableCollection<Models.Task> Tasks
        {
            get
            {
                if (currentUser != null)
                {
                    tasks = currentUser.Tasks;
                }
                return tasks;
            }
            set
            {
                tasks = value;
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SaveUsersToJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true 
            };
            string json = JsonSerializer.Serialize(users, options);
            File.WriteAllText("users.json", json);
        }

        private void LoadUsersFromJson()
        {
            if (File.Exists("users.json"))
            {
                string json = File.ReadAllText("users.json");
                users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            else
            {
                users = new List<User>();
            }
        }

        // Invokes a register form for singing up/in
        public void RegisterForm()
        {
            var loginWindow = new RegistrationForm();
            bool? dialogResult = loginWindow.ShowDialog();

            if (dialogResult != true)
            {
                Application.Current.Shutdown();
            }
        }

        public void LoadDays()
        {
            Days = new ObservableCollection<Day>();

            DateTime firstWeekDay = ChosenDate.AddDays(-(int)ChosenDate.DayOfWeek + (int)DayOfWeek.Monday);
            if (ChosenDate.DayOfWeek == DayOfWeek.Sunday)
                firstWeekDay = ChosenDate.AddDays(-7 + (int)DayOfWeek.Monday);

            for (int i = 0; i < 7; i++)
            {
                Day day = new Day { Date = firstWeekDay.AddDays(i) };
                if (Tasks != null && day != null)
                {
                    day.Tasks = new ObservableCollection<Models.Task>(Tasks.Where(task => task.Date.Date == day.Date.Date));
                }
                Days.Add(day);
            }
        }

        public void FilterTasks()
        {
            DateTime now = DateTime.Now;
            DateTime tomorrow = DateTime.Today.AddDays(1);
            DateTime oneHourFromNow = now.AddHours(1);
            FilteredTasks = new ObservableCollection<Models.Task>(
                Tasks.Where(task => 
                    (
                        task.Date.Date == now.Date && 
                        task.Time.TimeOfDay >= now.TimeOfDay && 
                        task.Time.TimeOfDay <= oneHourFromNow.TimeOfDay
                    ) ||
                    (
                        task.Date.Date == tomorrow.Date &&
                        task.Time.TimeOfDay >= tomorrow.TimeOfDay &&
                        task.Time.TimeOfDay <= oneHourFromNow.TimeOfDay
                    )
                )
            );
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to log out?",
                "Log Out Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.Yes)
            {
                SaveUsersToJson();
                Hide();


                var registrationForm = new RegistrationForm();
                bool? dialogResult = registrationForm.ShowDialog();

                if (dialogResult != true)
                {
                    Application.Current.Shutdown();
                }
                else
                {
                    Show();
                    Tasks = new ObservableCollection<Models.Task>(currentUser.Tasks);
                }
            }
        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            AddTaskForm atForm = new AddTaskForm();
            atForm.ShowDialog();

            UpdateViewData.LoadData();
        }

        private void ListBoxItem_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListBoxItem item = sender as ListBoxItem;
            if (item != null)
            {
                Models.Task clickedTask = item.DataContext as Models.Task;
                if (clickedTask != null)
                {
                    EditTaskForm modifyTaskWindow = new EditTaskForm(clickedTask);
                    modifyTaskWindow.ShowDialog();
                }
            }
        }

        // Hides all grids and shows only the proper one
        private void ShowGrid(Grid gridToShow)
        {
            CalendarGrid.Visibility = Visibility.Collapsed;
            InboxGrid.Visibility = Visibility.Collapsed;
            AllTasksGrid.Visibility = Visibility.Collapsed;

            gridToShow.Visibility = Visibility.Visible;
        }

        private void btnInbox_Click(object sender, RoutedEventArgs e)
        {
            ShowGrid(InboxGrid);
        }

        private void btnCalendar_Click(object sender, RoutedEventArgs e)
        {
            ShowGrid(CalendarGrid);
        }

        private void btnAllTasks_Click(object sender, RoutedEventArgs e)
        {
            ShowGrid(AllTasksGrid);
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to exit?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );
            if ( result == MessageBoxResult.Yes )
            {
                SaveUsersToJson();
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}