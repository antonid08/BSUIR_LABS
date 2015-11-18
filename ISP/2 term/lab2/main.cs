using System;
using System.Text;
using System.Globalization;

class Lab2
{	
	public static string MyReverse(string str) 
	{	
		/*
		на вход подается строка. на выход новая строка. никаких принтф в функции.
		*/
		string[] str2 = new string[10];
		
		int counter = 0;

		char[] arr = str.ToCharArray();

		StringBuilder new_str_b = new StringBuilder("", 300); // ВОТ КЛАСС СТРИНГ БИЛДЕР ( ПРО НЕГО В МЕТОДЕ "БАЗОВЫЕ ТЕХНОЛОГИИ ASP.NET
			// вообще, почитай там про строки")

		for (int i = 0; i < str.Length; ++i) {
			str2[counter] += arr[i];
			if (arr[i] == ' ') {
				++counter;
			} 
		}
		str2[counter] += " ";

		for (int current = counter; current >= 0; --current) {
			new_str_b.Append(str2[current]);;
		}

		string reversed_str = new_str_b.ToString(); 

		return reversed_str;
	}

	public static string FindUpper(string str) 
	{	
		StringBuilder new_str_b = new StringBuilder("", 300);

		for (int current = 0; current < str.Length; ++current) {
			if (char.IsUpper(str[current])) {
				if (str[current] >= 'A' && str[current] <= 'Z') continue;
				new_str_b.Append(str[current]);
			}
		}
		

		string only_not_english_upper = new_str_b.ToString();

		return only_not_english_upper;
	}

	public static void Month()
	{	
		string month;

		var culture = new CultureInfo("fr-Fr");
		
		Console.Write("\n");

		for (int i = 0; i < 12; ++i) {
			month = DateTime.Today.AddMonths(i).ToString("MMMM", culture);
			Console.WriteLine(month);
		}
	}

	public static void Main()
	{	
		int choose = 0;
		string str;

		do	 {
		Console.WriteLine ("\nНажмите: ");
		Console.WriteLine ("1 - поменять порядок слов на обратный.");
		Console.WriteLine ("2 - найти заглавные буквы, не входящие в английский алфавит.");
		Console.WriteLine ("3 - вывести 12 месяцев на французском языке.");
		Console.WriteLine ("4 - выйти из программы.");

		choose = Console.Read();

		switch (choose)
			{
				case '1' :
				Console.WriteLine("\nВведите строку: ");
				str = Console.ReadLine(); 
				Console.WriteLine(MyReverse(str));
				break;

				case '2' :
				Console.WriteLine("\nВведите строку: ");
				str = Console.ReadLine(); 
				Console.WriteLine(FindUpper(str));
				break;

				case '3' : 
				Month();
				break;

				case '4' :
				Console.WriteLine("\nРабота программы завершена!\n");
				break;

				default :
				Console.WriteLine ("\nВведите корректный вариант!\n");
				break;
			}
		} while (choose != '4');
		
 	}
}