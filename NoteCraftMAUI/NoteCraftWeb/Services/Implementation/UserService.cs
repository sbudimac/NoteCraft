using NoteCraftModels;

namespace NoteCraftWeb.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;

        public UserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> Login(UserAuthRequest userAuth)
        {
            return await httpClient.PostAsJsonAsync("api/login", userAuth);
        }

        public async Task<HttpResponseMessage> Register(UserCreateDto newUser)
        {
            return await httpClient.PostAsJsonAsync("api/register", newUser);
        }
    }
}
