﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCcompra.Modelo;
using TiendaServicios.Api.CarritoCcompra.Persistencia;

namespace TiendaServicios.Api.CarritoCcompra.Aplicacion
{
    public class Nuevo 
    {
        public class Ejecuta : IRequest
        {
            public DateTime FechaCreacionSesion { get; set; }
            public List<string> ProductoLista { get; set; }

        }


        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoCarrito _contexto;
            public Manejador(ContextoCarrito contexto)
            {
                _contexto = contexto;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = new CarritoSesion
                {
                    FechaCreacion = request.FechaCreacionSesion
                };

                _contexto.CarritoSesion.Add(carritoSesion);
                var value = await _contexto.SaveChangesAsync();

                if (value == 0) 
                {
                    throw new Exception("Error en la inserccion del carrito de compras");
                }

                int id = carritoSesion.CarritoSesionId;

                foreach (var item in request.ProductoLista)
                {
                    var detalleSesion = new CarritoSesionDetalle
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = id,
                        ProductoSeleccionado = item
                    };

                    _contexto.CarritoSesionDetalle.Add(detalleSesion);
                }

                var valueSesion = await _contexto.SaveChangesAsync();

                if(value > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar el detalle del carrito de compras");
            }
        }
    }
}
