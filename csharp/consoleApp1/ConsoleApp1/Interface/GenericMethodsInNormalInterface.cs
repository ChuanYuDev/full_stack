namespace ConsoleApp1.Interface;

public interface ITest
{
    void Swap<T>(ref T lhs, ref T rhs);
}

public class Test : ITest
{
    public void Swap<T>(ref T lhs, ref T rhs)
    {
        T temp;
        temp = lhs;
        lhs = rhs;
        rhs = temp;
    }
}

public class GenericMethodsInNormalInterface
{
    public static void Main()
    {
        ITest test = new Test();
        int lhs = 5;
        int rhs = 10;
        Console.WriteLine($"lhs: {lhs}, rhs: {rhs}");
        test.Swap(ref lhs, ref rhs);
        Console.WriteLine($"lhs: {lhs}, rhs: {rhs}");
    }
}