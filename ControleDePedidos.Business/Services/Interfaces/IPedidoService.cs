using ControleDePedidos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDePedidos.Business.Services.Interfaces;

public interface IPedidoService : IDisposable
{
    Task<IEnumerable<Pedido>> ListarPedidos(Guid clienteId);
    Task<Pedido?> ObterPorId(Guid id);
    Task Adicionar (Pedido pedido);
    Task Atualizar (Pedido pedido);
    Task Remover (Guid id);
}
