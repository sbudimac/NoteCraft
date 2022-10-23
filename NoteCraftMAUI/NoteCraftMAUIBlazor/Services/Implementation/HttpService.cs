using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using NoteCraftModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Blazored.LocalStorage;
using NoteCraftMAUIBlazor.Helpers;

namespace NoteCraftMAUIBlazor.Services.Implementation
{
	public class HttpService : IHttpService
	{
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        public HttpService(HttpClient httpClient, NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.navigationManager = navigationManager;
        }

        public async Task<T> Get<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await sendRequest<T>(request);
        }

        public async Task<T> Post<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return await sendRequest<T>(request);
        }

        public async Task Post(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            await sendRequest(request);
        }

        public async Task<T> Put<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return await sendRequest<T>(request);
        }

        public async Task Delete(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            await sendRequest(request);
        }

        private async Task<T> sendRequest<T>(HttpRequestMessage request)
        {
            // add jwt auth header if user is logged in and request is to the api url
            var isApiUrl = !request.RequestUri.IsAbsoluteUri;
            if (StaticInfo.UserBasicDetails != null && isApiUrl)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", StaticInfo.UserBasicDetails.Token);

            // auto logout on 401 response
            using var response = await httpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                navigationManager.NavigateTo("/login");
                return default;
            }

            // throw exception on error response
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
                throw new Exception((string)error["message"]);
            }

            return await response.Content.ReadFromJsonAsync<T>();
        }

        private async Task sendRequest(HttpRequestMessage request)
        {
            var isApiUrl = !request.RequestUri.IsAbsoluteUri;
            if (StaticInfo.UserBasicDetails != null && isApiUrl)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", StaticInfo.UserBasicDetails.Token);

            // auto logout on 401 response
            using var response = await httpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                navigationManager.NavigateTo("/login");
                return;
            }

            // throw exception on error response
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
                throw new Exception((string)error["message"]);
            }

            return;
        }
    }
}
