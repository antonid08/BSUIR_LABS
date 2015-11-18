using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

delegate uint del(uint a);

class RationalNumber
{
    public int N { get; set; }
    public uint M { get; set; }
    public string str { get; set; }
    
    public RationalNumber()
    {
    }

    public RationalNumber(int n, uint m)
    {
        this.N = n;
        this.M = m;
    }

    static public uint noException(uint x)
    {
        Console.WriteLine("\nError. Need to set M to 1");
        Console.WriteLine("Done");
        return x;
    }
    public static RationalNumber reduction(RationalNumber initial)
    {
        RationalNumber result = new RationalNumber(initial.N, initial.M);
        for (int divider = 2; divider <= result.M; divider++)
        {
            if (result.N % divider == 0 &&  result.M % divider == 0)
            {
                result.N /= divider;
                result.M /= Convert.ToUInt32(divider);
                return reduction(result);
                    break;
            }
        }
        return result;
    }

    public override string ToString()
    {
        return string.Format("{0}/{1}", N, M);
    }
    
    public static RationalNumber operator +(RationalNumber a, RationalNumber b)
    {
        RationalNumber result = new RationalNumber();
        result.M = a.M * b.M;
        result.N = a.N * Convert.ToInt32(b.M) + b.N * Convert.ToInt32(a.M);
        return reduction(result);
    }

    public static RationalNumber operator -(RationalNumber a, RationalNumber b)
    {
        RationalNumber result = new RationalNumber();
        result.M = a.M * b.M;
        result.N = a.N * Convert.ToInt32(b.M) - b.N * Convert.ToInt32(a.M);
        return reduction(result);
    }

    public static RationalNumber operator *(RationalNumber a, RationalNumber b)
    {
        RationalNumber result = new RationalNumber();
        result.M = a.M * b.M;
        result.N = a.N * b.N;
        return reduction(result);
    }

    public static RationalNumber operator /(RationalNumber a, RationalNumber b)
    {
        RationalNumber result = new RationalNumber();
        result.M = a.M * Convert.ToUInt32(b.N);
        result.N = a.N * Convert.ToInt32(b.M);
        return reduction(result);
    }

    public static RationalNumber createFromString(string initialString)
    {
        Regex slash = new Regex("/");
        string[] two_numbers = slash.Split(initialString);
        RationalNumber result = new RationalNumber(Convert.ToInt32(two_numbers[0]),
                                                   Convert.ToUInt32(two_numbers[1]));
        return result;
    }

    public void print()
    {
        Console.WriteLine( N + "/" + M);
    }
}

class Program
{
    static void Main(string[] args)
    {
        del firstDel = new del(RationalNumber.noException);
        del secondDel = delegate(uint a)
        {   
            return a;
        };

        Func<uint, uint> anonM = RationalNumber.noException;


        RationalNumber first = RationalNumber.createFromString(Console.ReadLine());
        RationalNumber second = new RationalNumber(20, 0);

        Console.Write("Input first number: ");
        try
        {
            int i = first.N / Convert.ToInt32(first.M);
        }
        catch (DivideByZeroException)
        {   
            first.M = firstDel(1);
            //first.M = anonM(1);
        }

        try
        {
            int i = second.N / Convert.ToInt32(second.M);
        }
        catch (DivideByZeroException)
        {
            second.M = secondDel(1);
        }


        Console.WriteLine("\nFirst number: " + first.ToString());
        Console.WriteLine("Second number: " + second.ToString());
 

        Console.WriteLine(first.ToString()+ " + " + second.ToString()+ " = " + (first + second).ToString());
        Console.WriteLine(first.ToString() + " - " + second.ToString() + " = " + (first - second).ToString());
        Console.WriteLine(first.ToString() + " * " + second.ToString() + " = " + (first * second).ToString());
        Console.WriteLine(first.ToString() + " / " + second.ToString() + " = " + (first / second).ToString());
        
        Console.Write("Press any key to exit...");
        Console.ReadKey();
    }
}

