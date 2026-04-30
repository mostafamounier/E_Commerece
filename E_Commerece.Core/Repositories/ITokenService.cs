using E_Commerece.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerece.Core.Repositories
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(AppUser user);
    }
}
