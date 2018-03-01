using System.Threading.Tasks;
using System.Collections.Generic;
using historianalarmservice.Model;
namespace historianalarmservice.Service.Interface
{
    public interface IAlarmService
    {
        Task<Alarm> getAlarmPerThingId(int thingId);
        Task<Alarm> addAlarm(Alarm alarm);
         
    }
}