using ControleDePedidos.Data.Context;
using ControleDePedidos.Data.Repositories.Interfaces;
using ControleDePedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDePedidos.Data.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly MeuDbContext _db;

    public ClienteRepository(MeuDbContext context)
    {
        _db = context;
    }

    public void Adicionar(ClienteEntity clienteEntity)
    {
        _db.Clientes.Add(clienteEntity);
    }

    public async Task<IEnumerable<ClienteEntity>> ListarClientes()
    {
        return await _db.Clientes.AsNoTracking().ToListAsync();
    }

    public void Remover(ClienteEntity clienteEntity)
    {
         _db.Clientes.Remove(clienteEntity); 
    }

    async Task<ClienteEntity> IClienteRepository.ObterPorId(Guid id)
    {
        return await _db.Clientes
            .Include(x => x.Pedidos)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    async Task<ClienteEntity> IClienteRepository.BuscarPorEmail(string email)
    {
        return await _db.Clientes.FirstOrDefaultAsync(e => e.Email == email);
    }

    public async Task Salvar()
    {
        await _db.SaveChangesAsync();
    }

    public void Dispose()
    {
        
    }
}
