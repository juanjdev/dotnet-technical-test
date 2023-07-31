using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaIngresoBibliotecario.Domain.Exceptions
{
    public class PrestamoException : Exception
    {
        public PrestamoException(string message): base(message) { }
    }
}
