using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NoteCraftModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteCraftMAUIBlazor.Services.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpService httpService;
        private readonly NavigationManager navigationManager;

        public UserAuthResponse UserAuthResponse { get; private set; }

        public AuthenticationService(IHttpService httpService, NavigationManager navigationManager)
        {
            this.httpService = httpService;
            this.navigationManager = navigationManager;
        }

        public async Task<string> Login(UserAuthRequest userAuth)
        {
            string returnStr = "";
            UserAuthResponse = await httpService.Post<UserAuthResponse>("api/user/login", userAuth);
            returnStr =  UserAuthResponse.Token;
            return returnStr;
        }

        public void Logout()
        {
            UserAuthResponse = null;
            navigationManager.NavigateTo("login");
        }

        public async Task<string> Register(UserCreateDto newUser)
        {
            string returnStr = "";
            UserAuthResponse = await httpService.Post<UserAuthResponse>("api/user/register", newUser);
            returnStr = UserAuthResponse.Token;
            return returnStr;
        }
    }
}
