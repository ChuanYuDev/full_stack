namespace ConsoleApp1.Polymorphism;

public class Polymorphism
{
    public static void Main()
    {
        Dog myDog = new Dog();
        Cat myCat = new Cat();
        
        myDog.Eat();
        myCat.Eat();

        myDog.MakeSound();
        myCat.MakeSound();

        List<Animal> animals = new List<Animal>();
        animals.Add(new Dog());
        animals.Add(new Cat());

        foreach (Animal animal in animals)
        {
            animal.Eat();
            animal.MakeSound();
        }
    }
}