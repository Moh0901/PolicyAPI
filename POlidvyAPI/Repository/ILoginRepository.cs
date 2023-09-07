using NuGet.Protocol.Plugins;
using POlidvyAPI.Model;
using POlidvyAPI.ViewModels;

namespace POlidvyAPI.Repository
{
    public interface ILoginRepository
    {
        UserTbl Authenticate(LoginViewModel user);
    }
}
