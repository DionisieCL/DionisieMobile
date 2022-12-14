using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Schoolager.Prism.Models;
using Xamarin.Essentials;

namespace Schoolager.Prism.Services
{
    public class ApiServicescs : IApiServices
    {
 
        public async Task<Response>GetListAsync<T>(
           string urlBase,
           string servicePrefix,
           string controller)
        {
            try
            {
                /*var client = new HttpClient
                {
                    BaseAddress = new Uri($"{urlBase}"),
                };

                var url = $"{servicePrefix}{controller}";*/

                HttpClient httpClient = new HttpClient();
               // var response = await httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/forecast?id=524901&appid=e9b98b4b8e7b477116d5163477065289");
               
                var response = await httpClient.GetAsync($"https://schoolager.azurewebsites.net/Api/Users/");
                //var response = await client.GetAsync(url);
                // var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = list
                };            
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<Response> Login(
          string urlBase,
          string servicePrefix,
          string controller,
          string email,
          string password)
        {
            try
            {
                string requestString = JsonConvert.SerializeObject(email);
                StringContent content = new StringContent(requestString, Encoding.UTF8, "application/json");
                /* HttpClient client = new HttpClient
                  {
                      BaseAddress = new Uri(urlBase)
                  };

                  string url = $"{servicePrefix}{controller}";
                  HttpResponseMessage response = await client.GetAsync(url);*/

                HttpClient httpClient = new HttpClient();
                var response =await  httpClient.PostAsync($"https://schoolager.azurewebsites.net/Api/Users/GetUserByEmail?email={email}&password={password}", content);
               // var response = await httpClient.PostAsync($"https://schoolager.azurewebsites.net/Api/Users/GetUserByEmail", content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode || result =="")
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }
    
                UserResponse userResponse = JsonConvert.DeserializeObject<UserResponse>(result);

                return new Response
                {
                    IsSuccess = true,
                    Result = userResponse
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}