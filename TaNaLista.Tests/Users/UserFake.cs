using Bogus;
using TaNaLista.Models;
using TaNaLista.Requests;
using TaNaLista.Response;

namespace TaNaLista.Tests.Users
{
    public class UserFakeTest
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
             .RuleFor(u => u.Id, x => Guid.NewGuid())
             .RuleFor(u => u.Name, x => x.Person.FirstName)
             .RuleFor(u => u.LastName, x => x.Person.LastName)
             .RuleFor(u => u.Email, x => x.Internet.Email())
             .RuleFor(u => u.Password, x => x.Internet.Password())
             .RuleFor(u => u.ShoppingLists, x => [])
             .Generate();
            return userFake;
        }

        //public static UserUpdateRequest GenerateUpdateFakerUser()
        //{
        //    var userFake = new Faker<UserUpdateRequest>("pt_BR")
        //     //.RuleFor(u => u.Id, x => Guid.NewGuid());
        //     .RuleFor(u => u.Name, x => x.Person.FirstName)
        //     .RuleFor(u => u.LastName, x => x.Person.LastName)
        //     .RuleFor(u => u.Email, x => x.Internet.Email())
        //     .RuleFor(u => u.Password, x => x.Internet.Password())
        //     //.RuleFor(u => u.ShoppingLists, x => new List<ShoppingList>())
        //     .Generate();
        //    return userFake;
        //}  

        public static UserCreateRequest GenerateInvalidFakerUserForCreate()
        {
            var userInvalidFake = new Faker<UserCreateRequest>("pt_BR")
             .RuleFor(u => u.Name, x => "")
             .RuleFor(u => u.LastName, x => "abc")
             .RuleFor(u => u.Email, x => "invalid-email")
             .RuleFor(u => u.Password, x => "123456")
             .Generate();

            return userInvalidFake;
        }

        public static UserUpdateRequest GenerateInvalidFakerUserForUpdate()
        {
            var userInvalidFake = new Faker<UserUpdateRequest>("pt_BR")
             .RuleFor(u => u.Name, x => "")
             .RuleFor(u => u.LastName, x => "abc")
             .RuleFor(u => u.Email, x => "invalid-email")
             .RuleFor(u => u.Password, x => "123456")
             .Generate();

            return userInvalidFake;
        }
    }
}
