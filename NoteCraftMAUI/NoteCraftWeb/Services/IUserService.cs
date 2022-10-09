using NoteCraftModels;

namespace NoteCraftWeb.Services
{
    public interface IUserService
    {
        Task<HttpResponseMessage> Login(UserAuthRequest userAuth);
        Task<HttpResponseMessage> Register(UserCreateDto newUser);
    }
}
