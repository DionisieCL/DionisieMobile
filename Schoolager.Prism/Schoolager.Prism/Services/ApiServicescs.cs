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
                var client = new HttpClient
                {
                    BaseAddress = new Uri($"{urlBase}"),
                };

                var url = $"{servicePrefix}{controller}";


               
               var response = await client.GetAsync(url);
              
                var result = await response.Content.ReadAsStringAsync();
                /*
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }
                else
                {*/



               
              /*  if (result.Equals(jsonString))
                {

                }*/
                    
                    var list = JsonConvert.DeserializeObject<List<T>>(result);
                   // var list = JsonConvert.DeserializeObject<List<T>>(result);
                    return new Response
                    {
                        IsSuccess = true,
                        Result = list
                    };
               // }
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
          User user)
        {
            try
            {
                string requestString = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(requestString, Encoding.UTF8, "application/json");
                 HttpClient client = new HttpClient
                  {
                      BaseAddress = new Uri(urlBase)
                  };

                  string url = $"{servicePrefix}{controller}";

                var response = await client.PostAsync(url, content);
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