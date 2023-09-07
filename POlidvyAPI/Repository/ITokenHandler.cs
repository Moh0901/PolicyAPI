using POlidvyAPI.Model;

namespace POlidvyAPI.Repository
{
    public interface ITokenHandler
    {
        String CreateToken(UserTbl user);
    }
}
