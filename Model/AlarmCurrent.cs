using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace historianalarmservice.Model {
    public class AlarmCurrent {
        public int thingId { get; set; }

        [NotMapped]
        public Thing thing { get; set; }
        public List<Alarm> alarms { get; set; }
    }
}