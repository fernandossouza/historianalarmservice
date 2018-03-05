using System.Threading.Tasks;
using System.Collections.Generic;
using historianalarmservice.Model;
namespace historianalarmservice.Service.Interface
{
    public interface IAlarmService
    {
        Task<List<AlarmCurrent>> GetAlarmPerThingId(int thingId);
        Task<List<AlarmCurrent>> GetAll();
        Task<Alarm> AddAlarm(Alarm alarm);
         
    }
}