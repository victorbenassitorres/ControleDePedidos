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
        try
        {
            var pedidos = await _pedidoService.ListarPedidos(clienteId);
            return Ok(pedidos);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        try
        {
            var pedidoId = await _pedidoService.ObterPorId(id);
            return Ok(pedidoId);
        }
        catch (NullException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] PedidoViewModel pedido)
    {
        if (pedido == null)
        {
            return BadRequest("Os dados do pedido são obrigatórios");
        }

        try
        {
            await _pedidoService.Adicionar(pedido.ToModel());
            return Ok(pedido);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] PedidoViewModel pedido)
    {
        if (pedido == null)
        {
            return BadRequest("Os dados do pedido são obrigatórios");
        }
        
        pedido.Id= id;

        try
        {
            await _pedidoService.Atualizar(pedido.ToModel());
            return StatusCode(StatusCodes.Status204NoContent, "Pedido atualizado com sucesso");
        }
        catch(ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remover(Guid id)
    {
        try
        {
            await _pedidoService.Remover(id);
            return StatusCode(StatusCodes.Status204NoContent, "Pedido removido com sucesso");
        }
        catch (NullException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}   