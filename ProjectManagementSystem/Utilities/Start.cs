using ProjectManagementSystem.DbConfig;

namespace ProjectManagementSystem.Utilities
{
    public class Start
    {
        public static async Task START_PROGRAM()
        {

            ProjectManagementDbContext context = new();
            context.Database.EnsureCreated();

            try
            {
                // Display start message
                Console.Write("\n<< STARTING >> ", Console.ForegroundColor = ConsoleColor.Green);
                Console.WriteLine("Project | Task Management System...\n", Console.ForegroundColor = ConsoleColor.White);

                // User authorization
                Authorization authorization = new();
                await authorization.AUTHORIZE_USER();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}