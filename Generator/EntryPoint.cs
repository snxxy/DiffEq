using System;

namespace DiffEq
{
    [Obsolete]
    sealed class EntryPoint
    {
        static void Main(string[] args)
        {
            ConsoleMenu cm = new ConsoleMenu();
            cm.InitMenu();
            Console.ReadKey();
        }
    }
}
