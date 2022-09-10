using NoteCraftModels;

namespace NoteCraftAPI.Service
{
    public interface IUserService
    {
        User GetById(string id);
        User GetByUsername(string username);
        User Register(UserDto userToRegister);
        string CreateToken(User user);
    }
}
