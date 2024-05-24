using DiaryWPF.Forms;
using DiaryWPF.Models;
using System.Windows;

namespace DiaryWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<User> users;

        public MainWindow()
        {
            InitializeComponent();
            users = new List<User>();

            var loginWindow = new RegistrationForm();
            bool? dialogResult = loginWindow.ShowDialog();

            if (dialogResult != true)
            {
                Application.Current.Shutdown();
            }
        }

        private void btnCalendar_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}