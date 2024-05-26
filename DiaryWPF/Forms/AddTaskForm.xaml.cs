using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DiaryWPF.Forms
{
    /// <summary>
    /// Interaction logic for AddTaskForm.xaml
    /// </summary>
    public partial class AddTaskForm : Window
    {
        public AddTaskForm()
        {
            DataContext = this;
            InitializeComponent();
        }

        private string taskTitle;

        private DateTime taskDateTime = DateTime.Now;

        private DateTime taskTime;

        private TimeSpan taskDuration;

        private string taskLocation;

        private string taskDescription;

        public string TaskTitle
        {
            get { return taskTitle; }
            set { taskTitle = value; }
        }

        public DateTime TaskDateTime
        {
            get { return taskDateTime; }
            set { taskDateTime = value; }
        }

        public DateTime TaskTime
        {
            get { return taskTime; }
            set { taskTime = value; }
        }

        public string TaskLocation
        {
            get { return taskLocation; }
            set { taskLocation = value; }
        }

        public TimeSpan TaskDuration
        {
            get { return taskDuration; }
            set { taskDuration = value; }
        }

        public string TaskDescription
        {
            get { return taskDescription; }
            set { taskDescription = value; }
        }

        private void textBoxLocation_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
