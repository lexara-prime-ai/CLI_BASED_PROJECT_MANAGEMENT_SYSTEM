using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.DbConfig
{
    /* 
        Database & Entity configuration 
    */
    public class ProjectManagementDbContext : DbContext
    {
        /*
            Default fields : Path to connection string
        */
        static readonly string rdx_PROJECT_PATH = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
        static readonly string rdx_CONNECTION_STRING_PATH = $"{rdx_PROJECT_PATH}\\DbConfig\\Environment.txt";

        /*
            TABLE WRAPPERS aka DbSets
        */
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<AssignedTask> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                string connectionString = File.ReadAllText(rdx_CONNECTION_STRING_PATH);
                optionsBuilder.UseSqlServer(connectionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // <one to many> relationship between a Project & Assigned Tasks
            modelBuilder.Entity<Project>()
                .HasMany(e => e.AssignedTasks)
                .WithOne(e => e.Project)
                .HasForeignKey(e => e.ProjectId)
                .IsRequired();

            // <one to many> relationship between a User & Assigned Tasks
            modelBuilder.Entity<User>()
                .HasMany(e => e.AssignedTasks)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();
        }
    }
}