namespace ConsoleApp1.Test;

public class Tuple
{
    public static void Main()
    {
        (double, int) t1 = (4.5, 3);
        // Console.WriteLine(typeof(t1));
        Console.WriteLine($"Tuple with elements {t1.Item1} and {t1.Item2}.");
    }
}