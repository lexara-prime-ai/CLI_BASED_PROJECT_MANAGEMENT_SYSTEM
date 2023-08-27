namespace ProjectManagementSystem.Entities
{
    /*
        **** Relationships ****
        User + Tasks => <one to many>
    */
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "user";
        public ICollection<AssignedTask> AssignedTasks { get; set; } = new List<AssignedTask>();

    }
}