using BookStore.BL.Services.LogService;
using BookStore.Shared.Interfaces;
using BookStore.Shared.Models;

namespace BookStore.BL.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> _repo;
        private readonly ILogger _logger;

        public AuthService(IRepository<User> repo ,ILogger logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public bool CheckIfUserExists()
        {
            return true;
        }
    }
}
