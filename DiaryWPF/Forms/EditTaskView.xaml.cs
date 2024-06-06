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
        public EditTaskView(DiaryTask clickedTask)
        {
            InitializeComponent();
            EditTaskViewModel viewModel = new EditTaskViewModel(clickedTask);
            DataContext = viewModel;
        }
    }
}
