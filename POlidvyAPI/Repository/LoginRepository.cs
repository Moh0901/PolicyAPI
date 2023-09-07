using POlidvyAPI.Model;
using POlidvyAPI.ViewModels;

namespace POlidvyAPI.Repository
{
    public class LoginRepository: ILoginRepository
    {
        private readonly PolicyMDBContext _context;
        public LoginRepository(PolicyMDBContext context)
        {
            _context = context;
        }
        public UserTbl Authenticate(LoginViewModel user)
        {
            var loggedUser = _context.UserTbls.FirstOrDefault(
                x => x.Username.ToLower() == user.Username.ToLower() && x.Password == user.Password);

            if (loggedUser == null)
            {
                return null;
            }
            return loggedUser;
        }
   
    }
}
