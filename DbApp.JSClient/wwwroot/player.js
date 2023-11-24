let players = [];
const teams = [];
let connection = null;
getdata();
setupSignalR();
defaultValuesToLoad();

let playerIdToUpdate = -1;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:15885/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("PlayerCreated", (user, message) => {
        getdata();
    });

    connection.on("PlayerDeleted", (user, message) => {
        getdata();
    });
    connection.on("PlayerUpdated", (user, message) => {
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
    await fetch('http://localhost:15885/player')
        .then(x => x.json())
        .then(y => {
            players = y;
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    players.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr>"
            + "<td>" + t.playerId + "</td>"
            + "<td>" + t.name + "</td>"
            + "<td>" + t.birthDate + "</td>"
            + "<td>" + t.kitNumber + "</td>"
            + "<td>" + t.teamId + "</td>"
            + "<td>" + t.value + "</td>"
            + "<td>" 
        + `<button type="button" onclick="remove(${t.playerId})">Delete</button>`
        + `<button type="button" onclick="showupdate(${t.playerId})">Update</button>`
            + "</td>"
          + "</tr>"
    });
}
function remove(id) {
    fetch('http://localhost:15885/player/' + id, {
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
    document.getElementById('formdiv').style.display = 'flex'

    fetch('http://localhost:15885/player', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                playerId: playerIdToUpdate,
                birthDate: birthDate,
                kitNumber: kitNumber,
                name: name,
                teamId: teamId,
                value: value

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
    document.getElementById('playernameupdate').value = players.find(t => t['playerId'] == id)['name'];
    document.getElementById('playerbirthdateupdate').value = players.find(t => t['playerId'] == id)['birthDate'];
    document.getElementById('playerkitnumberupdate').value = players.find(t => t['playerId'] == id)['kitNumber'];
    document.getElementById('playerteamidupdate').value = players.find(t => t['playerId'] == id)['teamId'];
    document.getElementById('playervalueupdate').value = players.find(t => t['playerId'] == id)['value'];
    document.getElementById('formdiv').style.display = 'none'
    document.getElementById('updateformdiv').style.display = 'flex';
    playerIdToUpdate = id;
}

function cancel() {
    document.getElementById('updateformdiv').style.display = 'none';
    document.getElementById('formdiv').style.display = 'flex';
}

function create() {
    let name = document.getElementById('playername').value;
    let birthDate = document.getElementById('playerbirthdate').value;
    let kitNumber = document.getElementById('playerkitnumber').value;
    let teamId = document.getElementById('playerteamid').value
    let value = document.getElementById('playervalue').value;

    fetch('http://localhost:15885/player', {
        method: 'POST',
        headers: {
        'Content-Type': 'application/json',},
        body: JSON.stringify(
            {
                birthDate: birthDate,
                kitNumber: kitNumber,
                name: name,
                teamId: teamId,
                value: value

            }),})
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {console.error('Error:', error);
        })
}

function defaultValuesToLoad() {
    document.getElementById('playername').placeholder = "Name";
    document.getElementById('playerbirthdate').placeholder = "Bith date format: DD/MM/YYYY";
    document.getElementById('playerkitnumber').placeholder = "Kit Number (1-99)";
    document.getElementById('playerteamid').placeholder = "Team ID (1-" + teams.length + ")";
    document.getElementById('playervalue').placeholder = "Value";
}