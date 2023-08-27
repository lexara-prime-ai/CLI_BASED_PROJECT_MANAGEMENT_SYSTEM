namespace ProjectManagementSystem.Entities
{
    /*
        **** Relationships ****
        Task + User => <many to one>
        Task + Project => <many to one>
    */
    public class AssignedTask
    {
        public int AssignedTaskId { get; set; }
        public string TaskTitle { get; set; } = string.Empty;
        public int ProjectId { get; set; } // Foreign Key Property
        public Project Project { get; set; } = new Project();
        public int UserId { get; set; } // Foreign Key Property
        public User User { get; set; } = new User();
        public bool Completed { get; set; } = false;
        
    }
}