// See https://aka.ms/new-console-template for more information

public interface IAnimal
{
    void Eat();
}

public class Animal
{
    // public virtual void Eat()
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
    // public override void Eat()
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
    // public override void Eat()
    public new void Eat()
    {
        Console.WriteLine("Cat eats");
    }

    public override void MakeSound()
    {
        Console.WriteLine("Mewo");
    }
}

public class Program
{
    public static void Main()
    {
        Dog myDog = new Dog();
        Cat myCat = new Cat();

        myDog.MakeSound();
        myCat.MakeSound();

        myDog.Eat();
        myCat.Eat();

        List<Animal> animals = new List<Animal>();
        animals.Add(new Dog());
        animals.Add(new Cat());

        foreach (Animal animal in animals)
        {
            animal.MakeSound();
        }
    }
}