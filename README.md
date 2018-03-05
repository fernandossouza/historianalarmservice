# historianalarmservice
API with historian of alarm and alarms currents

## Alarm
this used for CRUD of alarm
- alarmId: id alarm current.
    - Integer
    - Ignored on Create
- thingId: id of thing
    - Integer
    - Required
- alarmName: name of the alarm
    - String(50)
    - Required
- alarmDescription: description of the alarm
    - String(50)
    - Required
- alarmColor: color of the alarm
    - String(50)
    - Required
- datetime: date in ticks of the alarm
    - Long
    - Required

### JSON Example
```json
[
    {
        "thingId": 1,
        "alarms": [
            {
                "alarmId": 2,
                "thingId": 1,
                "alarmDescription": "Alto",
                "alarmName": "Pressão",
                "alarmColor": "#ed0404",
                "datetime": 636555924000000000
            }
        ]
    },
    {
        "thingId": 2,
        "alarms": [
            {
                "alarmId": 3,
                "thingId": 2,
                "alarmDescription": "Normal",
                "alarmName": "Pressão",
                "alarmColor": "#2319e8",
                "datetime": 636555942000000000
            },
            {
                "alarmId": 4,
                "thingId": 2,
                "alarmDescription": "Baixa",
                "alarmName": "Agitação",
                "alarmColor": "#2aed10",
                "datetime": 636555978000000000
            }
        ]
    }
]
```

## Url Alarm
* api/alarm
    * GET: Return alarms current for all things
    * POST: Create new alarm current
* api/alarm/{thingId}
    * GET: Return alarms current of the thing

## HistorianAlarm
this return historian alarm
- values: list with historian alarms
    - List: alarm
- total: numeric total of alarms of the filter
    - Integer
- historianId: id of the historian
    - Integer
- alarmId: id of the alarm current
    - Integer
- thingId: id of the thing
    - Integer
- alarmDescription: description of the alarm
    - String(100)
- alarmName: name of the alarm
    - String(20)
- alarmColor: color of the alarm
    - String(10)
- startDate: start date in ticks of the alarm
    - long
- endDate: end date in ticks of the alarm
    - long

### JSON Example
```json
{
    "values": [
        {
            "historianId": 3,
            "alarmId": 3,
            "thingId": 2,
            "alarmDescription": "Normal",
            "alarmName": "Pressão",
            "alarmColor": "Azul",
            "startDate": 3,
            "endDate": 0
        },
        {
            "historianId": 4,
            "alarmId": 4,
            "thingId": 2,
            "alarmDescription": "Baixa",
            "alarmName": "Agitação",
            "alarmColor": "Verde",
            "startDate": 4,
            "endDate": 0
        }
    ],
    "total": 2
}
```

## Url HistorianAlarm
* api/historianAlarm?thingid={thingId}&startDate={startDate}&endDate={endDate}&startat={startat}&quantity={quantity}
    * GET: Return historian alarms of the thing
        * thingId: id thing 
        * startDate: period inicial of the search
        * endDate: period last of the search
        * startat: represent where the list starts t the database (Default=0)
        * quantity: number of resuls in the query (Default=50)





