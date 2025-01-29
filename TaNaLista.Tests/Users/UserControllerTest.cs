using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using TaNaLista.Controllers;
using TaNaLista.Interfaces;
using TaNaLista.Models;
using TaNaLista.Requests;
using TaNaLista.Response;
using TaNaLista.Services;

namespace TaNaLista.Tests.Users
{
    public class UserControllerTest
    {
        //método GetUsers - reposta 200
        [Fact(DisplayName = "Retorna 200OK se GetUsers trazer lista de usuários")]
        public async Task GivenRequestWhenGetUsersThenShouldReturnOK()
        {
            var logger = new Mock<ILogger<User>>();
            var serviceMock = new Mock<IUserService>();

            serviceMock
                .Setup(x => x.GetAll(1, 10))
                .ReturnsAsync([new UserResponse { Id = Guid.NewGuid(), Name = "Name", LastName = "LastName", Email = "example@example.com" }]);
            //.ReturnsAsync(new List<UserResponse> { new UserResponse { Id = Guid.NewGuid(), Name = "Name", LastName = "LastName", Email = "example@example.com" } });


            var controller = new UsersController(serviceMock.Object, logger.Object);
            var result = await controller.GetUsers(1, 10);

            var resultResponseOk = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, resultResponseOk.StatusCode);
        }

        //método GetUsers - reposta 404
        [Fact(DisplayName = "Retorna 404NotFound se GetUsers trazer lista de usuários vazia ou nula")]
        public async Task GetUsers_WhenUserListIsEmptyOrNull_ShouldReturnNotFound()
        {
            var logger = new Mock<ILogger<User>>();
            var serviceMock = new Mock<IUserService>();

            serviceMock
                .Setup(service => service.GetAll(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync([]);

            var controller = new UsersController(serviceMock.Object, logger.Object);

            var result = await controller.GetUsers(1, 10);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        //método GetUser - reposta 200
        [Fact(DisplayName = "Retorna 200OK se GetUser receber um ID válido")]
        public async Task GivenValidIdWhenGetUserByIdThenShouldOK()
        {
            var contextMock = new Mock<IUserService>();
            var loggerMock = new Mock<ILogger<User>>();

            var userFake = UserFakeTest.GenerateGetByIdFakerUser();

            contextMock.
                Setup(x => x.GetUserById(userFake.Id))
                .ReturnsAsync(new UserResponse
                {
                    Id = userFake.Id,
                    Name = userFake.Name,
                    LastName = userFake.LastName,
                    Email = userFake.Email,
                    ShoppingLists = userFake.ShoppingLists?.Count ?? 0
                });

            var controller = new UsersController(contextMock.Object, loggerMock.Object);
            var result = await controller.GetUser(userFake.Id);

            Assert.IsType<OkObjectResult>(result);
        }

        //método GetUser - reposta 404
        [Fact(DisplayName = "Retorna 404NotFound se GetUser receber um ID inválido")]
        public async Task GivenInvalidIdWhenGetUserByIdThenShouldReturnNotFound()
        {
            var contextMock = new Mock<IUserService>();
            var loggerMock = new Mock<ILogger<User>>();
            var nonexistentId = Guid.NewGuid();

            contextMock
                .Setup(x => x.GetUserById(nonexistentId))
                .ReturnsAsync((UserResponse)null);

            var controller = new UsersController(contextMock.Object, loggerMock.Object);

            var result = await controller.GetUser(nonexistentId);

            Assert.IsType<NotFoundResult>(result);

        }

        //método Create - reposta 201
        [Fact(DisplayName = "Retorna 201Created caso crie usuário com dados válidos")]
        public async Task GivenValidDataWhenCreateUserWhenShouldReturnOk()
        {
            var serviceMock = new Mock<IUserService>();
            var logger = new Mock<ILogger<User>>();

            var userFake = UserFakeTest.GenerateFakerUser();
            var createdUser = new User
            {
                Id = Guid.NewGuid(),
                Name = userFake.Name,
                LastName = userFake.LastName,
                Email = userFake.Email,
                Password = userFake.Password
            };

            serviceMock
                .Setup(x => x.Create(It.IsAny<UserCreateRequest>())) // Qualquer(IsAny) UserCreateRequest é válido
                .ReturnsAsync(createdUser);

            var controller = new UsersController(serviceMock.Object, logger.Object);

            var result = await controller.PostUser(userFake);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(201, createdResult.StatusCode);

        }


        //método Create - reposta 400
        [Fact(DisplayName = "Retorna 400BadRequest caso crie usuário com dados inválidos")]
        public async Task GivenInvalidDataWhenCreateUserWhenShouldReturnBadRequest()
        {
            var loggerMock = new Mock<ILogger<User>>();
            var serviceMock = new Mock<IUserService>();
            var controller = new UsersController(serviceMock.Object, loggerMock.Object);

            controller.ModelState.AddModelError("Name", "Required");
            controller.ModelState.AddModelError("LastName", "Required");
            controller.ModelState.AddModelError("Email", "Email is invalid");
            controller.ModelState.AddModelError("Password", "Email must be min 8 characters");

            var invalidUserFake = UserFakeTest.GenerateInvalidFakerUserForCreate();


            var expectResult = await controller.PostUser(invalidUserFake);


            var badRequestResult = Assert.IsType<BadRequestObjectResult>(expectResult);
            var modelState = Assert.IsType<SerializableError>(badRequestResult.Value);
            Assert.Equal(4, modelState.Count);
        }

        //método Update - reposta 204
        [Fact(DisplayName = "Retorna 204NoContent caso atualize usuário com dados válidos")]
        public async Task GivenVaidDataWhenUpdateWhenShouldReturnNoContent()
        {
            var logger = new Mock<ILogger<User>>();
            var serviceMock = new Mock<IUserService>();

            var userFake = UserFakeTest.GenerateGetByIdFakerUser();

            var newUserFake = new UserUpdateRequest
            {
                Name = "NewName",
                LastName = "NewLastName",
                Email = "newexample@example.com",
                Password = "abcABC123.",
            };

            serviceMock
                .Setup(x => x.Update(userFake.Id, newUserFake))
                .ReturnsAsync(userFake);

            var controller = new UsersController(serviceMock.Object, logger.Object);
            var result = await controller.PutUser(userFake.Id, newUserFake);

            var statusResultOk = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, statusResultOk.StatusCode);

        }

        //método Update - reposta 400
        [Fact(DisplayName = "Retorna 400BadRequest caso atualize usuário com dados inválidos")]
        public async Task GivenInvalidDataWhenUpdateWhenShouldReturnBadRequest()
        {
            var logger = new Mock<ILogger<User>>();
            var serviceMock = new Mock<IUserService>();

            var controller = new UsersController(serviceMock.Object, logger.Object);

            controller.ModelState.AddModelError("Name", "Required");
            controller.ModelState.AddModelError("LastName", "Required");
            controller.ModelState.AddModelError("Email", "Email is invalid");
            controller.ModelState.AddModelError("Password", "Email must be min 8 characters");

            var invalidUserFake = UserFakeTest.GenerateInvalidFakerUserForUpdate();

            var expectResult = await controller.PutUser(Guid.NewGuid(), invalidUserFake);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(expectResult);
            var ModelState = Assert.IsType<SerializableError>(badRequestResult.Value);
            Assert.Equal(4, ModelState.Count);
        }


        //método Delete - reposta 200
        [Fact(DisplayName = "Retorna 200OK se Deletar user")]
        public async Task GivenValidUserWhenDeleteThenShouldReturnOk()
        {
            var logger = new Mock<ILogger<User>>();
            var serviceMock = new Mock<IUserService>();

            var userFake = UserFakeTest.GenerateGetByIdFakerUser();
            var controller = new UsersController(serviceMock.Object, logger.Object);

            serviceMock
                .Setup(x => x.Delete(userFake.Id))
                .ReturnsAsync((UserResponse)null);

            var result = await controller.DeleteUser(userFake.Id);

            var statusResultOk = Assert.IsType<OkResult>(result);
            Assert.Equal(200, statusResultOk.StatusCode);

        }


        //Chamada de método
        [Fact(DisplayName = "Chama serviço de criação de usuário apenas um vez")]
        public async Task GivenValidUserWhenCreateThenShouldBeCalledOnce()
        {
            var logger = new Mock<ILogger<User>>();
            var serviceMock = new Mock<IUserService>();

            var controller = new UsersController(serviceMock.Object, logger.Object);

            var userFake = UserFakeTest.GenerateFakerUser();

            var expectResult = await controller.PostUser(userFake);

            serviceMock.Verify(x => x.Create(It.Is<UserCreateRequest>(x => x == userFake)), Times.Once());


        }

    }

}