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

        public async Task<Alarm> GetAlarmPerThingId(int thingId)
        {
            var alarmDb = await _context.AlarmCurrents
                          .Where(x=>x.thingId == thingId)
                          .FirstOrDefaultAsync();
            
            return alarmDb;
        }

        public async Task<List<Alarm>> GetAll()
        {
            var alarms = await _context.AlarmCurrents.ToListAsync();

            return alarms;
        }

        public async Task<Alarm> AddAlarm(Alarm alarm)
        {
            var alarmDb = await GetAlarmPerThingId(alarm.thingId);

            if(alarmDb != null)
            {
                var historianAlarm = await _historianService.getHistorianAlarmPerAlarmId(alarmDb.idAlarm);

                if(historianAlarm != null)
                {
                    historianAlarm.endDate = alarm.datetime;
                    
                    await _historianService.updateHistorianAlarm(historianAlarm.idHistorian, historianAlarm);
                }
                await deleteAlarm(alarmDb.idAlarm);
            }

            _context.AlarmCurrents.Add(alarm);
            await _context.SaveChangesAsync();

            var newHistorianAlarm = ConvertAlarmForHistorianAlarm(alarm);

            await _historianService.addHistorianAlarm(newHistorianAlarm);

            return alarm;
        }

         public async Task<bool> deleteAlarm(int alarmId)
        {
            var alarm = await _context.AlarmCurrents.Where(x=>x.idAlarm == alarmId).FirstOrDefaultAsync();

            _context.Remove(alarm);
            await _context.SaveChangesAsync();
            return true;
        }

        private HistorianAlarm ConvertAlarmForHistorianAlarm(Alarm alarm)
        {
            HistorianAlarm newHistorianAlarm = new HistorianAlarm();

            newHistorianAlarm.alarm = alarm.alarm;
            newHistorianAlarm.idAlarm = alarm.idAlarm;
            newHistorianAlarm.thingId = alarm.thingId;
            newHistorianAlarm.startDate = alarm.datetime;

            return newHistorianAlarm;
        }

        
    }
}