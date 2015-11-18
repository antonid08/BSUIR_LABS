using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

class Program
{
    [DllImport("lib.dll")]
    public static extern void HelloWorldFunction();
    [DllImport("lib.dll", CallingConvention=CallingConvention.Cdecl)]
    public static extern int Add(int a, int b);
    
    [DllImport("winmm.dll")]
    extern static int mciSendString(string command, IntPtr responseBuffer, int bufferLength, int nothing);
     
    static void Main(string[] args)
    {
        HelloWorldFunction();
        int a = 5;
        int b = 10;
        Console.WriteLine("a + b = " + Add(a,b));

        Console.WriteLine("Input file name:");
        string fileName = Console.ReadLine();
        string command = string.Format("play D:\\" + fileName);
        IntPtr pt = Marshal.AllocHGlobal(0);
        mciSendString(command, pt, 0, 0);

        Console.WriteLine("Press any key to exit..");
        Console.ReadKey();

    }
}
