using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryWPF.Models
{
    public class Day
    {
        public DateTime Date { get; set; }

        public string DayOfWeek => Date.ToString("ddd");

        public ObservableCollection<Models.Event> Tasks { get; set; } 

        public Day()
        {
            Tasks = new ObservableCollection<Models.Event>();
        }
    }
}
