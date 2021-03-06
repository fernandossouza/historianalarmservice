using System.Threading.Tasks;
using System.Collections.Generic;
using historianalarmservice.Model;

namespace historianalarmservice.Service.Interface
{
    public interface IHistorianAlarmsService
    {
        Task<HistorianAlarm> getHistorianAlarm(int historianId);
        Task<HistorianAlarm> getHistorianAlarmPerAlarmId(int alarmId);
        Task<(IEnumerable<HistorianAlarm>,int)> getHistorianAlarmList(int thingId,long startDate,long endDate,int startat, int quantity);
        Task<HistorianAlarm> addHistorianAlarm(HistorianAlarm Historian);
        Task<HistorianAlarm> updateHistorianAlarm(int hitorianAlarmId,HistorianAlarm historianAlarm);
         
    }
}