using System;
using System.Collections.Generic;
using System.Text;

namespace MusicProjectDBCodeFirst.Data
{
    public class MyException : ApplicationException
    {
        public MyException(string message) : base(message) 
        {
            
        }
    }
}
