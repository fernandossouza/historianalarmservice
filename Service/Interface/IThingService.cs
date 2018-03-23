using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using historianalarmservice.Model;

namespace historianalarmservice.Service.Interface
{

    public interface IThingService
    {
        Task<(Thing, HttpStatusCode)> getThing(int thingId);
        Task<(List<Thing>, HttpStatusCode)> getThingList(int[] thingId);
    }
}
