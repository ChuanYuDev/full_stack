namespace ConsoleApp1.Polymorphism;

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