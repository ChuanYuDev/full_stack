namespace ConsoleApp1.Test;

public class ToDoList
{
    public static string?[] tasks = new string[10];
    public static int taskCount = 0;

    public static void AddTask()
    {
        Console.WriteLine("Enter a task:");
        tasks[taskCount] = Console.ReadLine();
        taskCount++;
    }

    public static void ViewTasks()
    {
        for (int i = 0; i < taskCount; i++)
        {
            Console.WriteLine(i + 1 + "." + tasks[i]);
        }
    }

    public static void CompleteTask()
    {
        Console.WriteLine("Select a task to mark as complete:");
        string taskNumber = Console.ReadLine() ?? "0";

        Console.WriteLine("The task number is " + taskNumber);

        int taskIndex;

        try
        {
            taskIndex = int.Parse(taskNumber) - 1;
        }
        catch (Exception)
        {
            Console.WriteLine("Invalid task number format");
            return;
        }

        if (taskIndex >= 0 && taskIndex < taskCount)
        {
            tasks[taskIndex] = tasks[taskIndex] + "(Completed)";
            Console.WriteLine("Task marked as complete.");
        }
        else
        {
            Console.WriteLine("Invalid task number.");
        }
    }

    public static void Main()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Add a task");
            Console.WriteLine("2. View tasks");
            Console.WriteLine("3. Mark a task as complete");
            Console.WriteLine("4. Exit");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;

                case "2":
                    ViewTasks();
                    break;

                case "3":
                    CompleteTask();
                    break;

                case "4":
                    running = false; 
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again");
                    break;
            }
        }
    }
}