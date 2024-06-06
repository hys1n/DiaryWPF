using DiaryWPF.Commands;
using DiaryWPF.Models;
using DiaryWPF.ViewModels;
using System.Windows;

namespace DiaryWPF.Forms
{
    /// <summary>
    /// Interaction logic for EditTaskView.xaml
    /// </summary>
    public partial class EditTaskView : Window
    {
        private EditTaskViewModel viewModel;

        public EditTaskView(DiaryTask clickedTask)
        {
            InitializeComponent();
            viewModel = new EditTaskViewModel(clickedTask);
            DataContext = viewModel;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (viewModel.CancelTaskCommand.CanExecute(null))
            {
                viewModel.CancelTaskCommand.Execute(null);
                if (viewModel.CancelTaskCommand is CancelTaskCommand cancelCommand)
                {
                    if (cancelCommand.WasCancelled == false)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }
    }
}
