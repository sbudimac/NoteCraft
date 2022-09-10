using MongoDB.Driver;
using NoteCraftAPI.Settings;
using NoteCraftModels;

namespace NoteCraftAPI.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> users;

        public UserRepository(INotecraftDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            users = database.GetCollection<User>(settings.UsersCollectionName);
        }

        public User GetById(string id)
        {
            return users.Find(user => user.Id == id).FirstOrDefault();
        }

        public User GetByUsername(string username)
        {
            return users.Find(user => user.Username == username).FirstOrDefault();
        }

        public User Register(User user)
        {
            users.InsertOne(user);

            return user;
        }
    }
}
