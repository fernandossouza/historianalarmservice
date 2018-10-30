using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace historianalarmservice.Model {
    public class Alarm {
        [Key]
        public int alarmId { get; set; }

        [Required]
        public int? thingId { get; set; }

        [NotMapped]
        public Thing thing { get; set; }

        [Required]
        [MaxLength (100)]
        public string alarmDescription { get; set; }

        [Required]
        [MaxLength (50)]
        public string alarmName { get; set; }

        [Required]
        [MaxLength (10)]
        public string alarmColor { get; set; }

        [Required]
        public int priority { get; set; }

        [Required]
        public long? datetime { get; set; }
    }
}