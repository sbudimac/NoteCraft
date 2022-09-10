using NoteCraftModels;

namespace NoteCraftAPI.Repository
{
    public interface IUserRepository
    {
        User GetById(string id);
        User GetByUsername(string username);
        User Register(User user);
    }
}
