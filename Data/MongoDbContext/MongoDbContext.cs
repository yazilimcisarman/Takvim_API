using MongoDB.Driver;
using Takvim_API.Models;

namespace Takvim_API.Data.MongoDbContext
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IMongoDatabase database)
        {
            _database = database;
        }

        public IMongoCollection<Takvim> Takvim => _database.GetCollection<Takvim>("Takvim");
    }
}
