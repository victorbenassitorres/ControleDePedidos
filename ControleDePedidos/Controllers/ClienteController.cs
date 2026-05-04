using Microsoft.AspNetCore.Mvc;
using ControleDePedidos.Business.BusinessException;
using ControleDePedidos.Business.BusinessExceptions;
using ControleDePedidos.Business.Models;
using ControleDePedidos.Business.Services.Interfaces;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using ControleDePedidos.ViewModels;

namespace ControleDePedidos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet]
    public async Task<IActionResult> ListarCliente()
    {
        var clientes = await _clienteService.ListarClientes();
        return Ok(clientes);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var clientesId = await _clienteService.ObterPorId(id);
        return Ok(clientesId);
    }
    
    [HttpGet("email/{email}")]
    public async Task<IActionResult> BuscarPorEmail(string email)
    {
        if (email == null)
        {
            return BadRequest("O e-mail é obrigatório");
        }

        var buscaPorEmail = await _clienteService.BuscarPorEmail(email);
        return Ok(buscaPorEmail);
    }
    

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] ClienteViewModel cliente)
    {
        if (cliente == null)
        {
            return BadRequest("Os dados do cliente sao obrigatorios.");
        }

        await _clienteService.Adicionar(cliente.ToModel());
        return StatusCode(StatusCodes.Status201Created, "Cliente cadastrado com sucesso.");

    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] ClienteViewModel cliente)
    {
        if (cliente == null)
        {
            return BadRequest("Os dados do cliente sao obrigatorios.");
        }

        cliente.Id = id;

        await _clienteService.Atualizar(cliente.ToModel());
        return StatusCode(StatusCodes.Status204NoContent, "Cliente atualizado com sucesso"); 
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remover(Guid id)
    {   
        await _clienteService.Remover(id);
        return StatusCode(StatusCodes.Status204NoContent, "Cliente removido com sucesso");
    }
}
