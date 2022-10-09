using NoteCraftModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteCraftMAUIBlazor.Services
{
    public interface IAuthenticationService
    {
        public UserAuthResponse UserAuthResponse { get; }

        Task<string> Login(UserAuthRequest userAuth);
        Task<string> Register(UserCreateDto newUser);
        void Logout();
    }
}
