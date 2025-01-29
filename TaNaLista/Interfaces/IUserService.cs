using Microsoft.AspNetCore.Mvc;
using TaNaLista.Models;
using TaNaLista.Requests;
using TaNaLista.Response;

namespace TaNaLista.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> GetUserById(Guid id);
        Task<int> GetTotal();
        Task<IEnumerable<UserResponse>> GetAll(int page = 1, int pageSize = 10);
        Task<User> Create(UserCreateRequest request);
        Task<User> Update(Guid id, UserUpdateRequest request);
        Task<UserResponse> Delete(Guid id);
    }
}
