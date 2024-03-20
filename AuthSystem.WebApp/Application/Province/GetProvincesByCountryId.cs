using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using AuthSystem.Domain.CountryAggregate;

namespace AuthSystem.WebApp.Application.Province
{
    public class GetProvincesByCountryId
    {
        public class Query : IRequest<Response>
        {
            public int CountryId { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(q => q.CountryId).NotEmpty();
            }
        }

        public class Response
        {
            public IList<Province> Provinces { get; set; }
            public record Province
            {
                public Province(int id, string name)
                {
                    Id = id;
                    Name = name;
                }

                public int Id { get; }
                public string Name { get; }
            }
        }

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly ICountryRepository _countryRepository;
            public Handler(ICountryRepository countryRepository)
            {
                _countryRepository = countryRepository;
            }
            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var provinces = await _countryRepository.GetProvincesByCountryIdAsync(request.CountryId, cancellationToken);

                return new Response
                { Provinces = provinces.Select(s => new Response.Province(s.Id, s.Name)).ToList() };
            }
        }
    }
}