using DiaryWPF.ViewModels;
using System.Windows.Controls;

namespace DiaryWPF.Views
{
    /// <summary>
    /// Interaction logic for AllTasksView.xaml
    /// </summary>
    public partial class AllTasksView : UserControl
    {
        public AllTasksView()
        {
            InitializeComponent();
            AllTasksViewModel viewModel = new AllTasksViewModel();
            DataContext = viewModel;
        }
    }
}
