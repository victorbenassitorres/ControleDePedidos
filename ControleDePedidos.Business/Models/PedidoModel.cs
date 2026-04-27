using ControleDePedidos.Business.Models;
using ControleDePedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace ControleDePedidos.Domain.Models;

public class Pedido : Entity
{
    public Guid Id { get; set; }

    public string ClienteNome { get; set; }

    public decimal Valor { get; set; }

    public int Status { get; set; }

    public Guid ClienteId { get; set; } 
}

