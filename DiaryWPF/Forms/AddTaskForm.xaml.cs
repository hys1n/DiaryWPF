using DiaryWPF.ViewModels;
using System.Windows;

namespace DiaryWPF.Forms
{
    /// <summary>
    /// Interaction logic for AddTaskForm.xaml
    /// </summary>
    public partial class AddTaskForm : Window
    {
        public AddTaskForm()
        {
            InitializeComponent();
            AddTaskFormViewModel viewModel = new AddTaskFormViewModel();
            DataContext = viewModel;
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
                DialogResult = false;
            }
        }
    }
}
