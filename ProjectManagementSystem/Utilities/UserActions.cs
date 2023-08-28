using ProjectManagementSystem.DbConfig;
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.Utilities
{
  /*
      This class encapsulates methods for displaying menus and handling user actions
      related to tasks, assignments, and project management.
  */
  public class UserActions
  {
    /*
        Default fields
    */
    static readonly Authorization authorization = new();
    static readonly ProjectManagementDbContext context = new();

    // Admin actions
    public static void DISPLAY_USER_ACTIONS(int rdx_USER_ID)
    {
      Console.WriteLine("* Displaying {{ User }} Actions: ", Console.ForegroundColor = ConsoleColor.White);

      // User Actions
      Console.WriteLine("1. View assigned tasks\n2. Mark task as completed\n");
      Console.WriteLine("* Select an option >");
      // Handle User Actions
      HANDLE_USER_ACTIONS(rdx_USER_ID);
    }

    // Handle User Actions
    public static void HANDLE_USER_ACTIONS(int rdx_USER_ID)
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
            DISPLAY_TASKS(rdx_USER_ID);
            break;
          case "2":
            UPDATE_TASK_STATUS(rdx_USER_ID);
            break;
          default:
            Console.Write("Ivalid option! ", Console.ForegroundColor = ConsoleColor.Red);
            Console.Write("Try again...", Console.ForegroundColor = ConsoleColor.White);
            // Return to login
            authorization.LOGIN_USER();
            break;
        }
      }
      else
      {
        Console.Write("Ivalid input! ", Console.ForegroundColor = ConsoleColor.Red);
        Console.Write("Try again...", Console.ForegroundColor = ConsoleColor.White);
        // Return to login
        authorization.LOGIN_USER();
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
      Console.WriteLine("5. Assign tasks");
      Console.WriteLine("6. View assigned tasks");

      // Handle User Actions
      HANDLE_ADMIN_ACTIONS();
    }

    // Handle Admin Actions
    public static void HANDLE_ADMIN_ACTIONS()
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
            VIEW_PROJECTS();
            break;
          case "2":
            CREATE_PROJECT();
            break;
          case "3":
            UPDATE_PROJECT();
            break;
          case "4":
            DELETE_PROJECT();
            break;
          case "5":
            ASSIGN_TASKS();
            break;
          case "6":
            VIEW_ASSIGNED_TASKS();
            break;
          default:
            Console.Write("Ivalid option! ", Console.ForegroundColor = ConsoleColor.Red);
            Console.Write("Try again...", Console.ForegroundColor = ConsoleColor.White);

            // Return to login
            authorization.LOGIN_USER();
            break;
        }
      }
      else
      {
        Console.Write("Ivalid option! ", Console.ForegroundColor = ConsoleColor.Red);
        Console.Write("Try again...", Console.ForegroundColor = ConsoleColor.White);

        // Return to login
        authorization.LOGIN_USER();
      }
    }

    // VIEW ASSIGNED TASKS
    public static void VIEW_ASSIGNED_TASKS()

    {
      var tasks = context.Tasks.Where(task => task.UserId != null).ToList();



      if (tasks.Count > 0)
      {
        foreach (var task in tasks)
        {
          Console.WriteLine($"Task ID: {task.AssignedTaskId}, Description: {task.TaskTitle}, Completed: {task.Completed}");
        }
      }

      else
      {
        Console.WriteLine("No tasks assigned.");
      }
    }

    /*****************************
        Admin Action Methods
    ****************************/

    // UPDATE TASK STATUS
    public static void UPDATE_TASK_STATUS(int rdx_USER_ID)
    {
      // Display tasks assigned to user
      DISPLAY_TASKS(rdx_USER_ID);

      Console.WriteLine("You're about to update a task's status...", Console.ForegroundColor = ConsoleColor.White);
      Console.WriteLine("\n1. Mark as complete\n2. Mark as incomplete", Console.ForegroundColor = ConsoleColor.White);

      // Read user input
      string? rdx_SELECTED_OPTION = Console.ReadLine();

      try
      {
        AssignedTask? task = context.Tasks.FirstOrDefault(u => u.UserId == rdx_USER_ID);

        if ((!string.IsNullOrWhiteSpace(rdx_SELECTED_OPTION)) && task != null)
        {
          int parsedInput = int.Parse(rdx_SELECTED_OPTION);

          if (parsedInput == 1)
          {
            // Update task status
            task.Completed = true;
            context.SaveChanges();
          }
          else if (parsedInput == 2)
          {
            // Update task status
            task.Completed = false;
            context.SaveChanges();
          }
          else
          {
            Console.Write("Ivalid input! ", Console.ForegroundColor = ConsoleColor.Red);
            Console.Write("Try again...", Console.ForegroundColor = ConsoleColor.White);
            // Return to login
            authorization.LOGIN_USER();
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred: {ex.Message}");
      }
    }
    // ASSIGN TASKS
    public static void ASSIGN_TASKS()
    {
      // Display existing projects
      VIEW_PROJECTS();

      // Display all users & their IDs
      DISPLAY_USERS();

      // Read user input
      // SELECT PROJECT ********************
      Console.WriteLine("Select project by ID e.g 1, 2, 3 etc...", Console.ForegroundColor = ConsoleColor.White);
      string? PROJECT_ID = Console.ReadLine();

      // SELECT USER ********************
      Console.WriteLine("Select user by ID e.g 1, 2, 3 etc...", Console.ForegroundColor = ConsoleColor.White);
      string? USER_ID = Console.ReadLine();

      // SELECT USER ********************
      Console.WriteLine("Enter task title >");
      string? TASK_TITLE = Console.ReadLine();

      try
      {
        // Validate input
        if ((!string.IsNullOrWhiteSpace(PROJECT_ID)) && (!string.IsNullOrWhiteSpace(USER_ID)) && (!string.IsNullOrWhiteSpace(TASK_TITLE)))
        {
          int projectId = int.Parse(PROJECT_ID);
          int userId = int.Parse(USER_ID);

          Project? project = context.Projects.FirstOrDefault(p => p.ProjectId == projectId);

          User? user = context.Users.FirstOrDefault(u => u.UserId == userId);

          // Check if project & user are null
          if (project != null && user != null)
          {
            // Create and assign a new task
            var newTask = new AssignedTask
            {
              TaskTitle = TASK_TITLE,
              ProjectId = projectId,
              UserId = userId
            };

            // Add changes to change tracker &
            // Mark changes for update
            context.Tasks.Add(newTask);
            context.SaveChanges();

            Console.WriteLine("Task added and assigned successfully.");
          }
          else
          {
            Console.WriteLine("Project or user not found.");
            ASSIGN_TASKS();
          }
        }
        else
        {
          Console.Write("Ivalid input! ", Console.ForegroundColor = ConsoleColor.Red);
          Console.Write("Try again...", Console.ForegroundColor = ConsoleColor.White);
          ASSIGN_TASKS();
        }

      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred: {ex.Message}");
      }
    }

    // DELETE A PROJECT
    public static void DELETE_PROJECT()
    {
      // Display existing projects
      VIEW_PROJECTS();

      // Read user input
      string? USER_INPUT = Console.ReadLine();
      Console.WriteLine($"Selected option: {USER_INPUT}", Console.ForegroundColor = ConsoleColor.White);

      try
      {
        // Validate input
        if (!string.IsNullOrWhiteSpace(USER_INPUT))
        {
          int parsedInput = int.Parse(USER_INPUT);

          // Project to update
          Project? project = context.Projects.FirstOrDefault(p => p.ProjectId == parsedInput);

          if (project != null)
          {
            Console.WriteLine($"Deleting project: {project.ProjectTitle}");

            context.Projects.Remove(project);

            // Mark changes for update
            context.SaveChanges();
          }
        }
        else
        {
          Console.Write("Ivalid input! ", Console.ForegroundColor = ConsoleColor.Red);
          Console.Write("Try again...", Console.ForegroundColor = ConsoleColor.White);
          UPDATE_PROJECT();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred: {ex.Message}");
      }
    }

    // UPDATE A PROJECT
    public static void UPDATE_PROJECT()
    {
      // Display existing projects
      VIEW_PROJECTS();

      // Read user input
      string? USER_INPUT = Console.ReadLine();
      Console.WriteLine($"Selected option: {USER_INPUT}", Console.ForegroundColor = ConsoleColor.White);

      try
      {
        // Validate input
        if (!string.IsNullOrWhiteSpace(USER_INPUT))
        {
          int parsedInput = int.Parse(USER_INPUT);

          // Project to update
          Project? project = context.Projects.FirstOrDefault(p => p.ProjectId == parsedInput);

          if (project != null)
          {
            Console.WriteLine($"Updating project: {project.ProjectTitle}");

            Console.WriteLine("Update project title >");
            string? UPDATED_projectTitle = Console.ReadLine();

            Console.WriteLine("Update project description >");
            string? UPDATED_projectDescription = Console.ReadLine();

            // Validate input
            if ((!string.IsNullOrWhiteSpace(UPDATED_projectTitle)) && (!string.IsNullOrWhiteSpace(UPDATED_projectDescription)))
            {
              project.ProjectTitle = UPDATED_projectTitle;
              project.ProjectDescription = UPDATED_projectDescription;
            }

            // Mark changes for update
            context.SaveChanges();
          }
        }
        else
        {
          Console.Write("Ivalid input! ", Console.ForegroundColor = ConsoleColor.Red);
          Console.Write("Try again...", Console.ForegroundColor = ConsoleColor.White);
          UPDATE_PROJECT();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred: {ex.Message}");
      }
    }

    // CREATE PROJECT
    public static void CREATE_PROJECT()
    {
      Console.WriteLine("\n* Enter project details: ", Console.ForegroundColor = ConsoleColor.White);

      try
      {
        // Read user input
        Console.WriteLine("* Enter project name >");
        string? projectTitle = Console.ReadLine();

        Console.WriteLine("* Enter project description >");
        string? projectDescription = Console.ReadLine();

        // Validate input
        if ((!string.IsNullOrWhiteSpace(projectTitle)) && (!string.IsNullOrWhiteSpace(projectDescription)))
        {
          Project project = new()
          {
            ProjectTitle = projectTitle,
            ProjectDescription = projectDescription
          };

          // Add new project to change tracker & 
          // Mark changes for update
          context.Projects.Add(project);
          context.SaveChanges();

          // Return to Action menu
          DISPLAY_ADMIN_ACTIONS();
        }
        else
        {
          Console.Write("ERROR: ", Console.ForegroundColor = ConsoleColor.White);
          Console.WriteLine("Invalid input...", Console.ForegroundColor = ConsoleColor.Red);
          CREATE_PROJECT();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred: {ex.Message}");
      }
    }

    // VIEW ALL PROJECTS
    public static void VIEW_PROJECTS()
    {
      Console.WriteLine("\n**** Displaying all projects...", Console.ForegroundColor = ConsoleColor.White);

      try
      {
        List<Project> projects = context.Projects.ToList();

        foreach (Project project in projects)
        {
          Console.WriteLine($"\n{project.ProjectId}. {project.ProjectTitle}", Console.ForegroundColor = ConsoleColor.White);
          Console.WriteLine($"\t{project.ProjectDescription}", Console.ForegroundColor = ConsoleColor.Yellow);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred: {ex.Message}");
      }
    }

    // DISPLAY TASKS ASSIGNED TO USER
    public static void DISPLAY_TASKS(int rdx_USER_ID)
    {
      // Display existing projects for ID reference
      VIEW_PROJECTS();

      Console.WriteLine("\n**** Displaying tasks assigned to user...", Console.ForegroundColor = ConsoleColor.White);

      try
      {
        List<AssignedTask> tasks = context.Tasks.Where(u => u.UserId == rdx_USER_ID).ToList();

        foreach (AssignedTask task in tasks)
        {
          Console.WriteLine($"\n{task.AssignedTaskId}. {task.TaskTitle}", Console.ForegroundColor = ConsoleColor.White);
          Console.WriteLine($"\tProject ID: {task.ProjectId}", Console.ForegroundColor = ConsoleColor.Yellow);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred: {ex.Message}");
      }
    }

    // DISPLAY ALL USERS
    public static void DISPLAY_USERS()
    {
      Console.WriteLine("\n**** Displaying all users...", Console.ForegroundColor = ConsoleColor.White);

      try
      {
        List<User> users = context.Users.Where(u => u.Role == "user").ToList();

        foreach (User user in users)
        {
          Console.WriteLine($"\n{user.UserId}. {user.UserName} >", Console.ForegroundColor = ConsoleColor.White);
          Console.WriteLine($"\t{user.Email}", Console.ForegroundColor = ConsoleColor.Yellow);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred: {ex.Message}");
      }
    }
  }
}