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
    public class AlarmController : Controller
    {
        private readonly IAlarmService _alarmService;

        public AlarmController(IAlarmService alarmService)
        {
            _alarmService = alarmService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var alarmsCurrents = await _alarmService.GetAll();

                if(alarmsCurrents == null)
                {
                    return NotFound();
                }

                return Ok(alarmsCurrents);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

        [HttpGet("{thingId}")]
        public async Task<IActionResult> GetThing(int thingId)
        {
            try
            {
                var alarmCurrent = await _alarmService.GetAlarmPerThingId(thingId);

                if(alarmCurrent == null)
                {
                    return NotFound();
                }

                return Ok(alarmCurrent);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Alarm alarm)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var alarmCurrent = await _alarmService.AddAlarm(alarm);

                    if(alarmCurrent == null)
                    {
                        return StatusCode(500,"Erro processo de inserção no banco");
                    }

                    return Created($"api/alarm/{alarmCurrent.thingId}",alarmCurrent);
                }
                
                return BadRequest(ModelState);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

        
        

    }
}