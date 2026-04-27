using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDePedidos.Domain.Entities;

public class ClienteEntity
{
    public Guid Id { get; set; }

    public string? Nome { get; set; }

    public string? Email { get; set; }

    public IEnumerable<PedidoEntity>? Pedidos { get; set; }

    public ClienteEntity(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }

    public void Atualizar(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }
}
