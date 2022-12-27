using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RESTCountries.Services;
using RestSharp;
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
                System.Net.ServicePointManager.ServerCertificateValidationCallback =
    ((sender, certificate, chain, sslPolicyErrors) => true);

                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase),
                };

                var url = $"{servicePrefix}{controller}";
                
                var response = await client.GetAsync(url);


                var result = await response.Content.ReadAsStringAsync();


                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }
                else
                {
                    var list = JsonConvert.DeserializeObject<List<T>>(result);
                    return new Response
                    {
                        IsSuccess = true,
                        Result = list
                    };
                }
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

        public async Task<Response> GetCountries<T>(string urlBase, string servicePrefix, string controller)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
    ((sender, certificate, chain, sslPolicyErrors) => true);

            try
            {
                var client = new RestClient(urlBase+servicePrefix+controller);

                var request = new RestRequest(Method.GET);
                request.AddHeader("Content-Type", "application/json; charset=utf-8");
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Access-Control-Allow-Origin","*");
                var response = client.Execute(request);

                var content = response.Content;

                List<CityResponse> list = JsonConvert.DeserializeObject<List<CityResponse>>(content);
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
        public async Task<Response> GetWeather(
         string urlBase,
         string servicePrefix,
         string controller)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                string url = $"{servicePrefix}{controller}";

                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode || result == "")
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(result);

                return new Response
                {
                    IsSuccess = true,
                    Result = weatherResponse
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