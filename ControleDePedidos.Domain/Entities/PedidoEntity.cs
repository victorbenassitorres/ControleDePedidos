using System;
using System.Collections.Generic;
using System.Text;
using ControleDePedidos.Domain.Enums;

namespace ControleDePedidos.Domain.Entities;

public class PedidoEntity
{
    public Guid Id { get; set; }

    public string? ClienteNome { get; set; }

    public decimal Valor { get; set; }

    public int Status { get; set; }

    public Guid ClienteId { get; set; }

    public ClienteEntity? Cliente { get; set; }

    public PedidoEntity(string clienteNome, decimal valor, int status, Guid clienteId)
    {
        ClienteNome = clienteNome;
        Valor = valor;
        Status = status;
        ClienteId = clienteId;
    }
    
public void Atualizar(string clienteNome, decimal valor, int status, Guid clienteId)
    {
        ClienteNome = clienteNome;
        Valor = valor;
        Status = status;
        ClienteId = clienteId;
    }
}
