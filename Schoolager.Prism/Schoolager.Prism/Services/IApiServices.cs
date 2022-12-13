using Schoolager.Prism.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace Schoolager.Prism.Services
{
    public interface IApiServices
    {
        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller);
        Task<Response> Login(string urlBase, string servicePrefix, string controller, string email, string password);

    }
}
