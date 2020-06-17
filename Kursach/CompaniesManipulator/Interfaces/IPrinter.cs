using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IPrinter
    {
        void Write(string str);
        string Read();
    }
}
