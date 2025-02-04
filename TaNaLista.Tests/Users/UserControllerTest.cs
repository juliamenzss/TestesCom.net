using System.Collections;
using System.Net;
using Bogus;
using Bogus.DataSets;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
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
        [Fact(DisplayName = "001 - Retorna 200OK se GetUsers trazer lista de usuários")]
        public async Task GivenRequestWhenGetUsersThenShouldReturnOK()
        {
            var page = Random.Shared.Next();
            var pageSize = Random.Shared.Next();

            var service = new Mock<IUserService>();
            var controller = new UsersController(service.Object, Mock.Of<ILogger<User>>());

            service
                .Setup(x => x.GetAll(page, pageSize))
                .ReturnsAsync([new UserResponse()]);

            var result = await controller.GetUsers(page, pageSize);

            Assert.IsType<OkObjectResult>(result);
            service.Verify(x => x.GetTotal(), Times.Once());

        }
        
        
        //método GetUsers - reposta 404
        [Fact(DisplayName = "002 - Retorna 404NotFound se GetUsers trazer lista de usuários vazia ou nula")]
        public async Task GetUsers_WhenUserListIsEmptyOrNull_ShouldReturnNotFound()
        {
            var page = Random.Shared.Next();
            var pageSize = Random.Shared.Next();

            var service = new Mock<IUserService>();
            var controller = new UsersController(service.Object, Mock.Of<ILogger<User>>());

            service
                .Setup(x => x.GetAll(page, pageSize));

            var result = await controller.GetUsers(page, pageSize);

            Assert.IsType<NotFoundObjectResult>(result);
            service.Verify(x => x.GetTotal(), Times.Never());
        }

        //método GetUser - reposta 200
        [Fact(DisplayName = "003 - Retorna 200OK se GetUser receber um ID válido")]
        public async Task GivenValidIdWhenGetUserByIdThenShouldOK()
        {
            var service = new Mock<IUserService>();
            var controller = new UsersController(service.Object, Mock.Of<ILogger<User>>());

            service.
                Setup(x => x.GetUserById(It.IsAny<Guid>()))
                .ReturnsAsync(new UserResponse());

            var result = await controller.GetUser(It.IsAny<Guid>());

            Assert.IsType<OkObjectResult>(result);
        }

        //método GetUser - reposta 404
        [Fact(DisplayName = "004 - Retorna 404NotFound se GetUser receber um ID inválido")]
        public async Task GivenInvalidIdWhenGetUserByIdThenShouldReturnNotFound()
        {
            var service = new Mock<IUserService>();
            var controller = new UsersController(service.Object, Mock.Of<ILogger<User>>());

            service.Setup(x => x.GetUserById(It.IsAny<Guid>()));

            var result = await controller.GetUser(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }

        //método Create - reposta 201
        [Fact(DisplayName = "005 - Retorna 201Created caso crie usuário com dados válidos")]
        public async Task GivenValidDataWhenCreateUserWhenShouldReturnOk()
        {
            var service = new Mock<IUserService>();
            var controller = new UsersController(service.Object, Mock.Of<ILogger<User>>());

            service
                .Setup(x => x.Create(It.IsAny<UserCreateRequest>())) 
                .ReturnsAsync(new User());

            var result = await controller.PostUser(new());

            Assert.IsType<CreatedAtActionResult>(result);
        }


        //método Create - reposta 400
        [Fact(DisplayName = "006 - Retorna 400BadRequest caso crie usuário com dados inválidos")]
        public async Task GivenInvalidDataWhenCreateUserWhenShouldReturnBadRequest()
        {
            var loggerMock = new Mock<ILogger<User>>();
            var serviceMock = new Mock<IUserService>();
            var controller = new UsersController(serviceMock.Object, loggerMock.Object);

            controller.ModelState.AddModelError("Name", "Required");
            controller.ModelState.AddModelError("LastName", "Required");
            controller.ModelState.AddModelError("Email", "Email is invalid");
            controller.ModelState.AddModelError("Password", "Email must be min 8 characters");

            var expectResult = await controller.PostUser(new());

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(expectResult);
            var modelState = Assert.IsType<SerializableError>(badRequestResult.Value);
            Assert.Equal(4, modelState.Count);
        }

        //método Update - reposta 204
        [Fact(DisplayName = "007 - Retorna 204NoContent caso atualize usuário com dados válidos")]
        public async Task GivenValidData_WhenUpdatingUser_ShouldReturnNoContent()
        {
            var service = new Mock<IUserService>();
            var controller = new UsersController(service.Object, Mock.Of<ILogger<User>>());

            service
            .Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<UserUpdateRequest>()))
            .ReturnsAsync(new User());

            var result = await controller.PutUser(Guid.NewGuid(), new());

            Assert.IsType<NoContentResult>(result);
        }


        //método Update - reposta 400
        [Fact(DisplayName = "008 - Retorna 400BadRequest caso atualize usuário com dados inválidos")]
        public async Task GivenInvalidDataWhenUpdateWhenShouldReturnBadRequest()
        {
            var logger = new Mock<ILogger<User>>();
            var serviceMock = new Mock<IUserService>();

            var controller = new UsersController(serviceMock.Object, logger.Object);

            controller.ModelState.AddModelError("Name", "Required");
            controller.ModelState.AddModelError("LastName", "Required");
            controller.ModelState.AddModelError("Email", "Email is invalid");
            controller.ModelState.AddModelError("Password", "Email must be min 8 characters");

            var expectResult = await controller.PutUser(Guid.NewGuid(), new());

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(expectResult);
            var ModelState = Assert.IsType<SerializableError>(badRequestResult.Value);
            Assert.Equal(4, ModelState.Count);
        }


        //método Delete - reposta 200
        [Fact(DisplayName = "009 - Retorna 200OK se Deletar user")]
        public async Task GivenValidUserWhenDeleteThenShouldReturnOk()
        {
            var service = new Mock<IUserService>();
            var controller = new UsersController(service.Object, Mock.Of<ILogger<User>>());

            service
                .Setup(x => x.Delete(It.IsAny<Guid>()));

            var result = await controller.DeleteUser(It.IsAny<Guid>());
            Assert.IsType<OkResult>(result);
        }


        //Chamada de método
        [Fact(DisplayName = "010 - Chama serviço de criação de usuário apenas um vez")]
        public async Task GivenValidUserWhenCreateThenShouldBeCalledOnce()
        {
            var service = new Mock<IUserService>();
            var controller = new UsersController(service.Object, Mock.Of<ILogger<User>>());

            var expectResult = await controller.PostUser(new());

            service.Verify(x => x.Create(It.IsAny<UserCreateRequest>()), Times.Once());


        }

    }

}