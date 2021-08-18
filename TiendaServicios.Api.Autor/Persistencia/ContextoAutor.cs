using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Modelo;

namespace TiendaServicios.Api.Autor.Persistencia
{
    public class ContextoAutor : DbContext // Representa a EntityFrameworkCore
    {
        
        // Esto sirve para que arranque la instacia cuando se ejecute el startup
        public ContextoAutor(DbContextOptions<ContextoAutor> options) : base(options) { }


        // Clases de tipo entidad para mapeo de tablas con el mismo nombre
        public DbSet<AutorLibro> AutorLibro { get; set; }
        public DbSet<GradoAcademico> GradoAcademico { get; set; }

    }
}
