using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using AuthSystem.Domain.CountryAggregate;

namespace AuthSystem.Infrastructure.Identity
{
    public sealed class User : IdentityUser
    {
        private User(){}
        public User(string login, Province province, bool isAgreeToWorkForFood)
        {
            UserName = Guard.Against.NullOrWhiteSpace(login, nameof(login));
            Email = login;
            Province = Guard.Against.Null(province, nameof(province)) ;
            IsAgreeToWorkForFood = Guard.Against.Default(isAgreeToWorkForFood, nameof(isAgreeToWorkForFood)) ;
        }
        public Province Province { get; private set; }
        public bool IsAgreeToWorkForFood { get; private set; }
    }
}