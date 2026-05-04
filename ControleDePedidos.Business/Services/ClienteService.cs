using ControleDePedidos.Business.Models;
using ControleDePedidos.Business.Services.Interfaces;
using ControleDePedidos.Data.Repositories.Interfaces;
using ControleDePedidos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using ControleDePedidos.Business.BusinessExceptions;

namespace ControleDePedidos.Business.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IPedidoRepository _pedidoRepository;

    public ClienteService(IClienteRepository clienteRepository, IPedidoRepository pedidoRepository)
    {
        _clienteRepository = clienteRepository;
        _pedidoRepository = pedidoRepository;
    }

    public async Task<IEnumerable<Cliente>> ListarClientes()
    {
        var entidades = await _clienteRepository.ListarClientes();
        
        return entidades.Select(c => new Cliente 
        {
           Id = c.Id,
           Nome = c.Nome,
           Email = c.Email
        });
    
    }    
    public async Task<Cliente> ObterPorId(Guid id)
    {
        var entidade = await _clienteRepository.ObterPorId(id);
    
        if (entidade == null) return null;

        return new Cliente
        {
            Id = entidade.Id,
            Nome = entidade.Nome,
            Email = entidade.Email
        };
    }
    
    public async Task<Cliente> BuscarPorEmail(string email)
    {
        var entidade = await _clienteRepository.BuscarPorEmail(email);

        if (entidade == null) return null;

        return new Cliente
        {
            Id = entidade.Id,
            Nome = entidade.Nome,
            Email = entidade.Email
        };
    }

    public async Task Adicionar(Cliente cliente)
    {
        var existente = await _clienteRepository.BuscarPorEmail(cliente.Email);
        if (existente != null)
        {
            throw new ClienteExisteException("Esse cliente já existe");
        }

        ClienteEntity clienteEntity = new ClienteEntity(
            cliente.Nome,
            cliente.Email);

        _clienteRepository.Adicionar(clienteEntity);

        await _clienteRepository.Salvar();
    }

    public async Task Atualizar(Cliente cliente)
    {
        var clienteEntity = await _clienteRepository.ObterPorId(cliente.Id);

        if (clienteEntity != null)
        {
            clienteEntity.Atualizar(
                cliente.Nome,
                cliente.Email);

            await _clienteRepository.Salvar();

            
        }
        
        if (clienteEntity == null)
        {
            throw new NullException("O cliente solicitado não existe");
        }

    }

    public void Dispose()
    {
        _clienteRepository?.Dispose();
    }

    public async Task Remover(Guid id)
    {
        var cliente = await _clienteRepository.ObterPorId(id);

        if (cliente == null)
        {
            throw new NullException("O cliente solicitado não existe");
        }

        if (cliente.Pedidos.Any() == true)
        {
            throw new ClientePossuiPedidosException("O cliente possui pedidos cadastrados");
        }

       _clienteRepository.Remover(cliente);
       await _clienteRepository.Salvar();
    }

    

}
