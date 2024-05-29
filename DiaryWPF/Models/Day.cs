using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryWPF.Models
{
    public class Day
    {
        public DateTime Date { get; set; }
        public string DayOfWeek => Date.ToString("ddd");
    }
}
