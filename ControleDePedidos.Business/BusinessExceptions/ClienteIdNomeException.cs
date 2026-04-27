using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDePedidos.Business.BusinessExceptions;

public class ClienteIdNomeException : Exception
{
    public ClienteIdNomeException(string message) : base(message) { }
}
