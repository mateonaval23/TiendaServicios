using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCcompra.RemoteModel;

namespace TiendaServicios.Api.CarritoCcompra.RemoteInterface
{
    public interface ILibroService
    {
        Task<(bool resultado, LibroRemote libro, string ErrorMessage)> GetLibro(Guid LibroId);
    }
}
