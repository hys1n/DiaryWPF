namespace DiaryWPF.Models
{
    internal class User
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Id { get; set; }
        public List<Task> Tasks { get; set; }

        public User() { }

        public User(int id, string userName, string email, string password)
        {
            Id = id;
            UserName = userName;
            Email = email;
            Password = password;
        }
    }
}
