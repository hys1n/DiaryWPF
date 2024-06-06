using Diary.Forms;
using Diary.Models;
using DiaryWPF.Forms;
using DiaryWPF.Models;
using DiaryWPF.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Diary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private object previousDataContext;

        public MainWindow()
        {
            InitializeComponent();
            CalendarViewModel viewModel = new CalendarViewModel();
            DataContext = viewModel;
        }

        private void btnAllTasks_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new AllTasksViewModel();
        }

        private void btnCalendar_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new CalendarViewModel();
        }

        private void btnInbox_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new InboxViewModel();
        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            previousDataContext = DataContext;

            AddTaskForm addTaskForm = new AddTaskForm
            {
                DataContext = new AddTaskFormViewModel()
            };

            if (addTaskForm.ShowDialog() == true)
            {
                CalendarViewModel.LoadDays();
            }

            DataContext = previousDataContext;
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to log out?",
                "Log Out Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if ( result == MessageBoxResult.Yes )
            {
                RegistrationForm registrationForm = new RegistrationForm();
                registrationForm.Show();

                this.Close();
            }
        }


    }
}