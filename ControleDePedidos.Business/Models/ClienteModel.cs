using ControleDePedidos.Domain.Entities;
using ControleDePedidos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDePedidos.Business.Models;

public class Cliente : Entity
{
    public Guid Id { get; set; }

    public string Nome { get; set; }

    public string Email { get; set; }
}

