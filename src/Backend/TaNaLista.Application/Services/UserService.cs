using Microsoft.EntityFrameworkCore;
using TaNaLista.Communication.Requests;
using TaNaLista.Communication.Response;
using TaNaLista.Domain.Interfaces;
using TaNaLista.Domain.Models;

namespace TaNaLista.Application.Services
{
    public class UserService
        (IUserContext context) : IUserService
    {
        public readonly IUserContext _context = context;


        public async Task<IEnumerable<UserResponse>> GetAll(int page = 1, int pageSize = 10)
        {
            var user = await _context.Users
               .OrderBy(x => x.Id)
               .Select(x => new UserResponse
               {
                   Id = x.Id,
                   Name = x.Name,
                   LastName = x.LastName,
                   Email = x.Email,
                   ShoppingLists = x.ShoppingLists.Count
               })
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();

            return user;
        }

        public async Task<int> GetTotal()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<UserResponse> GetUserById(long id)
        {

            var user = await _context.Users
                .Where(x => x.Id == id)
                .Select(x => new UserResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    LastName = x.LastName,
                    Email = x.Email,
                    ShoppingLists = x.ShoppingLists.Count
                })
                .SingleOrDefaultAsync();

            if (user == null)
            {
                return null!;
            }

            return user;
        }

        public async Task<User> Create(UserCreateRequest request)
        {
            var newUser = new User
            {
                Name = request.Name,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
            };

            context.Users.Add(newUser);
            await context.SaveChangesAsync();
            return newUser;
        }


        public async Task<User> Update(long id, UserUpdateRequest request)
        {

            var user = await context.Users
                    .SingleOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return null!;
            }

            user.Name = request.Name;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.Password = request.Password;

            await context.SaveChangesAsync();

            return user;
        }


        public async Task<UserResponse> Delete(long id)
        {
            await context.Users.Where(x => x.Id == id).ExecuteDeleteAsync();
            return null!;
        }
    }
}