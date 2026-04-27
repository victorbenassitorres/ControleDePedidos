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
        try 
        {
           var clientes = await _clienteService.ListarClientes();
           return Ok(clientes);
        }
        
        catch
        {
            return NotFound();
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        try
        {
            var clientesId = await _clienteService.ObterPorId(id);
            return Ok(clientesId);
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
    
    [HttpGet("email/{email}")]
    public async Task<IActionResult> BuscarPorEmail(string email)
    {
        if (email == null)
        {
            return BadRequest("O e-mail é obrigatório");
        }

        try
        {
            var buscaPorEmail = await _clienteService.BuscarPorEmail(email);
            return Ok(buscaPorEmail);
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
    public async Task<IActionResult> Adicionar([FromBody] ClienteViewModel cliente)
    {
        if (cliente == null)
        {
            return BadRequest("Os dados do cliente sao obrigatorios.");
        }

        try
        {
            await _clienteService.Adicionar(cliente.ToModel());
            return StatusCode(StatusCodes.Status201Created, "Cliente cadastrado com sucesso.");
        }
        catch (ClienteExisteException ex)
        {
            return Conflict(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] ClienteViewModel cliente)
    {
        if (cliente == null)
        {
            return BadRequest("Os dados do cliente sao obrigatorios.");
        }

        cliente.Id = id;

        try
        {
            await _clienteService.Atualizar(cliente.ToModel());
            return StatusCode(StatusCodes.Status204NoContent, "Cliente atualizado com sucesso"); 
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

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remover(Guid id)
    {
        try
        {
            await _clienteService.Remover(id);
            return StatusCode(StatusCodes.Status204NoContent, "Cliente removido com sucesso");
        }
        catch (NullException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ClientePossuiPedidosException ex)
        {
            return Conflict(ex.Message);
        }
    }
}
