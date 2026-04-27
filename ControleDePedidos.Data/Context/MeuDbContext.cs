using ControleDePedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDePedidos.Data.Context;

public class MeuDbContext : DbContext
{
    public MeuDbContext(DbContextOptions options) : base(options) { }
    
    public DbSet<PedidoEntity> Pedidos { get; set; }

    public DbSet<ClienteEntity> Clientes { get; set; }
}
