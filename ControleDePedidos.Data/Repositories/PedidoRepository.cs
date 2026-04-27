using ControleDePedidos.Data.Context;
using ControleDePedidos.Data.Repositories.Interfaces;
using ControleDePedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDePedidos.Data.Repositories;
public class PedidoRepository : IPedidoRepository
{
    private readonly MeuDbContext _db;

    public PedidoRepository(MeuDbContext context)
    {
        _db = context;
    }

    public async Task Adicionar(PedidoEntity pedidoEntity)
    {
        await _db.Pedidos.AddAsync(pedidoEntity);
    }  

    async Task<PedidoEntity> IPedidoRepository.ObterPorId(Guid id)
    {
        return await _db.Pedidos.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<PedidoEntity>> ListarPedidos(Guid clienteId)
    {
        return await _db.Pedidos
            .AsNoTracking()
            .Where(p => p.ClienteId == clienteId)
            .ToListAsync();
    }

    public void Remover(PedidoEntity pedidoEntity)
    {
        _db.Pedidos.Remove(pedidoEntity);
    }

    public async Task Salvar()
    {
        await _db.SaveChangesAsync();
    }

    public void Dispose()
    {
        
    }
}
