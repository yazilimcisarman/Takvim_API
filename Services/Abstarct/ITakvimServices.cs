using Takvim_API.Models;

namespace Takvim_API.Services.Abstarct
{
    public interface ITakvimServices
    {
        Task<IEnumerable<Takvim>> GetAllEvent();
        Task<bool> CreateEvent(Takvim takvim);
        Task<bool> UpdateEvent(string id, Takvim takvim);
        Task<Takvim> GetEventById(string id);
        Task<List<Takvim>> GetEventByDate(DateTime start);
        Task DeleteEvent(string id);
    }
}
