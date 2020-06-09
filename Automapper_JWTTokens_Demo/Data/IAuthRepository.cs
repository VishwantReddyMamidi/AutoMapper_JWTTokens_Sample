using Automapper_JWTTokens_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automapper_JWTTokens_Demo.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Regsiter(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}
