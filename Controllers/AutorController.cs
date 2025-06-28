using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tienda.Microservicios.Autor.Api.Aplicacion;

namespace Tienda.Microservicios.Autor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AutorController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<AutorDto>>> GetAutores()
        {
            return await _mediator.Send(new Consulta.ListaAutor());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AutorDto>> GetAutorLibro(string id)
        {
            return await _mediator.Send(new ConsultarFiltro.AutorUnico { AutorGuid = id });
        }

        [HttpGet("Nombre")]
        public async Task<ActionResult<List<AutorDto>>> FiltrarPorNombre([FromQuery] string nombre)
        {
            return await _mediator.Send(new ConsultaPorNombre.AutorPorNombre { Nombre = nombre });
        }


    }
}
