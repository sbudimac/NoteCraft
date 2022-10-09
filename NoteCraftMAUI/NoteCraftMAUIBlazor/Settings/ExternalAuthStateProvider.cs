using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NoteCraftMAUIBlazor.Settings
{
	public class ExternalAuthStateProvider : AuthenticationStateProvider
    {
        private readonly Task<AuthenticationState> authenticationState;

        public ExternalAuthStateProvider(AuthenticatedUser user) =>
            authenticationState = Task.FromResult(new AuthenticationState(user.Principal));

        public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
            authenticationState;
    }

    public class AuthenticatedUser
    {
        public ClaimsPrincipal Principal { get; set; } = new();
    }
}
