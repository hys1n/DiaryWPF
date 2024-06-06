using DiaryWPF.Forms;
using DiaryWPF.Models;
using DiaryWPF.ViewModels;
using System.Windows.Controls;

namespace DiaryWPF.Views
{
    /// <summary>
    /// Interaction logic for InboxView.xaml
    /// </summary>
    public partial class InboxView : UserControl
    {
        public InboxView()
        {
            InitializeComponent();
            InboxViewModel viewModel = new InboxViewModel();
            DataContext = viewModel;
        }

        private void ListBoxItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListBoxItem item = sender as ListBoxItem;
            if (item != null)
            {
                DiaryTask clickedTask = item.DataContext as DiaryTask;
                if (clickedTask != null)
                {
                    EditTaskView modifyTaskWindow = new EditTaskView(clickedTask);
                    modifyTaskWindow.DataContext = new EditTaskViewModel(clickedTask);
                    modifyTaskWindow.ShowDialog();
                }
            }
        }
    }
}
