using System;
using System.ComponentModel.DataAnnotations;
using ControleDePedidos.Business.Models;

namespace ControleDePedidos.ViewModels;

public class ClienteViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Nome e obrigatorio.")]
    [StringLength(120, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 120 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email e obrigatorio.")]
    [EmailAddress(ErrorMessage = "Email invalido.")]
    [StringLength(160, ErrorMessage = "Email deve ter no maximo 160 caracteres.")]
    public string Email { get; set; } = string.Empty;

    public Cliente ToModel()
    {
        return new Cliente
        {
            Id = Id,
            Nome = Nome.Trim(),
            Email = Email.Trim().ToLowerInvariant()
        };
    }

    public static ClienteViewModel FromModel(Cliente cliente)
    {
        return new ClienteViewModel
        {
            Id = cliente.Id,
            Nome = cliente.Nome ?? string.Empty,
            Email = cliente.Email ?? string.Empty
        };
    }
}
