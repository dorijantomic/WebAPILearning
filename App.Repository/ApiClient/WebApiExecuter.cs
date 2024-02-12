using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace App.Repository.ApiClient
{
    public class WebApiExecuter : IWebApiExecuter
    {
        private readonly string baseUrl;
        private readonly HttpClient httpClient;

        public WebApiExecuter(string baseUrl, HttpClient httpClient)
        {
            this.baseUrl = baseUrl;
            this.httpClient = httpClient;

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> InvokeGet<T>(string uri)
        {
            return await httpClient.GetFromJsonAsync<T>(GetUrl(uri));
        }

        public async Task<T> InvokePost<T>(string uri, T obj)
        {
            var response = await httpClient.PostAsJsonAsync(GetUrl(uri), obj);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task InvokePut<T>(string uri, T obj)
        {
            var response = await httpClient.PutAsJsonAsync(GetUrl(uri), obj);
            response.EnsureSuccessStatusCode();
        }

        public async Task InvokeDelete<T>(string uri)
        {
            var response = await httpClient.DeleteAsync(GetUrl(uri));
            response.EnsureSuccessStatusCode();
        }

        private string GetUrl(string uri)
        {
            return $"{baseUrl}/{uri}";
        }
    }
}