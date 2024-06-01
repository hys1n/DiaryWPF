using DiaryWPF.Models;
using System.ComponentModel;
using System.Windows;

namespace DiaryWPF.Forms
{
    /// <summary>
    /// Interaction logic for EditTaskForm.xaml
    /// </summary>
    public partial class EditTaskForm : Window
    {
        public Models.Task OriginalTask { get; set; }

        public Models.Task Task { get; set; }

        private bool isProgrammaticClose = false;

        public EditTaskForm(Models.Task task)
        {
            InitializeComponent();
            OriginalTask = task.Clone();
            Task = task;
            DataContext = task;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Task.Title) || Task.Title.Length > 32 
                || string.IsNullOrWhiteSpace(Task.Location) || AddTaskForm.IsValidStreetName(Task.Location))
            {
                MessageBox.Show("Not all input fields are filled or input text is invalid", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show(
                                    "Are you sure you want to edit?",
                                    "Confirmation",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Question
                            );
                if (result == MessageBoxResult.Yes)
                {
                    isProgrammaticClose = true;
                    UpdateViewData.LoadData();
                    Close();
                }
            }
        }

        private void RestoreTask()
        {
            Task.Date = OriginalTask.Date;
            Task.Title = OriginalTask.Title;
            Task.IsCompleted = OriginalTask.IsCompleted;
            Task.Time = OriginalTask.Time;
            Task.Duration = OriginalTask.Duration;
            Task.Description = OriginalTask.Description;
            Task.Location = OriginalTask.Location;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {            MessageBoxResult result = MessageBox.Show(
                    "Are you sure you want to undo the changes?",
                    "Confirmation",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
            );
            if (result == MessageBoxResult.Yes)
            {
                RestoreTask();
                isProgrammaticClose = true;
                UpdateViewData.LoadData();
                Close();
            }
        }

        private void btnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                     "Are you sure you want to delete the task?",
                     "Confirmation",
                     MessageBoxButton.YesNo,
                     MessageBoxImage.Question
             );
            if (result == MessageBoxResult.Yes)
            {
                Models.Task taskToDelete = DataContext as Models.Task;

                if (taskToDelete != null)
                {
                    MainWindow.Tasks.Remove(taskToDelete);
                }

                isProgrammaticClose = true;
                Close();
                UpdateViewData.LoadData();

                MessageBox.Show(
                     "The task successfully deleted!",
                     "Success!",
                     MessageBoxButton.OK,
                     MessageBoxImage.Information
                );
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!isProgrammaticClose)
            {
                MessageBoxResult result = MessageBox.Show(
                    "Are you sure you want to undo the changes?",
                    "Confirmation",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                if (result == MessageBoxResult.Yes)
                {
                    RestoreTask();
                    UpdateViewData.LoadData();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
