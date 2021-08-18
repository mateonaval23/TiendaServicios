using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<LibreriaMaterialDTO>>
        {
            public Ejecuta() { }
        }

        public class Manejador : IRequestHandler<Ejecuta, List<LibreriaMaterialDTO>>
        {
            private readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;

            public Manejador(ContextoLibreria contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }
            public async Task<List<LibreriaMaterialDTO>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libros = await _contexto.LibreriaMaterial.ToListAsync();

                if (libros == null)
                {
                    throw new Exception("No se encontro el autor");
                }

                // Tipo de dato origne, tipo de dato a convertir, elemento a convertir
                var librosDto = _mapper.Map<List<LibreriaMaterial>, List<LibreriaMaterialDTO>>(libros);

                return librosDto;
            }
        }
    }
}
