using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiaryWPF.View.UserControls
{
    /// <summary>
    /// Interaction logic for InputField.xaml
    /// </summary>
    public partial class InputField : UserControl, INotifyPropertyChanged, IDataErrorInfo
    {
        public InputField()
        {
            DataContext = this;
            InitializeComponent();
        }

        private string fieldCaption;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Error { get { return null; } }

        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        public string this[string name]
        {
            get
            {
                string result = null;

                switch (name)
                {
                    case "TextInputField":
                        if (string.IsNullOrWhiteSpace(TextInputField))
                            result = "Username cannot be empty";
                        else if (TextInputField.Length < 3)
                            result = "Username must be minimum of 4 characters";
                        break;
                }

                if (ErrorCollection.ContainsKey(name))
                    ErrorCollection[name] = result;
                else if (result != null)
                    ErrorCollection.Add(name, result);

                OnPropertyChanged("ErrorCollection");
                return result;
            }
        }

        public string FieldCaption
        {
            get { return fieldCaption; }
            set
            {
                fieldCaption = value;
                OnPropertyChanged();
            }
        }

        public string TextInputField
        {
            get { return textBoxInputField.Text; }
            set 
            { 
                textBoxInputField.Text = value; 
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
