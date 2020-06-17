using System;
using System.Collections.Generic;
using System.Text;
using Application.Interfaces;

namespace Application.Printers
{
    public class ConsolePrinter : IPrinter
    {
        public void Write(string str)
        {
            Console.WriteLine(str);
        }

        public string Read()
        {
            return Console.ReadLine();
        }
    }
}
