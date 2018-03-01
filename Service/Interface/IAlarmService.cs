using System.Threading.Tasks;
using System.Collections.Generic;
using historianalarmservice.Model;
namespace historianalarmservice.Service.Interface
{
    public interface IAlarmService
    {
        Task<Alarm> GetAlarmPerThingId(int thingId);
        Task<List<Alarm>> GetAll();
        Task<Alarm> AddAlarm(Alarm alarm);
         
    }
}