namespace ToDoList.RestfulAPI.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; } = false;
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
