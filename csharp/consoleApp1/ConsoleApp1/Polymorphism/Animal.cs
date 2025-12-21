namespace ConsoleApp1.Polymorphism;
public class Animal
{
    public void Eat()
    {
        Console.WriteLine("Animal eats");
    }
    public virtual void MakeSound()
    {
        Console.WriteLine("Animal makes sound");
    }
}

public class Dog : Animal
{
    // Member hiding
    public new void Eat()
    {
        Console.WriteLine("Dog eats");
    }

    public override void MakeSound()
    {
        Console.WriteLine("Bark");
    }
}

public class Cat : Animal
{
    public new void Eat()
    {
        Console.WriteLine("Cat eats");
    }

    public override void MakeSound()
    {
        Console.WriteLine("Mewo");
    }
}
