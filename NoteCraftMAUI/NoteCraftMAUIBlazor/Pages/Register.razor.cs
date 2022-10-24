using Microsoft.AspNetCore.Components;
using NoteCraftMAUIBlazor.Services;
using NoteCraftModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NoteCraftMAUIBlazor.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NoteCraftMAUIBlazor.Pages
{
    public partial class Register
    {
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        public UserCreateDto NewUser { get; set; } = new UserCreateDto();

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private string spinnerClass = "";

        public async void RegisterUser()
        {
            spinnerClass = "spinner-border spinner-border-sm";
            try
            {
                var response = await AuthenticationService.Register(NewUser);
                if (!string.IsNullOrWhiteSpace(response))
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(response) as JwtSecurityToken;

                    string id = jsonToken.Claims.FirstOrDefault(f => f.Type == ClaimTypes.NameIdentifier).Value;
                    string username = jsonToken.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Name).Value;

                    var userBasicDetails = new UserBasicDetails
                    {
                        Id = id,
                        Username = username,
                        Token = response
                    };

                    string basicUserInfoStr = Newtonsoft.Json.JsonConvert.SerializeObject(userBasicDetails);
                    await SecureStorage.SetAsync(nameof(StaticInfo.UserBasicDetails), basicUserInfoStr);
                    StaticInfo.UserBasicDetails = userBasicDetails;

                    await App.Current.MainPage.DisplayAlert("Success", "Valid user information provided!", "OK");
                    NavigationManager.NavigateTo("/");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Invalid user information provided", "OK");
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Something went wrong, please try again", "OK");
                NavigationManager.NavigateTo("/register");
            }
            spinnerClass = "";
            this.StateHasChanged();
        }
    }
}
