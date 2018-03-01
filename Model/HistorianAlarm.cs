using System.ComponentModel.DataAnnotations;
namespace historianalarmservice.Model
{
    public class HistorianAlarm
    {
        [Key]
        public int idHistorian{get;set;}
        public int idAlarm{get;set;}
        public int thingId{get;set;}
        public string alarm{get;set;}
        public long startDate{get;set;}
        public long endDate{get;set;}
    }
}