using Bogus;
using TaNaLista.Communication.Requests;
using TaNaLista.Domain.Models;

namespace TaNaLista.Test.CommonTestUtilities
{
    public class FakerUser
    {
        public static UserCreateRequest GenerateFakerUser()
        {
            var userFake = new Faker<UserCreateRequest>("pt_BR")
             //.RuleFor(u => u.Id, x => Guid.NewGuid())
             .RuleFor(u => u.Name, x => x.Person.FirstName)
             .RuleFor(u => u.LastName, x => x.Person.LastName)
             .RuleFor(u => u.Email, x => x.Internet.Email())
             .RuleFor(u => u.Password, x => x.Internet.Password())
             .Generate();


            return userFake;
        }

        public static User GenerateGetByIdFakerUser()
        {
            var userFake = new Faker<User>("pt_BR")
             .RuleFor(u => u.Name, x => x.Person.FirstName)
             .RuleFor(u => u.LastName, x => x.Person.LastName)
             .RuleFor(u => u.Email, x => x.Internet.Email())
             .RuleFor(u => u.Password, x => x.Internet.Password())
             .RuleFor(u => u.ShoppingLists, x => [])
             .Generate();
            return userFake;
        }
    }
}
