using Microsoft.AspNetCore.Components;
using NoteCraftMAUIBlazor.Services;
using NoteCraftModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
                if (!string.IsNullOrEmpty(response))
                {
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
                NavigationManager.NavigateTo("/register");
            }
            spinnerClass = "";
            this.StateHasChanged();
        }
    }
}
