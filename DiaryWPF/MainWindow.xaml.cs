﻿using DiaryWPF.Forms;
using DiaryWPF.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DiaryWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<User> users;

        public static User currentUser;

        private static ObservableCollection<Models.Task> tasks;

        public ObservableCollection<Day> Days { get; set; }

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

        public MainWindow()
        {
            InitializeComponent();
            users = new List<User>();

            DataContext = this;
            LoadDays();

            //var loginWindow = new RegistrationForm();
            //bool? dialogResult = loginWindow.ShowDialog();

            //if (dialogResult != true)
            //{
            //    Application.Current.Shutdown();
            //}

            ObservableCollection<Models.Task> dummyTasks = new ObservableCollection<Models.Task>();
            for (int i = 0; i < 3; i++)
            {
                Models.Task task = new Models.Task(
                    "Meeting with John",
                    new DateTime(2024, 6, 1),
                    new DateTime(2024, 6, 1, 14, 0, 0),
                    new TimeSpan(1, 0, 0),
                    "Office",
                    "Discuss quarterly results",
                    false
                );

                dummyTasks.Add(task);
            }
            User dummyUser = new User(0, "User", "email@gmail.com", "1234", dummyTasks);
            currentUser = dummyUser;
        }

        private void LoadDays()
        {
            Days = new ObservableCollection<Day>();
            DateTime firstWeekDay = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            for (int i = 0; i < 7; i++)
            {
                Days.Add(new Day { Date = firstWeekDay.AddDays(i) });
            }
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

        private void btnCalendar_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            AddTaskForm atForm = new AddTaskForm();
            atForm.ShowDialog();
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
    }
}