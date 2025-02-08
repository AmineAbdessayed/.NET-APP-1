using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProject.Models;

namespace ApiProject.Services
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}