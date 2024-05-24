using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryWPF.Models
{
    public class Task
    {
        public int Title { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public Task() { }
        public Task(int title, DateTime date, string description)
        {
            Title = title;
            Date = date;
            Description = description;
        }
    }
}
