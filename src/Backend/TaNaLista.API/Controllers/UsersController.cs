using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaNaLista.Communication.Requests;
using TaNaLista.Communication.Response;
using TaNaLista.Domain.Interfaces;
using TaNaLista.Domain.Models;

namespace TaNaLista.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService service, ILogger<User> logger) : ControllerBase
    {
        private readonly IUserService _service = service ?? throw new ArgumentNullException(nameof(service));
        private readonly ILogger<User> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        [SwaggerOperation(Summary = "Return a list of Users")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserResponse>))]
        [HttpGet]
        public async Task<IActionResult> GetUsers(int page = 1, int pagesize = 10)
        {
            var users = await _service.GetAll(page, pagesize);

            if (users == null || !users.Any())
            {
                return NotFound("Users not found");
            }
            var result = new PaginateResultResponse<UserResponse>
            {
                CurrentPage = page,
                PageSize = pagesize,
                TotalItems = await _service.GetTotal(),
                Items = users
            };
            return Ok(result);
        }


        [SwaggerOperation("Get a user by id")]
        [ProducesResponseType<User>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(long id)
        {
            var user = await _service.GetUserById(id);

            if (user == null)
            {
                _logger.LogWarning("User is null");
                return NotFound();
            }

            return Ok(user);
        }


        [SwaggerOperation("Create a new user")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> PostUser(UserCreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _service.Create(request);
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch (DbException dbEx)
            {
                _logger.LogError(dbEx, "Error in database");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error create user");
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation("Update user by id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutUser(long id, UserUpdateRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = await _service.Update(id, request);

                if (user == null)
                {
                    _logger.LogWarning("User is null");
                    return NotFound();
                }

                return NoContent();

            }
            catch (DbException dbEx)
            {
                _logger.LogError(dbEx, "Error in database");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error update user");
                return BadRequest();
            }
        }


        [HttpDelete("{id}")]
        [SwaggerOperation("Delete user by id")]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        public async Task<IActionResult> DeleteUser(long id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}
