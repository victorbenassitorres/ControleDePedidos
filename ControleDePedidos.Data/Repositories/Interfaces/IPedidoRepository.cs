using ControleDePedidos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDePedidos.Data.Repositories.Interfaces;

public interface IPedidoRepository
{
    public Task<IEnumerable<PedidoEntity>> ListarPedidos(Guid clienteId);

    public Task Adicionar(PedidoEntity pedidoEntity);

    public Task<PedidoEntity> ObterPorId(Guid id);

    public void Remover(PedidoEntity pedidoEntity);

    public Task Salvar();
    void Dispose();
}
