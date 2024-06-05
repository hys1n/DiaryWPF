using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryWPF.Models
{
    /// <summary>
    /// Class which stores all the data about a day.
    /// </summary>
    public class Day
    {
        public DateTime Date { get; set; }

        public string DayOfWeek => Date.ToString("ddd");

        public ObservableCollection<DiaryTask> Tasks { get; set; }

        public Day()
        {
            Tasks = new ObservableCollection<DiaryTask>();
        }

        /// <summary>
        /// Utility method for finding dates of the week and 
        /// sorting tasks by days.
        /// </summary>
        public static ObservableCollection<Day> LoadDays(DateTime chosenDate, ObservableCollection<DiaryTask> tasks)
        {
            var days = new ObservableCollection<Day>();

            DateTime firstWeekDay = chosenDate.AddDays(-(int)chosenDate.DayOfWeek + (int)System.DayOfWeek.Monday);
            if (chosenDate.DayOfWeek == System.DayOfWeek.Sunday)
                firstWeekDay = chosenDate.AddDays(-7 + (int)System.DayOfWeek.Monday);

            for (int i = 0; i < 7; i++)
            {
                Day day = new Day { Date = firstWeekDay.AddDays(i) };
                if (tasks != null && day != null)
                {
                    day.Tasks = new ObservableCollection<DiaryTask>(tasks.Where(task => task.Date.Date == day.Date.Date));
                }
                days.Add(day);
            }

            return days;
        }
    }
}
