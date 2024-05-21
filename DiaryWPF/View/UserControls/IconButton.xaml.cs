using FontAwesome.WPF;
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
        private string icon;
        private FontAwesomeIcon iconParsed;

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

        public string Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                if (Enum.TryParse(icon, true, out FontAwesomeIcon result))
                {
                    iconParsed = result;
                    // Notify that 'Icon' property has changed
                    OnPropertyChanged();
                    // Notify that 'IconParsed' property has changed for binding
                    OnPropertyChanged(nameof(IconParsed));
                }
            }
        }

        public FontAwesomeIcon IconParsed
        {
            get { return iconParsed; }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
