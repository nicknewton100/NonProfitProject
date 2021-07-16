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
            title: '10k Run for Cancer!',
            start: '2021-07-24',    
            end: '2021-07-31'
        },
        {
            title: 'Movie Night',
            start: '2021-07-26',
            end: '2021-07-26'
        },
    ]
});

calendar.render();

/*Bootstrap calendar import from https://cdn.jsdelivr.net/npm/fullcalendar@5.1.0/main.min.js */