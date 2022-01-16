using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BL.Services.AuthService
{
    public interface IAuthService
    {
        bool CheckIfUserExists();
    }
}
