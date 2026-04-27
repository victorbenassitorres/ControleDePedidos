using System;
using System.ComponentModel.DataAnnotations;
using ControleDePedidos.Business.Models;
using ControleDePedidos.Domain.Models;

namespace ControleDePedidos.ViewModels;

public class PedidoViewModel
{
    public Guid Id {get; set;}

    [Required(ErrorMessage = "O nome do pedido é obrigatório")]
    [StringLength(120, MinimumLength = 3, ErrorMessage = "O nome deve estar entre 3 a 120 caracteres")]
    public string ClienteNome {get; set;} = string.Empty;

    [Required(ErrorMessage  = "O valor do pedido é obrigatório")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
    public decimal Valor {get; set;}

    [Required(ErrorMessage = "O status do pedido é obrigatório")]
    [Range(0, 3, ErrorMessage = "O valor de status inserido é inválido")]
    public int Status {get; set;}

    //[Range(1, int.MaxValue, ErrorMessage = "O id está incorreto")]
    public Guid ClienteId{get; set;}

    public Pedido ToModel()
    {
        return new Pedido
        {
            Id = Id,
            ClienteNome = ClienteNome.Trim(),
            Valor = Valor,
            Status = Status,
            ClienteId = ClienteId
        };
    }

    public static PedidoViewModel FromModel(Pedido pedido)
    {
        return new PedidoViewModel
        {
            Id = pedido.Id,
            ClienteNome = pedido.ClienteNome,
            Valor = pedido.Valor,
            Status = pedido.Status,
            ClienteId = pedido.ClienteId
        };
    }
}    