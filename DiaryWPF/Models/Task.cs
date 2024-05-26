using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryWPF.Models
{
    public class Task
    {
        public string Title { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time {  get; set; } 

        public TimeSpan Duration { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public Task() { }
        public Task(string title, DateTime date, DateTime time, TimeSpan duration, string location, string description)
        {
            Title = title;
            Date = date;
            Time = time;
            Duration = duration;
            Location = location;
            Description = description;
        }
    }
}
