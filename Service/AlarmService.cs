using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using historianalarmservice.Model;
using historianalarmservice.Data;
using historianalarmservice.Service.Interface;

namespace historianalarmservice.Service
{
    public class AlarmService : IAlarmService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHistorianAlarmsService _historianService;
        public AlarmService (ApplicationDbContext context, IHistorianAlarmsService historianService)
        {
            _context = context;
            _historianService = historianService;
        }

        public async Task<List<Alarm>> GetAlarmPerThingId(int thingId)
        {
            var alarmDb = await _context.AlarmCurrents
                          .Where(x=>x.thingId == thingId)
                          .ToListAsync();
            
            return alarmDb;
        }

        private async Task<Alarm> GetAlarmPerThingIdAndAlarmName(int thingId,string alarmName)
        {
            var alarmDb = await _context.AlarmCurrents
                          .Where(x=>x.thingId == thingId && x.alarmName ==  alarmName)
                          .FirstOrDefaultAsync();
            
            return alarmDb;
        }

        public async Task<List<Alarm>> GetAll()
        {
            var alarms = await _context.AlarmCurrents.OrderBy(a=>a.thingId).ToListAsync();

            return alarms;
        }

        public async Task<Alarm> AddAlarm(Alarm alarm)
        {
            var alarmDb = await GetAlarmPerThingIdAndAlarmName(alarm.thingId.Value,alarm.alarmName);

            if(alarmDb != null)
            {
                var historianAlarm = await _historianService.getHistorianAlarmPerAlarmId(alarmDb.alarmId);

                if(historianAlarm != null)
                {
                    historianAlarm.endDate = alarm.datetime.Value;
                    
                    await _historianService.updateHistorianAlarm(historianAlarm.historianId, historianAlarm);
                }
                await deleteAlarm(alarmDb.alarmId);
            }

            _context.AlarmCurrents.Add(alarm);
            await _context.SaveChangesAsync();

            var newHistorianAlarm = ConvertAlarmForHistorianAlarm(alarm);

            await _historianService.addHistorianAlarm(newHistorianAlarm);

            return alarm;
        }

         public async Task<bool> deleteAlarm(int alarmId)
        {
            var alarm = await _context.AlarmCurrents.Where(x=>x.alarmId == alarmId).FirstOrDefaultAsync();

            _context.Remove(alarm);
            await _context.SaveChangesAsync();
            return true;
        }

        private HistorianAlarm ConvertAlarmForHistorianAlarm(Alarm alarm)
        {
            HistorianAlarm newHistorianAlarm = new HistorianAlarm();

            newHistorianAlarm.alarmDescription = alarm.alarmDescription;
            newHistorianAlarm.alarmId = alarm.alarmId;
            newHistorianAlarm.thingId = alarm.thingId.Value;
            newHistorianAlarm.startDate = alarm.datetime.Value;
            newHistorianAlarm.alarmColor = alarm.alarmColor;
            newHistorianAlarm.alarmName = alarm.alarmName;

            return newHistorianAlarm;
        }

        
    }
}