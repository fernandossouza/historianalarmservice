using System.ComponentModel.DataAnnotations;
namespace historianalarmservice.Model
{
    public class Alarm
    {
        [Key]   
        public int idAlarm{get;set;}
        public int thingId{get;set;}
        public string alarm{get;set;}
        public long datetime{get;set;}
    }
}