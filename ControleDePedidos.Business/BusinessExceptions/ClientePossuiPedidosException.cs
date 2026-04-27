using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDePedidos.Business.BusinessException;

public class ClientePossuiPedidosException : Exception
{
    public ClientePossuiPedidosException(string message) : base(message) { }
}
