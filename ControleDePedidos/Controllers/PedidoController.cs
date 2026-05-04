using ControleDePedidos.Business.BusinessException;
using ControleDePedidos.Business.BusinessExceptions;
using ControleDePedidos.Business.Models;
using ControleDePedidos.Business.Services.Interfaces;
using ControleDePedidos.Domain.Models;
using ControleDePedidos.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ControleDePedidos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidoController : ControllerBase
{
    private readonly IPedidoService _pedidoService;

    public PedidoController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpGet("pedido/{clienteId}")]
    public async Task<IActionResult> ListarPedidos(Guid clienteId)
    {
        var pedidos = await _pedidoService.ListarPedidos(clienteId);
        return Ok(pedidos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var pedidoId = await _pedidoService.ObterPorId(id);
        return Ok(pedidoId);
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] PedidoViewModel pedido)
    {
        if (pedido == null)
        {
            return BadRequest("Os dados do pedido são obrigatórios");
        }

        await _pedidoService.Adicionar(pedido.ToModel());
        return Ok(pedido);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] PedidoViewModel pedido)
    {
        if (pedido == null)
        {
            return BadRequest("Os dados do pedido são obrigatórios");
        }
        
        pedido.Id= id;

        await _pedidoService.Atualizar(pedido.ToModel());
        return StatusCode(StatusCodes.Status204NoContent, "Pedido atualizado com sucesso");
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remover(Guid id)
    {
        await _pedidoService.Remover(id);
        return StatusCode(StatusCodes.Status204NoContent, "Pedido removido com sucesso");
    }
}   