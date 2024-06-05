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
    }
}