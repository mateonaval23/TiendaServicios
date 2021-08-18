using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Modelo;

namespace TiendaServicios.Api.Libro.Persistencia
{
    public class ContextoLibreria : DbContext
    {
        // Agregar constructor por bug para el proyecto de test
        public ContextoLibreria() { }

        public ContextoLibreria(DbContextOptions<ContextoLibreria> options ) : base(options) { }

        // Se agrega virtual para sobreescribir a futuro en proyecto test con mock
        public virtual DbSet<LibreriaMaterial> LibreriaMaterial { get; set; }
    }
}
