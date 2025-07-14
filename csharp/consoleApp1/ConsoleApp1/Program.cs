// See https://aka.ms/new-console-template for more information
using System.Drawing;
using Pastel;
using System;
using System.Globalization;

// Module 1
// Console.WriteLine("Hello, World!".Pastel(Color.Green));

// Module 2
// Method to divide two numbers
// public class Program
// {
//     public static double DivideNumbers(int numerator, int denominator)
//     {
//         if (denominator == 0)
//         {
//             Console.WriteLine("Error: Division by zero is not allowed.");
//             return double.NaN; // Return "Not a Number" to indicate an error
//         }

//         double result = numerator / denominator;
//         return result;
//     }

//     public static void Main()
//     {
//         // Attempt to divide 10 by 0
//         double result = DivideNumbers(10, 0);
//         Console.WriteLine("The result is: " + result);
//     }
// }

// Method to calculate the average of an array
// public class Program
// {
//     public static double CalculateAverage(int[] numbers)
//     {
//         if (numbers.Length == 0)
//         {
//             Console.WriteLine("Error: Cannot calculate the average of an empty array.");
//             return double.NaN; // Return "Not a number" to indicate an error
//         }

//         int sum = 0;
//         for (int i = 0; i < numbers.Length; i++)
//         {
//             sum += numbers[i];
//         }
//         return (double)sum / numbers.Length;
//     }

//     public static void Main()
//     {
//         int[] numbers = {}; // Empty array
//         double average = CalculateAverage(numbers);
//         Console.WriteLine("The average is: " + average);
//     }
// }

// Method to calculate the final price after a discount
// public class Program
// {
//     public static double ApplyDiscount(double price, double discountPercentage)
//     {
//         // return price - discountPercentage;
//         return price * (1 - discountPercentage / 100);
//     }

//     public static void Main()
//     {
//         double finalPrice = ApplyDiscount(1000, 15);
//         Console.WriteLine("The final price is: " + finalPrice);
//     }
// }

// public class Program
// {
//     public static int FindMax(int[] numbers)
//     {
//         // if (numbers.Length == 0)
//         // {
//         //     Console.WriteLine("The array is empty");
//         //     return 0;
//         // }

//         // int max = numbers[0];
//         int max = int.MinValue;

//         for (int i = 0; i < numbers.Length; i++)
//         {
//             if (numbers[i] > max)
//             {
//                 max = numbers[i];
//             }
//         }
//         return max;
//     }

//     public static void Main()
//     {
//         int[] myNumbers = { -5, -10, -3, -8, -2 };
//         int maxNumber = FindMax(myNumbers);
//         Console.WriteLine("The maximum number is: " + maxNumber);
//     }
// }

// public class Calculator
// {
//     public static int number1 = 10;
//     public static int number2 = 5;

//     public static int Add()
//     {
//         return number1 + number2;
//     }

//     public static void Main(string[] args)
//     {
//         Console.WriteLine("The sum is " + Add());
//     }

// }

// public class NumberDisplay
// {
//     public static void DisplayNumbers()
//     {
//         for (int i = 1; i <= 10; i++)
//         {
//             Console.WriteLine(i);
//         }
//     }

//     public static void Main(string[] args)
//     {
//         DisplayNumbers();
//     }
// }

// public class UserInput
// {
//     public static void GreetUser()
//     {
//         Console.WriteLine("Enter your name:");
//         string? name = Console.ReadLine();
//         Console.WriteLine("Hello " + name + "!");
//     }

//     public static void Main(string[] args)
//     {
//         GreetUser();
//     }
// }

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

// Module 3