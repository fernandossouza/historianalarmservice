using System.ComponentModel.DataAnnotations;
namespace historianalarmservice.Model
{
    public class HistorianAlarm
    {
        [Key]
        public int historianId{get;set;}
        [Required]
        public int alarmId{get;set;}
        [Required]
        public int thingId{get;set;}        
        [Required]
        [MaxLength(100)]
        public string alarmDescription{get;set;}
        [Required]
        [MaxLength(50)]
        public string alarmName{get;set;}
        [Required]
        [MaxLength(10)]
        public string alarmColor{get;set;}
        [Required]
        public long startDate{get;set;}
        public long endDate{get;set;}
    }
}