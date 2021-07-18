let calendarEl = document.getElementById('calendar');

let calendar = new FullCalendar.Calendar(calendarEl, {
    initalView: 'dayGridMonth',
    headerToolbar: {
        left: 'prev,next today',
        center: 'title',
        right: 'dayGridMonth,timeGridWeek,timeGridDay'
    },
    events: []
});

//references for function below: https://stackoverflow.com/questions/16361364/accessing-mvcs-model-property-from-javascript
function setCalendar(e) {
    var model = JSON.parse(e);
    for (var i = 0; i < model.length; i++) {
        model[i].eventStartDate = model[i].eventStartDate.substring(0, model[i].eventStartDate.length - 9);
        model[i].eventEndDate = model[i].eventEndDate.substring(0, model[i].eventEndDate.length - 9);
        calendar.addEvent({ title: model[i].eventName, start: model[i].eventStartDate, end: model[i].eventEndDate });
    }
}
    calendar.render();

/*Bootstrap calendar import from https://cdn.jsdelivr.net/npm/fullcalendar@5.1.0/main.min.js */