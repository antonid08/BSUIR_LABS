using System;
using System.Text;

class Human 
{	
	static string planet = "Earth";

	int age;

	string name;
	string country;
	string city;
	
	public Human (int age, string name, string country = "unknown", 
		          string city = "unknown")  
	{
		this.age = age;
		this.name = name;
		this.country = country;
		this.city = city;
	}	

	public static string Planet 
	{
		get { return planet; } 
	}

	public int Age{

		get { return age; }
		set { age = value; }
	}

	public string Name 
	{
		get { return name; }
		set { name = value; }
	}

	public string Country 
	{
		get { return country; }
		set { country = value; }
	}

	public string City 
	{
		get { return city; }
		set { city = value; }
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

	
}

class Lab3
{
	public static void Main()
	{
		Console.WriteLine(Human.Planet);

		Human man = new Human(17, "Ilya");
		man.Print_Inf();

		Human second_man = new Human(24, "Alex", city: "Minsk");
		second_man.Print_Inf();

		Console.WriteLine("In what city there lives the man? ");
		man.City = Console.ReadLine();

		Console.WriteLine("In what country there lives the second_man? ");
		second_man.Country = Console.ReadLine();

		Console.WriteLine("\nNew information: \n");
		man.Print_Inf();
		second_man.Print_Inf();

		Console.WriteLine("Press any key to do nothing :)");
		Console.Read();
		Console.WriteLine();
		man.do_something();
		Console.WriteLine("");

		Console.WriteLine("What can the man do? ");
		man.do_something(Console.ReadLine());
	}		

}