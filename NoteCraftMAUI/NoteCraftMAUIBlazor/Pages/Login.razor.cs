using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MongoDB.Bson.IO;
using Newtonsoft.Json;
using NoteCraftMAUIBlazor.Helpers;
using NoteCraftMAUIBlazor.Services;
using NoteCraftModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NoteCraftMAUIBlazor.Pages
{
    public partial class Login
    {
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        public UserAuthRequest UserAuth { get; set; } = new UserAuthRequest();

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private string spinnerClass = "";

        public async void LoginUser()
        {
            spinnerClass = "spinner-border spinner-border-sm";
            try
            {
                var response = await AuthenticationService.Login(UserAuth);
                if (!string.IsNullOrWhiteSpace(response))
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(response) as JwtSecurityToken;

                    string username = jsonToken.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Name).Value;

                    var userBasicDetails = new UserBasicDetails
                    {
                        Username = username,
                        Token = response
                    };
                    
                    string basicUserInfoStr = Newtonsoft.Json.JsonConvert.SerializeObject(userBasicDetails);
                    await SecureStorage.SetAsync(nameof(StaticInfo.UserBasicDetails), basicUserInfoStr);
                    StaticInfo.UserBasicDetails = userBasicDetails;

                    await App.Current.MainPage.DisplayAlert("Success", "Valid username and password!", "OK");
                    NavigationManager.NavigateTo("/");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Invalid username or password", "OK");
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Something went wrong, please try again", "OK");
                NavigationManager.NavigateTo("/login");
            }
            spinnerClass = "";
            this.StateHasChanged();
        }
    }
}
