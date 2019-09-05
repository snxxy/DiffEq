using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Generator
{
    sealed class EntryPoint
    {
        static void Main(string[] args)
        {
            ConsoleMenu cm = new ConsoleMenu();
            cm.InitMenu();
        }
    }
}
