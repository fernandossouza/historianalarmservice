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
    public class HistorianAlarmsService : IHistorianAlarmsService
    {
        private readonly ApplicationDbContext _context;
        public HistorianAlarmsService (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<HistorianAlarm> getHistorianAlarm(int historianId)
        {
            var historianAlarm = await _context.HistorianAlarms
                                .Where(a=>a.idHistorian == historianId )
                                .FirstOrDefaultAsync();

            return historianAlarm;
        }
        public async Task<HistorianAlarm> getHistorianAlarmPerAlarmId(int alarmId)
        {
            var historianAlarm = await _context.HistorianAlarms
                                .Where(a=>a.idAlarm == alarmId )
                                .FirstOrDefaultAsync();

            return historianAlarm;
        }

        public async Task<(IEnumerable<HistorianAlarm>,int)> getHistorianAlarmList(int thingId,long startDate,long endDate,
        int startat, int quantity)
        {
            var historianAlarm = await _context.HistorianAlarms
                                .Where(a=>a.thingId == thingId 
                                && a.startDate >= startDate && a.startDate <= endDate)
                                .Skip(startat).Take(quantity)
                                .ToListAsync();

            var total = await _context.HistorianAlarms
                                .Where(a=>a.thingId == thingId 
                                && a.startDate >= startDate && a.startDate <= endDate)
                                .CountAsync();

            return (historianAlarm,total);
        }

        public async Task<HistorianAlarm> addHistorianAlarm(HistorianAlarm historianAlarm)
        {
            _context.HistorianAlarms.Add(historianAlarm);
            await _context.SaveChangesAsync();
            return historianAlarm;            

        }

        public async Task<HistorianAlarm> updateHistorianAlarm(int historianAlarmId,HistorianAlarm historianAlarm)
        {
            var historianDb = await _context.HistorianAlarms
                            .Where(x=> x.idHistorian == historianAlarmId)
                            .AsNoTracking()
                            .FirstOrDefaultAsync();

            if( historianDb == null || historianDb.idHistorian != historianAlarm.idHistorian)
            {
                return null;
            }

            _context.HistorianAlarms.Update(historianAlarm);
            await _context.SaveChangesAsync();
            return historianAlarm;
        }

        
        
    }
}