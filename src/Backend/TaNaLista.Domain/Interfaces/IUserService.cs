using TaNaLista.Communication.Requests;
using TaNaLista.Communication.Response;
using TaNaLista.Domain.Models;

namespace TaNaLista.Domain.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> GetUserById(long id);
        Task<int> GetTotal();
        Task<IEnumerable<UserResponse>> GetAll(int page = 1, int pageSize = 10);
        Task<User> Create(UserCreateRequest request);
        Task<User> Update(long id, UserUpdateRequest request);
        Task<UserResponse> Delete(long id);
    }
}
