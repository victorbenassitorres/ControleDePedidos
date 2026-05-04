using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDePedidos.Business.BusinessExceptions;

public class NullException : Exception
{
    public NullException(string message) : base(message) { }
}
