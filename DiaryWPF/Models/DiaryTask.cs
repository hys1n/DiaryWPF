using Diary.Models;
using System.Collections.ObjectModel;

namespace DiaryWPF.Models
{
    /// <summary>
    /// Class which stores information about a task.
    /// </summary>
    public class DiaryTask
    {
        public string Title { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public TimeSpan Duration { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public DiaryTask() { }

        public DiaryTask(string title, DateTime date, DateTime time, TimeSpan duration, 
            string location, string description, bool isCompleted)
        {
            Title = title;
            Date = date;
            Time = time;
            Duration = duration;
            Location = location;
            Description = description;
            IsCompleted = isCompleted;
        }


        /// <summary>
        /// Method which filters tasks.
        /// </summary>
        /// <param name="tasks">Tasks that are filtered.</param>
        /// <returns></returns>
        public static ObservableCollection<DiaryTask> FilterTasks(ObservableCollection<DiaryTask> tasks)
        {
            DateTime now = DateTime.Now;
            DateTime tomorrow = DateTime.Today.AddDays(1);
            DateTime oneHourFromNow = now.AddHours(1);
            return new ObservableCollection<DiaryTask>(
                tasks.Where(task =>
                    (
                        task.Date.Date == now.Date &&
                        task.Time.TimeOfDay >= now.TimeOfDay &&
                        task.Time.TimeOfDay <= oneHourFromNow.TimeOfDay
                    ) ||
                    (
                        task.Date.Date == tomorrow.Date &&
                        task.Time.TimeOfDay >= tomorrow.TimeOfDay &&
                        task.Time.TimeOfDay <= oneHourFromNow.TimeOfDay
                    )
                )
            );
        }

        /// <summary>
        /// Utility method for cloning an object
        /// </summary>
        /// <returns>The old copy of a task.</returns>
        public DiaryTask Clone()
        {
            return new DiaryTask
            {
                Title = Title,
                Date = Date,
                Time = Time,
                Duration = Duration,
                Location = Location,
                Description = Description,
                IsCompleted = IsCompleted,
            };
        }

        /// <summary>
        /// Utility method which restores a task to a condition
        /// before changes.
        /// </summary>
        /// <param name="taskToChange">Task to be restored.</param>
        /// <param name="originalTask">An original task.</param>
        public static void RestoreTask(DiaryTask taskToChange, DiaryTask originalTask)
        {
            taskToChange.Date = originalTask.Date;
            taskToChange.Title = originalTask.Title;
            taskToChange.IsCompleted = originalTask.IsCompleted;
            taskToChange.Time = originalTask.Time;
            taskToChange.Duration = originalTask.Duration;
            taskToChange.Description = originalTask.Description;
            taskToChange.Location = originalTask.Location;
        }

        /// <summary>
        /// Method which deletes a task.
        /// </summary>
        /// <param name="taskToDelete">Task to be deleted.</param>
        public static void RemoveTask(DiaryTask taskToDelete)
        {
            UserManager.CurrentUser.Tasks.Remove(taskToDelete);
        }
    }
}
