namespace ConsoleApp1.Test;

public class DivideNumbersProgram
{
    private static double DivideNumbers(int numerator, int denominator)
    {
        if (denominator == 0)
        {
            Console.WriteLine("Error: Division by zero is not allowed.");
            return double.NaN; // Return "Not a Number" to indicate an error
        }

        double result = (double) numerator / denominator;
        return result;
    }

    public static void Main()
    {
        // Attempt to divide 10 by 0
        double result = DivideNumbers(10, 0);
        Console.WriteLine("The result is: " + result);
        
        result = DivideNumbers(10, 3);
        Console.WriteLine("The result is: " + result);
    }
}