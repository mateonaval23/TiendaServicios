﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCcompra.Modelo;

namespace TiendaServicios.Api.CarritoCcompra.Persistencia
{
    public class ContextoCarrito : DbContext
    {
        public ContextoCarrito(DbContextOptions<ContextoCarrito> options) : base(options) { }

        public DbSet<CarritoSesion> CarritoSesion { get; set; }
        public DbSet<CarritoSesionDetalle> CarritoSesionDetalle { get; set; }
    }
}
