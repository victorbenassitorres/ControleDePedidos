using System;
using System.Collections.Generic;
using System.Text;
using ControleDePedidos.Domain.Entities;

namespace ControleDePedidos.Data.Repositories.Interfaces;

public interface IClienteRepository 
{
    public Task<IEnumerable<ClienteEntity>> ListarClientes();

    public Task<ClienteEntity> ObterPorId(Guid id);

    public Task<ClienteEntity> BuscarPorEmail(string email);

    public void Adicionar(ClienteEntity clienteEntity);

    public void Remover(ClienteEntity clienteEntity);

    public Task Salvar();
    void Dispose();
}
