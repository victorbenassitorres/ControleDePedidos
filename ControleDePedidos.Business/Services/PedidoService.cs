using ControleDePedidos.Data.Repositories.Interfaces;
using ControleDePedidos.Business.Services.Interfaces;
using ControleDePedidos.Domain.Entities;
using ControleDePedidos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using ControleDePedidos.Business.BusinessExceptions;

namespace ControleDePedidos.Business.Services;

public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IClienteRepository _clienteRepository;

    public PedidoService(IPedidoRepository pedidoRepository, IClienteRepository clienteRepository)      
    {
        _pedidoRepository = pedidoRepository;
        _clienteRepository = clienteRepository;
    }

    public async Task<IEnumerable<Pedido>> ListarPedidos(Guid clienteId)
    {
        var entidades = await _pedidoRepository.ListarPedidos(clienteId);

        return entidades.Select(c => new Pedido
        {
            Id = c.Id,
            ClienteNome = c.ClienteNome,
            Valor = c.Valor,
            Status = c.Status,
            ClienteId = c.ClienteId
        });
    }

    public async Task<Pedido?> ObterPorId(Guid id)
    {
        var entidade = await _pedidoRepository.ObterPorId(id);
 
        if (entidade == null) return null;

        return new Pedido
        {
            Id = entidade.Id,
            ClienteNome = entidade.ClienteNome,
            Valor = entidade.Valor,
            Status = entidade.Status,
            ClienteId = entidade.ClienteId
        };
    }

    public async Task Adicionar(Pedido pedido)
    {
        var cliente = await _clienteRepository.ObterPorId(pedido.ClienteId);
        if (cliente == null)
        {
            throw new NullException("O Id de cliente cadastrado não existe");
        }

        PedidoEntity pedidoEntity = new PedidoEntity(
            pedido.ClienteNome,
            pedido.Valor,
            pedido.Status,
            pedido.ClienteId);
        
        if (pedido.ClienteNome != cliente.Nome || pedido.ClienteId != cliente.Id)
        {
            throw new ClienteIdNomeException("O nome do cliente não corresponde ao Id informado");
        }

        if (pedidoEntity != null)
        {
            await _pedidoRepository.Adicionar(pedidoEntity);

            await _pedidoRepository.Salvar();

            return;
        }
    
        
    }
    
    public async Task Atualizar(Pedido pedido)
    {
        var cliente = await _clienteRepository.ObterPorId(pedido.ClienteId);
        if (cliente == null)
        {
            throw new NullException("O Id de cliente cadastrado não existe");
        }

        var pedidoEntity = await _pedidoRepository.ObterPorId(pedido.Id);

        if (pedidoEntity != null)
        {
            pedidoEntity.Atualizar(
                pedido.ClienteNome,
                pedido.Valor,
                pedido.Status,
                pedido.ClienteId
            );

            await _pedidoRepository.Salvar();
        }
    
        if (pedidoEntity == null)
        {
            throw new NullException("O pedido solicitado não existe");
        }
    }

    public async Task Remover(Guid id)
    {
        var pedido = await _pedidoRepository.ObterPorId(id);
    
        if (pedido == null)
        {
            throw new NullException("O pedido solicitado não existe");
        }
    
        _pedidoRepository.Remover(pedido);
        await _pedidoRepository.Salvar();
    }

    public void Dispose()
    {
        _pedidoRepository?.Dispose();
    }
}
      
