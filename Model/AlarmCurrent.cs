using System.Collections.Generic;
namespace historianalarmservice.Model
{
    public class AlarmCurrent
    {
        public int thingId{get;set;}
        public List<Alarm> alarms{get;set;}
    }
}