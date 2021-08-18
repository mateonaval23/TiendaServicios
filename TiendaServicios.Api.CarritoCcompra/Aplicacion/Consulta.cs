using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCcompra.Persistencia;
using TiendaServicios.Api.CarritoCcompra.RemoteInterface;

namespace TiendaServicios.Api.CarritoCcompra.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta: IRequest<CarritoDTO>
        {
            public int CarritoSesionId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDTO>
        {
            private readonly ContextoCarrito _contexto;
            private readonly ILibroService _libroService;

            public Manejador(ContextoCarrito contexto, ILibroService libroService)
            {
                _contexto = contexto;
                _libroService = libroService;
            }
            public async Task<CarritoDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {

                var carritoSesion = await _contexto.CarritoSesion.FirstOrDefaultAsync(x => x.CarritoSesionId == request.CarritoSesionId);
                var carritoSesionDetalle = await _contexto.CarritoSesionDetalle.Where(x => x.CarritoSesionId == request.CarritoSesionId).ToListAsync();

                var listaCarritoDto = new List<CarritoDetalleDTO>();

                foreach (var libro in carritoSesionDetalle)
                {
                    var response = await _libroService.GetLibro(new Guid(libro.ProductoSeleccionado));
                    if (response.resultado) 
                    {
                        var objetoLibro = response.libro;
                        var carritoDetalle = new CarritoDetalleDTO
                        {
                             TituloLibro = objetoLibro.Titulo,
                             FechaPublicacion = objetoLibro.FechaPublicacion,
                             LibroId = objetoLibro.LibreriaMaterialId
                        };
                        listaCarritoDto.Add(carritoDetalle);
                    }
                }

                var carritoSesionDto = new CarritoDTO
                {
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaCreacion = carritoSesion.FechaCreacion,
                    ListaProducto = listaCarritoDto
                };

                return carritoSesionDto;

            }
        }
    }
}
