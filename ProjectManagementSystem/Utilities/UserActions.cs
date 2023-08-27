namespace ProjectManagementSystem.Utilities
{
    /*
        This class encapsulates methods for displaying menus and handling user actions
        related to tasks, assignments, and project management.
    */
    public class UserActions
    {
        // Admin actions
        public static void DISPLAY_USER_ACTIONS()
        {
            Console.WriteLine("* Displaying {{ User }} Actions: ", Console.ForegroundColor = ConsoleColor.White);

            // User Actions
            Console.WriteLine("1. View assigned tasks\n2. Mark task as completed\n");
            Console.WriteLine("* Select an option >");
            // Handle User Actions
            HANDLE_USER_ACTIONS();
        }

        // Handle User Actions
        public static void HANDLE_USER_ACTIONS()
        {
            // Read user input
            string? USER_INPUT = Console.ReadLine();

            // Validate input
            if (!string.IsNullOrWhiteSpace(USER_INPUT))
            {
                // Display Selected Option
                Console.WriteLine($"Selected option: {USER_INPUT}");

                switch (USER_INPUT)
                {
                    case "1":
                        Console.WriteLine("Displaying assigned tasks...");
                        break;
                    case "2":
                        Console.WriteLine("Displaying tasks to mark as completed...");
                        break;
                    default:
                        Console.Write("Ivalid option! ", Console.ForegroundColor = ConsoleColor.Red);
                        Console.Write("Try again...", Console.ForegroundColor = ConsoleColor.White);
                        HANDLE_USER_ACTIONS();
                        break;
                }

            }
            else
            {
                Console.Write("Ivalid option! ", Console.ForegroundColor = ConsoleColor.Red);
                Console.Write("Try again...", Console.ForegroundColor = ConsoleColor.White);
                HANDLE_USER_ACTIONS();
            }


        }

        // User actions
        public static void DISPLAY_ADMIN_ACTIONS()
        {
            Console.WriteLine("* Displaying {{ Admin }} Actions: ", Console.ForegroundColor = ConsoleColor.White);

            // Admin Actions
            Console.WriteLine("1. View projects");
            Console.WriteLine("2. Create project");
            Console.WriteLine("3. Update project");
            Console.WriteLine("4. Delete project");
            Console.WriteLine("5. View assigned tasks");

            // Handle User Actions
            HANDLE_ADMIN_ACTIONS();
        }

        // Handle Admin Actions
        public static void HANDLE_ADMIN_ACTIONS()
        {
            Console.WriteLine("Handling admin actions");
        }
    }
}