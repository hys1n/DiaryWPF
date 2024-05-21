using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace DiaryWPF.View.UserControls
{
    /// <summary>
    /// Interaction logic for IconButton.xaml
    /// </summary>
    public partial class IconButton : UserControl, INotifyPropertyChanged
    {
        public IconButton()
        {
            DataContext = this;
            InitializeComponent();
        }

        private string placeholder;
        public event PropertyChangedEventHandler? PropertyChanged;

        public string Placeholder
        {
            get { return placeholder; }
            set 
            {
                placeholder = value;
                OnPropertyChanged();
            }
        }

        private string icon;

        public string Icon
        {
            get { return icon; }
            set
            {
                icon = value;
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
