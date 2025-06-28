using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tienda.Microservicios.Autor.Api.Moledo;
using Tienda.Microservicios.Autor.Api.Persistencia;

namespace Tienda.Microservicios.Autor.Api.Aplicacion
{
    public class ConsultaPorNombre
    {
        public class AutorPorNombre : IRequest<List<AutorDto>>
        {
            public string Nombre { get; set; }
        }

        public class Manejador : IRequestHandler<AutorPorNombre, List<AutorDto>>
        {
            private readonly ContextoAutor _contexto;
            private readonly IMapper _mapper;

            public Manejador(ContextoAutor contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<List<AutorDto>> Handle(AutorPorNombre request, CancellationToken cancellationToken)
            {
                var autores = await _contexto.AutorLibros
                    .Where(a => a.Nombre.Contains(request.Nombre))
                    .ToListAsync();

                return _mapper.Map<List<AutorLibro>, List<AutorDto>>(autores);
            }
        }
    }
}
