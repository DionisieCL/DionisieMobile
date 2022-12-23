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

                /*var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase),
                };

                var url = $"{servicePrefix}{controller}";*/
                HttpClient client = new HttpClient();
                var response = await client.GetAsync("https://restcountries.eu/rest/v1/all");


                var result = await response.Content.ReadAsStringAsync();
                //string url = "https://restcountries.eu/rest/v1/all";

                // Web Request with the given url.
                /*WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;

                WebResponse response = request.GetResponse();*/

                return null;

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

              /*  var list = JsonConvert.DeserializeObject<List<T>>(result);
                   // var list = JsonConvert.DeserializeObject<List<T>>(result);
                    return new Response
                    {
                        IsSuccess = true,
                        Result = list
                    };*/
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

        public async Task<Response> Test<T>()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
    ((sender, certificate, chain, sslPolicyErrors) => true);
            /*string WebAPIUrl = "https://api.first.org/data/v1/countries"; // Set your REST API URL here.
            var uri = new Uri(WebAPIUrl);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            */

            try
            {
                var client = new RestClient("https://restcountries.com/v2/all");

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

                //var response = await client.GetAsync(uri);
                //var response = await RESTCountriesAPI.GetAllCountriesAsync();
                //  var content = await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
            }
            return null;
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
                //string requestString = JsonConvert.SerializeObject(user);
                //StringContent content = new StringContent(requestString, Encoding.UTF8, "application/json");
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