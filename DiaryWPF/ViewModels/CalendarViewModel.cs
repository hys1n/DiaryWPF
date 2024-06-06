using Diary.Models;
using Diary.ViewModels;
using DiaryWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryWPF.ViewModels
{
    /// <summary>
    /// ViewModel which is responsible for showing and getting data
    /// from CalendarView.
    /// </summary>
    public class CalendarViewModel : ViewModelBase
    {
        private static DateTime chosenDate = DateTime.Now;

        private static ObservableCollection<Day> days;

        public static DateTime ChosenDate
        {
            get { return chosenDate; }
            set
            {
                if (chosenDate != value)
                {
                    chosenDate = value;
                    LoadDays();
                }
            }
        }

        public static ObservableCollection<Day> Days
        {
            get { return days; }
            set
            {
                days = value;
            }
        }

        public CalendarViewModel()
        {
            Days = new ObservableCollection<Day>();
            LoadDays();
        }

        /// <summary>
        /// Method for showing dates in a calendar.
        /// </summary>
        public static void LoadDays()
        {
            Days.Clear();
            var newDays = Day.LoadDays(ChosenDate, UserManager.CurrentUser.Tasks);
            foreach (var day in newDays)
            {
                Days.Add(day);
            }
        }

    }
}
