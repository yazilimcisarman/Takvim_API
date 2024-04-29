using MongoDB.Driver;
using Takvim_API.Data.MongoDbContext;
using Takvim_API.Models;
using Takvim_API.Repositories.Abstract;

namespace Takvim_API.Repositories.Concrete
{
    public class TakvimRepository : ITakvimRepository
    {
        private readonly IMongoCollection<Takvim> _takvim;
        public TakvimRepository(MongoDbContext database)
        {
            _takvim = database.Takvim;
        }

        public async Task<IEnumerable<Takvim>> GetTakvim()
        {
            return await _takvim.Find(title => true).ToListAsync();
        }
        public async Task<bool> CreateEvent(Takvim takvim)
        {
            await _takvim.InsertOneAsync(takvim);
            return true;
        }
        public async Task<bool> UpdateEvent(string takvimid, Takvim newtakvim)
        {
            var oldtakvim = Builders<Takvim>.Filter.Eq(x => x._id, takvimid);

            var takvim = Builders<Takvim>.Update
                .Set(x => x.Title, newtakvim.Title)
                .Set(x => x.Description, newtakvim.Description)
                .Set(x => x.Start, newtakvim.Start)
                .Set(x => x.End, newtakvim.End)
                .Set(x => x.Location, newtakvim.Location);
            var result = await _takvim.UpdateOneAsync(oldtakvim,takvim);
            if (result.ModifiedCount > 0)
            {
                return true;
            }
            return false;
        }
        public async Task DeleteEvent(string takvimid)
        {
            await _takvim.DeleteOneAsync(x => x._id == takvimid);
        }
        public async Task<Takvim> GetEventById(string id)
        {
            return await _takvim.Find(x => x._id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Takvim>> GetEventByDate(DateTime start)
        {
            return await _takvim.Find(x => x.Start == start).ToListAsync();
        }
    }
}
