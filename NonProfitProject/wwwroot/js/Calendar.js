let calendarEl = document.getElementById('calendar');

let calendar = new FullCalendar.Calendar(calendarEl, {
    initalView: 'dayGridMonth',
    headerToolbar: {
        left: 'prev,next today',
        center: 'title',
        right: 'dayGridMonth,timeGridWeek,timeGridDay'
    },
    events: [
        {
            title: "Calendar Created",
            start: "2021-06-26",
        },
    ],
});

calendar.render();

/*Bootstrap calendar import from https://cdn.jsdelivr.net/npm/fullcalendar@5.1.0/main.min.js */