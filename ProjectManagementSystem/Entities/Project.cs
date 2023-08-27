namespace ProjectManagementSystem.Entities
{
    /*
        **** Relationships ****
        Project + Tasks => <one to many>
    */
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; } = string.Empty;
        public string ProjectDescription { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public ICollection<AssignedTask> AssignedTasks { get; set; } = new List<AssignedTask>();
    }
}