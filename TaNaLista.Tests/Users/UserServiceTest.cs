using System.Xml.Linq;
using Moq;
using Moq.EntityFrameworkCore;
using TaNaLista.Interfaces;
using TaNaLista.Models;
using TaNaLista.Requests;
using TaNaLista.Response;
using TaNaLista.Services;

namespace TaNaLista.Tests.Users
{
    public class UserServiceTest
    {
        [Fact(DisplayName = "Retorna um usuário válido se existir")]
        public async Task GivenValidIdWhenGetUserByIdThenShouldReturnUser()
        {
            var contextMock = new Mock<IUserContext>();

            var fakeUserResult = UserFakeTest.GenerateGetByIdFakerUser();

            contextMock
                .Setup(x => x.Users)
                .ReturnsDbSet(new List<User> { fakeUserResult }.AsQueryable());

            var service = new UserServiceDB(contextMock.Object);

            var expectResult = await service.GetUserById(fakeUserResult.Id);

            Assert.NotNull(expectResult);
            Assert.Equal(expectResult.Id, fakeUserResult.Id);
            Assert.Equal(expectResult.Name, fakeUserResult.Name);
            Assert.Equal(expectResult.Email, fakeUserResult.Email);
        }

        [Fact(DisplayName = "Retorna a lista de usuarios paginada")]
        public async Task GivenPaginationWhenGetAllThenShouldReturnPaginateUsers()
        {
            var contextMock = new Mock<IUserContext>();
            var fakeUsers = new List<User>
            {
                new() {Id = Guid.NewGuid(), Name = "Name",LastName = "LastName", Email = "email@email.com", Password = "abcABC123.@", ShoppingLists = [new ShoppingList()] },
                new() {Id = Guid.NewGuid(), Name = "Name2",LastName = "LastName2", Email = "gmail@gmail.com", Password = "abcABC123.@", ShoppingLists = [new ShoppingList()]},
            };

            contextMock
                .Setup(x => x.Users)
                .ReturnsDbSet(fakeUsers);

            var service = new UserServiceDB(contextMock.Object);

            var expectResult = await service.GetAll(1, 10);

            Assert.NotEmpty(expectResult);
            Assert.NotNull(expectResult);
            Assert.Equal(2, expectResult.Count());
            Assert.Contains(expectResult, x => x.Name == "Name");
        }

        [Fact(DisplayName = "Retorna user criado se dados forem válidos")]
        public async Task GivenValidDataWhenCreateThenShouldReturnCreatedUser()
        {
            var contextMock = new Mock<IUserContext>();

            var userFake = new UserCreateRequest
            {
                Name = "NameTeste",
                LastName = "LastNameTeste",
                Email = "email@email.com",
                Password = "passwordD123.",
            };

            contextMock
                .Setup(x => x.Users)
                .ReturnsDbSet([]);

            var service = new UserServiceDB(contextMock.Object);

            var expectResult = await service.Create(userFake);

            Assert.NotNull(expectResult);

            Assert.Equal(expectResult.Name, userFake.Name);
            Assert.Equal(expectResult.LastName, userFake.LastName);
            Assert.Equal(expectResult.Email, userFake.Email);
        }

        //[Fact(DisplayName = "Retorna mensagem se id não for válido em delete")] // teste inválido porque na regra de negógio volta null caso receba id inválido
        //public async Task GivenInvalidIdWhenDeleteThenShouldReturnMessage()
        //{
        //    var contextMock = new Mock<IUserContext>();
        //    var service = new UserServiceDB(contextMock.Object);

        //    var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.Delete(Guid.Empty));

        //    Assert.Equal("UserId is not valid", exception.Message);
        //}

    }

}
