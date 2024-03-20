using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthSystem.Domain.CountryAggregate;
using MediatR;
namespace AuthSystem.WebApp.Application.Country
{
    public class GetAll
    {
        public class Query : IRequest<Response> { }
        public class Response
        {
            public IList<Country> Countries { get; set; }

            public record Country
            {
                public Country(int id, string name)
                {
                    Id = id;
                    Name = name;
                }
                public int Id { get; set; }
                public string Name { get; set; }
            }
        }

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly ICountryRepository _repository;

            public Handler(ICountryRepository repository)
            {
                _repository = repository;
            }
            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var countries = await _repository.GetAllAsync(cancellationToken);
                return new Response { Countries = countries.Select(c => new Response.Country(c.Id, c.Name)).ToList() };
            }
        }
    }
}