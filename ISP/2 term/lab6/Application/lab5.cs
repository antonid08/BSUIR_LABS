using System;
using System.Text;


public interface mySpecialization
{
    string getSpecialization();
}

abstract class Human : IEquatable<Human>, IComparable<Human>  
{	
	static string planet = "Earth";

    public int age { get; set; }

    public string name { get; set; }
    public string country { get ; set; }
    public string city { get; set; }

    /*Equals Realization*/
    public bool Equals(Human other)
    {
        if (other == null)
            return false;
        if (this.age == other.age && this.name == other.name && this.country == other.country &&
            this.city == other.city)
            return true;
        return false;
    }
   /***********/
    /* CompareTo realization (by age)*/
    public int CompareTo(Human second){
        if (age > second.age)
            return 1;
        if (age < second.age)
            return -1;
        return 0;
    }
    /************/
	public static string Planet 
	{
		get { return planet; } 
	}

	public void Print_Inf()
	{
		Console.WriteLine(age + "\n" + name + "\n" + country +
			              "\n" + city + "\n");
	}

	public void do_something()
	{
		Console.WriteLine("What?! I don't know what to do!");
	}

	public void do_something(string action)
	{
		Console.WriteLine("Ok! I'll " + action + "! :) \n");
	}

    abstract public void Go_to_work();

	
}


abstract class Student : Human, mySpecialization
{
    public Student(int age, string name, string country = "unknown", 
		          string city = "unknown")  
	{
		this.age = age;
		this.name = name;
		this.country = country;
		this.city = city;

        Console.WriteLine("\nHello!. My name is " + name + "! :)"); 
	}    
    abstract public void To_calculate_a_limit();
    abstract public override void Go_to_work();
    public virtual string getSpecialization()
    {
        return "I had not know yet.";
    }
}

class Ingeneer : Student
{

    public Ingeneer(int age, string name, string country = "unknown",
                  string city = "unknown")
        : base(age, name, country = "unknown", city = "unknown")
    {
    }
    public override void To_calculate_a_limit()
    {
        Console.WriteLine("Ok! No problem!");
    }

    public override void Go_to_work()
    {
        Console.WriteLine("I go to BSUIR because I am an ingeneer.");
 
    }

    public override string getSpecialization()
    {
        return "Ingeneer";
    }

}

class Economist : Student
{
    public Economist(int age, string name, string country = "unknown",
                  string city = "unknown")
        : base(age, name, country = "unknown", city = "unknown")
    {
    }

    public override void To_calculate_a_limit()
    {
        Console.WriteLine("Ok, Google. Calculate a limit for me.");
    }

    public override void Go_to_work()
    {
        
    }
    public override string getSpecialization()
    {
        return "Economist";
    }
}

class Teacher_of_Russian : Student
{
    public Teacher_of_Russian(int age, string name, string country = "unknown",
                  string city = "unknown")
        : base(age, name, country = "unknown", city = "unknown")
    {
    }

    public override void To_calculate_a_limit()
    {
        Console.WriteLine("Что ты написал? Я учитель русского!");
    }

    public override void Go_to_work()
    {
        Console.WriteLine("I go to BSPU because I am a teacher.");    
    }
    public override string getSpecialization()
    {
        return "russianTeacher";
    }
}


class Lab3
{
	public static void Main()
	{
		Console.WriteLine(Human.Planet);

        Console.WriteLine("The student lives on the planet the " + Student.Planet +
                           " because he is a human.");

        Student Ivan = new Ingeneer(17, "Ivan");
        Ivan.Go_to_work();

        Student Alex = new Economist(19, "Alex", "Belarus", "Minsk");
        Alex.Go_to_work();

        Student Egor = new Teacher_of_Russian(22, "Egor", city : "Pekin");
        Egor.Go_to_work();

        Console.WriteLine("\n" + Ivan.name + ", calculate a limit please.");
        Ivan.To_calculate_a_limit();
        Console.WriteLine("\n" + Alex.name + ", calculate a limit please.");
        Alex.To_calculate_a_limit();
        Console.WriteLine("\n" + Egor.name + ", calculate a limit please.");
        Egor.To_calculate_a_limit();

        Console.WriteLine("\n Ivan is " + Ivan.getSpecialization());

        Console.WriteLine("\n" + Ivan.Equals(Egor));
        Console.WriteLine("\n" + Ivan.CompareTo(Egor));

        Console.ReadKey();
	}		

}