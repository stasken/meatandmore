using MeatAndMoreAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeatAndMoreAPI.Services
{
    public interface IUserService
    {
        Task<UserDTO> Login(string username, string password);
    }
}
