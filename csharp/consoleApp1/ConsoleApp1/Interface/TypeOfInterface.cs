namespace ConsoleApp1.Interface;

public interface IPlayable
{
    void Play();
}

public class Guitar : IPlayable
{
    public void Play()
    {
        Console.WriteLine("The guitar is playing");
    }
}

public class TypeOfInterface
{
    public static void Main()
    {
        IPlayable play = new Guitar();
        play.Play();
        
        Console.WriteLine($"The Type of play is {play.GetType()}");
    }
}