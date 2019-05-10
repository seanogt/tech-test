using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.CustomExceptions
{
    public class InvalidOrderException : System.Exception
    {
        public InvalidOrderException()
        { }

        public InvalidOrderException(string message): base(message)
        { }
    }
}
