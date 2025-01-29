//using TaNaLista.Interfaces;
//using TaNaLista.Response;

//namespace TaNaLista.Services
//{
//    public class UserService(IUserRepository repository)
//    {
//        private readonly IUserRepository _repository = repository;
    

//    public async Task<UserResponse?> GetUserById(Guid id)
//        {
//            var user = await _repository.GetUserById(id);

//            if(user == null)
//            {
//                return null;
//            }
//            return new UserResponse
//            {
//                Id = user.Id,
//                Name = user.Name,
//                LastName = user.LastName,
//                Email = user.Email
//            };
//        }
//    }
//}
