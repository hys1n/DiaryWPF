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
    }
}
