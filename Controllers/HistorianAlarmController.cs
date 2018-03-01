using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using historianalarmservice.Service;
using historianalarmservice.Service.Interface;
using historianalarmservice.Model;

namespace historianalarmservice.Controllers
{
    [Route("api/[controller]")]
    public class HistorianAlarmController : Controller
    {
         private readonly IHistorianAlarmsService _historianAlarmsService;

        public HistorianAlarmController(IHistorianAlarmsService historianAlarmsService)
        {
            _historianAlarmsService = historianAlarmsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHistorian([FromQuery]int thingId,[FromQuery]long startDate,[FromQuery]long endDate,
        [FromQuery]int startat, [FromQuery]int quantity)
        {
            try
            {
                if (quantity == 0)
                    quantity = 50;

                var (alarmsHistorian,total) = await _historianAlarmsService.getHistorianAlarmList(thingId,startDate,endDate,startat,quantity);

                if(alarmsHistorian == null || alarmsHistorian.Count()==0)
                {
                    return NotFound();
                }

                return Ok(new{values = alarmsHistorian, total = total});

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }
    }
}