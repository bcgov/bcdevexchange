using bcdevexchange.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bcdevexchange
{
    public interface IEventBriteService
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<IEnumerable<Event>> GetAllCoursesAsync();
    }
}
