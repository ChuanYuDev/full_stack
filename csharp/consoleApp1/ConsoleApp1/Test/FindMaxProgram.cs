namespace ConsoleApp1.Test;

public class FindMaxProgram
{
    private static int FindMax(int[] numbers)
    {
        // if (numbers.Length == 0)
        // {
        //     Console.WriteLine("The array is empty");
        //     return 0;
        // }

        // int max = numbers[0];
        int max = int.MinValue;

        for (int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] > max)
            {
                max = numbers[i];
            }
        }
        return max;
    }

    public static void Main()
    {
        int[] myNumbers = { -5, -10, -3, -8, -2 };
        int maxNumber = FindMax(myNumbers);
        Console.WriteLine("The maximum number is: " + maxNumber);
    }
}