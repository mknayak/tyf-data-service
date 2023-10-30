using tyf.data.service.Models;

namespace tyf.data.service.Managers
{
    public interface ISecurityManager
    {
        bool Compare(string salt, string strToHash, string hashedValue);
        string ComputeHash(string salt, string strToHash);
        AuthToken CreateToken(UserModel model);
        UserModel? ValidateToken(AuthToken authToken);
    }
}