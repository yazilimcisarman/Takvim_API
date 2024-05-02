using Takvim_API.Models;
using Takvim_API.Repositories.Abstract;
using Takvim_API.Services.Abstarct;

namespace Takvim_API.Services.Concrete
{
    public class TakvimServices : ITakvimServices
    {
        private readonly ITakvimRepository _repository;

        public TakvimServices(ITakvimRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Takvim>> GetAllEvent()
        {
            return await _repository.GetTakvim();
        }
        public async Task<bool> CreateEvent(Takvim takvim)
        {
            var result = await _repository.CreateEvent(takvim);
            return result;
        }

        public async Task<bool> UpdateEvent(string id, Takvim takvim)
        {
            var result = await _repository.UpdateEvent(id,takvim);
            return result;
        }
        public async Task<Takvim> GetEventById(string id)
        {
            return await _repository.GetEventById(id);
        }
        public async Task<List<Takvim>> GetEventByDate(DateTime start)
        {
            return await _repository.GetEventByDate(start);
        }
        public async Task DeleteEvent(string id)
        {
            await _repository.DeleteEvent(id);
        }
    }
}
