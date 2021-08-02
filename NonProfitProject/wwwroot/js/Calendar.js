
let calendarEl = document.getElementById('calendar');

let calendar = new FullCalendar.Calendar(calendarEl, {
    initalView: 'dayGridMonth',
    headerToolbar: {
        left: 'prev,next today',
        center: 'title',
        right: 'dayGridMonth,timeGridWeek,timeGridDay'
    },
    eventClick: function (info) {
        var dateString;
        
        if (info.event.start.toDateString() == info.event.end.toDateString()) {
            dateString = `${moment(info.event.start).format('LL')}    ${moment(info.event.start).format('LT')} - ${moment(info.event.end).format('LT')}`
        }
        else{
            dateString = `${moment(info.event.start).format('LLLL')} - ${moment(info.event.end).format('LLLL')}`
        }
        console.log(dateString);
        Swal.fire({
            title: `<h1 class:"bg-dark;" style="text-align:center;">${info.event.title}</h1>`,
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
        calendar.addEvent(
            {
                title: model[i].eventName,
                start: model[i].eventStartDate,
                end: model[i].eventEndDate,
                details: model[i].eventDescription,
                address: address
            });
    }
}
    calendar.render();

/*Bootstrap calendar import from https://cdn.jsdelivr.net/npm/fullcalendar@5.1.0/main.min.js */