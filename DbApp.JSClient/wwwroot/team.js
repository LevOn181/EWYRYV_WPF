let teams = [];
let connection = null;
getdata();
setupSignalR();
defaultValuesToLoad();

let teamIdToUpdate = -1;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:15885/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("TeamCreated", (user, message) => {
        getdata();
    });

    connection.on("TeamDeleted", (user, message) => {
        getdata();
    });
    connection.on("TeamUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();


}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
}

async function getdata() {
    await fetch('http://localhost:15885/team')
        .then(x => x.json())
        .then(y => {
            teams = y;
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    teams.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr>"
            + "<td>" + t.teamId + "</td>"
            + "<td>" + t.name + "</td>"
            + "<td>" + t.foundationYear + "</td>"
            + "<td>"
            + `<button type="button" onclick="remove(${t.teamId})">Delete</button>`
            + `<button type="button" onclick="showupdate(${t.teamId})">Update</button>`
            + "</td>"
            + "</tr>"
    });
}
function remove(id) {
    fetch('http://localhost:15885/team/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    document.getElementById('formdiv').style.display = 'flex';

    fetch('http://localhost:15885/team', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                teamId: teamIdToUpdate,
                name: name,
                foundationYear: foundationYear,

            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        })
}
function showupdate(id) {
    document.getElementById('teamnameupdate').value = teams.find(t => t['teamId'] == id)['name'];
    document.getElementById('teamfoundationyearupdate').value = teams.find(t => t['teamId'] == id)['foundationYear'];
    document.getElementById('formdiv').style.display = 'none'
    document.getElementById('updateformdiv').style.display = 'flex';
    teamIdToUpdate = id;
}


function create() {
    let teamname = document.getElementById('teamname').value;
    let foundationYear = document.getElementById('teamfoundationyear').value;

    fetch('http://localhost:15885/team', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                name: teamname,
                foundationYear: foundationYear

            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        })
}

function cancel() {
    document.getElementById('updateformdiv').style.display = 'none';
    document.getElementById('formdiv').style.display = 'flex';
}

function defaultValuesToLoad() {
    document.getElementById('teamname').placeholder = "Team name";
    document.getElementById('teamfoundationyear').placeholder = "Year of foundation (1800-2023)";
}