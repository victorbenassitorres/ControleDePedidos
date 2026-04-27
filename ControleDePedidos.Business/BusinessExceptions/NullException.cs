using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDePedidos.Business.BusinessException;

public class NullException : Exception
{
    public NullException(string message) : base(message) { }
}
