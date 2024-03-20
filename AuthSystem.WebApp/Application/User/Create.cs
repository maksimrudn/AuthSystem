using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using AuthSystem.Domain.CountryAggregate;
using AuthSystem.Infrastructure.Identity;

namespace AuthSystem.WebApp.Application.User
{
    public class Create
    {
        public class Command : IRequest
        {
            public string Login { get; set; }

            public string Password { get; set; }

            public string ConfirmPassword { get; set; }

            public bool IsAgreeToWorkForFood { get; set; }

            public int CountryId { get; set; }

            public int ProvinceId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(command => command.Login).EmailAddress();
                RuleFor(command => command.Password).NotEmpty();
                RuleFor(command => command.IsAgreeToWorkForFood).NotEqual(false);
                RuleFor(command => command.ProvinceId).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly UserManager<AuthSystem.Infrastructure.Identity.User> _userManager;
            private readonly ICountryRepository _countryRepository;

            public Handler(UserManager<AuthSystem.Infrastructure.Identity.User> userManager, ICountryRepository countryRepository)
            {
                _userManager = userManager;
                _countryRepository = countryRepository;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var province = await _countryRepository.GetProvinceByIdAsync(request.ProvinceId, cancellationToken);
                var user = new AuthSystem.Infrastructure.Identity.User(request.Login, province, request.IsAgreeToWorkForFood);

                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                    return Unit.Value;

                var message = string.Join('|', result.Errors.Select(err => $"{err.Code} - {err.Description}"));
                throw new ApplicationException(message);
            }
        }
    }
}