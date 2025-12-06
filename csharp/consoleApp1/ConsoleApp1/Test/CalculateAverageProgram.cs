namespace ConsoleApp1.Test;

public class CalculateAverageProgram
{
    // Method to calculate the average of an array
    private static double CalculateAverage(int[] numbers)
    {
        if (numbers.Length == 0)
        {
            Console.WriteLine("Error: Cannot calculate the average of an empty array.");
            return double.NaN; // Return "Not a number" to indicate an error
        }

        int sum = 0;
        for (int i = 0; i < numbers.Length; i++)
        {
            sum += numbers[i];
        }
        return (double)sum / numbers.Length;
    }

    public static void Main()
    {
        int[] numbers = {}; // Empty array
        double average = CalculateAverage(numbers);
        Console.WriteLine("The average is: " + average);
    }
}