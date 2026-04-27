using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDePedidos.Business.BusinessExceptions;

public class ClienteExisteException :Exception
{
    public ClienteExisteException(string message) : base(message) { }
}
