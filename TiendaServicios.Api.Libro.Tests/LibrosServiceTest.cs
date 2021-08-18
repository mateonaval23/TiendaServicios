
using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiendaServicios.Api.Libro.Aplicacion;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;
using Xunit;

namespace TiendaServicios.Api.Libro.Tests
{
    public class LibrosServiceTest
    {

        private IEnumerable<LibreriaMaterial> obtenerDataPrueba()
        {

            //Uso Genfu para esto
            A.Configure<LibreriaMaterial>()
                .Fill(x => x.Titulo).AsArticleTitle()  //Genero un titulo aleatorio
                .Fill(x => x.LibreriaMaterialId, () => { return Guid.NewGuid(); });

            // Genera 30 objetos aleatorios
            var list = A.ListOf<LibreriaMaterial>(30);
            list[0].LibreriaMaterialId = Guid.Empty;

            return list;
        }

        private Mock<ContextoLibreria> CrearContexto()
        {
            var dataPrueba = obtenerDataPrueba().AsQueryable();

            var dbSet = new Mock<DbSet<LibreriaMaterial>>();
            //Indicamos libreria material es de tipo entidad y darle los elementos para que sea entidad con las propiedades
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(dataPrueba.Provider);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Expression).Returns(dataPrueba.Expression);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.ElementType).Returns(dataPrueba.ElementType);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.GetEnumerator()).Returns(dataPrueba.GetEnumerator());

            dbSet.As<IAsyncEnumerable<LibreriaMaterial>>().Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<LibreriaMaterial>(dataPrueba.GetEnumerator()));

            var contexto = new Mock<ContextoLibreria>();
            contexto.Setup(x => x.LibreriaMaterial).Returns(dbSet.Object);

            return contexto;
        }

        [Fact]
        public async void GetLibros()
        {
            // Para poder debuggear el test
            System.Diagnostics.Debugger.Launch();
            // Que metodo microservice libro se encarga de realizar la consulta de libros a la DB

            // 1 - Emular a la instancia de EF Core - ContextoLibreria
            // para emular las acciones y eventos de un objeto en un ambiente de unit test utilizamos objetos de tipo mock
            var mockContexto = CrearContexto();


            // 2 - Emular al mapping IMapper
            var mapConfigTest = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new MappingTest());
            });

            var mapper = mapConfigTest.CreateMapper();

            //3 - Instanciar a la clase manejador
            Consulta.Manejador manejador = new Consulta.Manejador(mockContexto.Object, mapper);

            Consulta.Ejecuta request = new Consulta.Ejecuta();

            var lista = await manejador.Handle(request, new System.Threading.CancellationToken());


            //Condicion de test

            Assert.True(lista.Any());

        }
    }
}
