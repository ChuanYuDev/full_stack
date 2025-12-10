namespace ConsoleApp1.Test;

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
    public new void Eat()
    {
        Console.WriteLine("Dog eats");
    }

    public override void MakeSound()
    {
        Console.WriteLine("Bark");
    }
}

public class OverridingVsMethodHiding
{
    public static void Main()
    {
        Animal animal = new Animal();
        Animal dogAnimal = new Dog();
        Dog dog = new Dog();
        
        animal.Eat();           //Output: Animal eats
        animal.MakeSound();     //Output: Animal makes sound
        dogAnimal.Eat();        //Output: Animal eats
        dogAnimal.MakeSound();  //Output: Bark
        dog.Eat();              //Output: Dog eats
        dog.MakeSound();        //Output: Bark
    } 
}