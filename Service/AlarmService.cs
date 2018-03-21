using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using historianalarmservice.Data;
using historianalarmservice.Model;
using historianalarmservice.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace historianalarmservice.Service {
    public class AlarmService : IAlarmService {
        private readonly ApplicationDbContext _context;
        private readonly IHistorianAlarmsService _historianService;
        private readonly IConfiguration _configuration;
        private readonly HttpClient client = new HttpClient ();
        public AlarmService (ApplicationDbContext context, IHistorianAlarmsService historianService, IConfiguration configuration) {
            _context = context;
            _historianService = historianService;
            _configuration = configuration;
        }

        public async Task<List<AlarmCurrent>> GetAlarmPerThingId (int thingId) {
            var alarmDb = await _context.AlarmCurrents
                .Where (x => x.thingId == thingId)
                .ToListAsync ();

            var alarmCurrent = ConvertAlarmInAlarmCurrent (alarmDb);

            return alarmCurrent;
        }

        private async Task<Alarm> GetAlarmPerThingIdAndAlarmName (int thingId, string alarmName) {
            var alarmDb = await _context.AlarmCurrents
                .Where (x => x.thingId == thingId && x.alarmName == alarmName)
                .FirstOrDefaultAsync ();

            return alarmDb;
        }

        public async Task<List<AlarmCurrent>> GetAll () {
            var alarms = await _context.AlarmCurrents.OrderBy (a => a.thingId).ToListAsync ();

            var alarmCurrent = ConvertAlarmInAlarmCurrent (alarms);

            return alarmCurrent;
        }

        private List<AlarmCurrent> ConvertAlarmInAlarmCurrent (IList<Alarm> alarms) {
            List<AlarmCurrent> alarmCurrentList = new List<AlarmCurrent> ();

            var alarmGroups = alarms.GroupBy (a => a.thingId);

            foreach (var alarmGroup in alarmGroups) {
                AlarmCurrent alarmCurrent = new AlarmCurrent ();
                foreach (var alarm in alarmGroup) {
                    if (alarmCurrent.alarms == null)
                        alarmCurrent.alarms = new List<Alarm> ();

                    alarmCurrent.thingId = alarm.thingId.Value;
                    alarmCurrent.alarms.Add (alarm);
                }
                alarmCurrentList.Add (alarmCurrent);
            }

            return alarmCurrentList;
        }

        public async Task<Alarm> AddAlarm (Alarm alarm) {
            var alarmDb = await GetAlarmPerThingIdAndAlarmName (alarm.thingId.Value, alarm.alarmName);

            if (alarmDb != null) {
                var historianAlarm = await _historianService.getHistorianAlarmPerAlarmId (alarmDb.alarmId);

                if (historianAlarm != null) {
                    historianAlarm.endDate = alarm.datetime.Value;

                    await _historianService.updateHistorianAlarm (historianAlarm.historianId, historianAlarm);
                }
                await deleteAlarm (alarmDb.alarmId);
            }

            _context.AlarmCurrents.Add (alarm);
            await _context.SaveChangesAsync ();

            var newHistorianAlarm = ConvertAlarmForHistorianAlarm (alarm);

            await _historianService.addHistorianAlarm (newHistorianAlarm);

            Trigger (alarm);
            return alarm;
        }
        public async Task<bool> deleteAlarm (int alarmId) {
            var alarm = await _context.AlarmCurrents.Where (x => x.alarmId == alarmId).FirstOrDefaultAsync ();

            _context.Remove (alarm);
            await _context.SaveChangesAsync ();
            return true;
        }

        private async void Trigger (Alarm alarm) {
            try {
                if (_configuration["AlarmPostEndpoint"] != null) {
                    client.DefaultRequestHeaders.Accept.Clear ();
                    client.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));
                    var content = new StringContent (JsonConvert.SerializeObject (alarm), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync (_configuration["AlarmPostEndpoint"], content);
                    if (response.IsSuccessStatusCode) {
                        Console.WriteLine ("Data posted on AssociationPostEndpoint");
                        //Console.WriteLine (await response.Content.ReadAsStringAsync ());
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine (ex.Message);
            }
        }
        private HistorianAlarm ConvertAlarmForHistorianAlarm (Alarm alarm) {
            HistorianAlarm newHistorianAlarm = new HistorianAlarm ();

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