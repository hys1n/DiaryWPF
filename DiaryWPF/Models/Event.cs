namespace DiaryWPF.Models
{
    public class Event
    {
        public string Title { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time {  get; set; } 

        public TimeSpan Duration { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public Event() { }

        public Event(string title, DateTime date, DateTime time, TimeSpan duration, string location, string description, bool isCompleted)
        {
            Title = title;
            Date = date;
            Time = time;
            Duration = duration;
            Location = location;
            Description = description;
            IsCompleted = isCompleted;
        }

        // Utility method for cloning an object
        public Event Clone()
        {
            return new Event
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
    }
}
