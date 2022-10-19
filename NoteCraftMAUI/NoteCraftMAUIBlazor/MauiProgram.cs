using Microsoft.AspNetCore.Components.WebView.Maui;
using NoteCraftMAUIBlazor.Data;
using NoteCraftMAUIBlazor.Services;
using NoteCraftMAUIBlazor.Services.Implementation;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using NoteCraftMAUIBlazor.Settings;
using NoteCraftMAUIBlazor.Helpers;
using Blazored.LocalStorage;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.JSInterop;

namespace NoteCraftMAUIBlazor
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif
            builder.Services
                .AddScoped<IAuthenticationService, AuthenticationService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<INoteService, NoteService>()
                .AddScoped<IHttpService, HttpService>();

            builder.Services.AddScoped(x =>
            {
                var apiUrl = new Uri("https://localhost:7298/");
                return new HttpClient() { BaseAddress = apiUrl };
            });
            
            var host = builder.Build();
            var authenticationService = host.Services.GetRequiredService<IAuthenticationService>();

            return host;
        }
    }
}