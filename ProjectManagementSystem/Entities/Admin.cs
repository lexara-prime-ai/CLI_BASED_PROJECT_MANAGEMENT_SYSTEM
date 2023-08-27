namespace ProjectManagementSystem.Entities
{
    /*
        **** Relationships ****
        Admin + Projects => <one to many>
    */
    public class Admin
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public ICollection<Project> Projects = new List<Project>();
    }
}