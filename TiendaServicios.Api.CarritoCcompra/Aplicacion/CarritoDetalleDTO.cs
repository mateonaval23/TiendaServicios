using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.CarritoCcompra.Aplicacion
{
    public class CarritoDetalleDTO
    {
        public Guid? LibroId { get; set; }
        public string TituloLibro { get; set; }
        public string AutorLibro { get; set; }
        public DateTime? FechaPublicacion { get; set; }
    }
}
