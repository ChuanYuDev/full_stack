namespace ConsoleApp1.Test;

public class ParamsModifier
{
    public static void ParamsModifierExample(params int[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            System.Console.Write(list[i] + " ");
        }
        System.Console.WriteLine();
    }

    public static void Main()
    {
        // You can send a comma-separated list of arguments of the specified type.
        ParamsModifierExample(1, 2, 3, 4);
        // Output: 1 2 3 4

        // A params parameter accepts zero or more arguments.
        // The following calling statement displays only a blank line.
        ParamsModifierExample();

        // An array argument can be passed, as long as the array
        // type matches the parameter type of the method being called.
        int[] myIntArray = { 5, 6, 7, 8, 9 };
        ParamsModifierExample(myIntArray);
        // Output: 5 6 7 8 9
    }
}