using System.Collections.ObjectModel;

namespace DiaryWPF.Models
{
    public class Day
    {
        public DateTime Date { get; set; }

        public string DayOfWeek => Date.ToString("ddd");

        public ObservableCollection<Models.Task> Tasks { get; set; } 

        public Day()
        {
            Tasks = new ObservableCollection<Models.Task>();
        }
    }
}
