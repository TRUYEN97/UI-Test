using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTest.Exceptions
{
    public class FactoryClassTypeNotFoundException: Exception
    {
        public FactoryClassTypeNotFoundException(string message): base($"FactoryClass:{ message}") { }
        public FactoryClassTypeNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
