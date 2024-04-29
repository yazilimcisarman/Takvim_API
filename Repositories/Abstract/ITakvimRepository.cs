using Takvim_API.Models;

namespace Takvim_API.Repositories.Abstract
{
    public interface ITakvimRepository
    {
        //Create
        Task<bool> CreateEvent(Takvim takvim);
        //Read
        Task<IEnumerable<Takvim>> GetTakvim();
        //Update
        Task<bool> UpdateEvent(string takvimid, Takvim updatetakvim);
        //Delete
        Task DeleteEvent(string takvimid);

        //GetEventById
        Task<Takvim> GetEventById(string id);

        //GetEventByDate

        Task<List<Takvim>> GetEventByDate(DateTime start);
    }
}
