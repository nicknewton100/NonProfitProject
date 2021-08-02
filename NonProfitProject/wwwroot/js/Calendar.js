

let calendarEl = document.getElementById('calendar');

let calendar = new FullCalendar.Calendar(calendarEl, {
    initalView: 'dayGridMonth',
    height: 600,
    aspectRatio: 2,
    contentHeight: "integer, auto",
    headerToolbar: {
        left: 'prev,next today',
        center: 'title',
        right: 'dayGridMonth,timeGridWeek,timeGridDay'
    },
    eventClick: function (info) {
        var dateString;
        console.log(info.event.extendedProps.eventid)
        if (info.event.start.toDateString() == info.event.end.toDateString()) {
            dateString = `${moment(info.event.start).format('LL')}    ${moment(info.event.start).format('LT')} - ${moment(info.event.end).format('LT')}`
        }
        else{
            dateString = `${moment(info.event.start).format('LLLL')} - ${moment(info.event.end).format('LLLL')}`
        }
        Swal.fire({
            title: `<head><link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous"></head><div class="row justify-content-center"><h1 class:"bg-dark;" style="text-align:center;">${info.event.title}</h1><!--<form action="/eventcalendar/${info.event.extendedProps.eventid}/email/" >
                        <button type="button" class="fas fa-envelope" style="margin-right: 10px;padding:0; font-size: larger; background-color: transparent; outline: none; border: none;position:absolute; left: 1em; top: 1em;" onclick="Share(this)"></button>
                    </form>--></div>`,
            html: `<div style="font-size:medium;"><h4 style="margin-bottom:10px;text-align:left;">Details</h4><p style="text-align:left;"><i class="fas fa-clock" style="margin-right:1em;"></i>${dateString}</p><p style="text-align:left;"><i class="fas fa-map-marker" style="margin-right:1em;"></i>${info.event.extendedProps.address}</p><br /><p style="text-align:left;">${info.event.extendedProps.details}</p></div>`,
            width: '700',
            showConfirmButton: false,
            showCloseButton: true,
            
        })
    },
});

//references for function below: https://stackoverflow.com/questions/16361364/accessing-mvcs-model-property-from-javascript
function setCalendar(e) {
    var model = JSON.parse(e);
    for (var i = 0; i < model.length; i++) {
        var address;
        if (model[i].eventAddr2 == undefined) {
            address = model[i].eventAddr1 + ", " + model[i].eventCity + ", " + model[i].eventState + " " + model[i].eventPostalCode;
        }
        else {
            address = model[i].eventAddr1 + " " + model[i].eventAdd2 + ", " + model[i].eventCity + ", " + model[i].eventState + " " + model[i].eventPostalCode;
        }
        console.log(model[i].eventID);
        calendar.addEvent(
            {
                title: model[i].eventName,
                start: model[i].eventStartDate,
                end: model[i].eventEndDate,
                details: model[i].eventDescription,
                address: address,
                eventid: model[i].eventID
            });
    }
}
    calendar.render();

/*Bootstrap calendar import from https://cdn.jsdelivr.net/npm/fullcalendar@5.1.0/main.min.js */