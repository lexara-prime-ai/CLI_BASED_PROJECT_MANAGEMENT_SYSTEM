using ProjectManagementSystem.DbConfig;
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.Utilities
{
    /*
        The Authorization class manages user access control
        through streamlined registration and login procedures.
    */
    public class Authorization
    {
        /*
            Globally accessible fields
        */
        readonly ProjectManagementDbContext context = new();

        /*
            User Authorization
        */
        public void AUTHORIZE_USER()
        {
            Console.WriteLine("Please select an option to continue:\n1. Register\n2. Login\n",
                Console.ForegroundColor = ConsoleColor.White
            );

            string? USER_INPUT = Console.ReadLine();

            // Validate input
            if (!string.IsNullOrWhiteSpace(USER_INPUT))
            {
                switch (USER_INPUT)
                {
                    case "1":
                        REGISTER_USER();
                        break;
                    case "2":
                        LOGIN_USER();
                        break;

                    default:
                        Console.Write("ERROR: ", Console.ForegroundColor = ConsoleColor.White);
                        Console.WriteLine("Invalid input...", Console.ForegroundColor = ConsoleColor.Red);
                        AUTHORIZE_USER();
                        break;
                }
            }
            else
            {
                Console.Write("ERROR: ", Console.ForegroundColor = ConsoleColor.White);
                Console.WriteLine("Invalid input...", Console.ForegroundColor = ConsoleColor.Red);
                AUTHORIZE_USER();
            }
        }

        /*
            Authentication methods
        */
        public void REGISTER_USER()
        {
            Console.WriteLine("Switching to Registration...");

            try
            {
                // Display instructions to user
                Console.WriteLine("Enter username e.g John Doe >");
                string? Username = Console.ReadLine();

                Console.WriteLine("Enter email e.g johndoe@gmail.com >");
                string? Email = Console.ReadLine();

                Console.WriteLine("Enter password >");
                string? Password = Console.ReadLine();
                string HashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);

                if ((!string.IsNullOrWhiteSpace(Username)) && (!string.IsNullOrWhiteSpace(Email)))
                {
                    // Craete a new User
                    User user = new User()
                    {
                        UserName = Username,
                        Email = Email,
                        Password = HashedPassword,
                        Role = "user"
                    };

                    // Add user
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void LOGIN_USER()
        {
            Console.WriteLine("\nSwitching to Login...", Console.ForegroundColor = ConsoleColor.White);

            // Display instructions to user
            Console.WriteLine("* Enter username >");
            string? USER_NAME = Console.ReadLine();

            Console.WriteLine("* Enter password >");
            string? USER_PASSWORD = Console.ReadLine();

            // Validate input
            if (!string.IsNullOrWhiteSpace(USER_NAME) && !string.IsNullOrWhiteSpace(USER_PASSWORD))
            {
                try
                {
                    // Read user
                    User? user = context.Users.FirstOrDefault(u => u.UserName == USER_NAME);

                    // Validate password
                    if (user != null)
                    {
                        string STORED_PASSWORD = user.Password;
                        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(USER_PASSWORD, STORED_PASSWORD);

                        if (isPasswordValid)
                        {
                            Console.Write($"\nWelcome ", Console.ForegroundColor = ConsoleColor.Green);
                            Console.Write($"{user.UserName}", Console.ForegroundColor = ConsoleColor.White);

                            // Check user role
                            if (user.Role == "admin")
                            {
                                Console.WriteLine("\n\n<< Logged in as Admin >>", Console.ForegroundColor = ConsoleColor.Green);

                                // Display Admin Actions
                                UserActions.DISPLAY_ADMIN_ACTIONS();
                            }
                            else
                            {
                                Console.WriteLine("\n\n<< Logged in as User >>", Console.ForegroundColor = ConsoleColor.Green);

                                // Display User Actions
                                UserActions.DISPLAY_USER_ACTIONS();
                            }
                        }
                        else
                        {
                            Console.Write("Passwords do not match! ", Console.ForegroundColor = ConsoleColor.Red);
                            Console.Write("Try again...", Console.ForegroundColor = ConsoleColor.White);
                            LOGIN_USER();
                        }
                    }
                    else
                    {
                        Console.Write("User not found! ", Console.ForegroundColor = ConsoleColor.Red);
                        Console.Write("Try again...\n", Console.ForegroundColor = ConsoleColor.White);
                        LOGIN_USER();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                Console.Write("ERROR: ", Console.ForegroundColor = ConsoleColor.White);
                Console.WriteLine("Invalid input...", Console.ForegroundColor = ConsoleColor.Red);
                LOGIN_USER();
            }
        }
    }
}
