using ControleDePedidos.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDePedidos.Business.Services.Interfaces;

public interface IClienteService : IDisposable
{
    Task<IEnumerable<Cliente>> ListarClientes();
    Task<Cliente?> ObterPorId(Guid id);
    Task<Cliente?> BuscarPorEmail(string email);
    Task Adicionar (Cliente cliente);
    Task Atualizar (Cliente cliente);
    Task Remover (Guid id); 
}
