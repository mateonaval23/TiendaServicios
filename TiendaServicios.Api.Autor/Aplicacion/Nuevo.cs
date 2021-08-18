using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly ContextoAutor _contexto;

            public Manejador(ContextoAutor contextoAutor)
            {
                _contexto = contextoAutor;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                // Quiero insertar un nuevo autor en la base de datos: AutorLibro
                var autorLibro = new AutorLibro
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    FechaNacimiento = request.FechaNacimiento,
                    AutorLibroGuid = Convert.ToString(Guid.NewGuid())
                };

                // Agrego el objeto
                _contexto.AutorLibro.Add(autorLibro);

                // confirma persistencia y retorna la cantidad de transacciones que se ejecutaron
                var valor = await _contexto.SaveChangesAsync();

                // Si se genero el autor voy por el true
                if(valor > 0)
                {
                    return Unit.Value;
                }

                // Si hubo error disparo una excepcion
                throw new Exception("No se pudo insertar el autor del libro");
            }
        }
    }
}
