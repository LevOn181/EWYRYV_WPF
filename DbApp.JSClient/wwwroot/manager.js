let managers = [];
let connection = null;
getdata();
setupSignalR();
defaultValuesToLoad();

let managerIdToUpdate = -1;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:15885/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("ManagerCreated", (user, message) => {
        getdata();
    });

    connection.on("ManagerDeleted", (user, message) => {
        getdata();
    });
    connection.on("ManagerUpdated", (user, message) => {
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
    await fetch('http://localhost:15885/manager')
        .then(x => x.json())
        .then(y => {
            managers = y;
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    managers.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr>"
            + "<td>" + t.managerId + "</td>"
            + "<td>" + t.name + "</td>"
            + "<td>" + t.nationality + "</td>"
            + "<td>" + t.teamId + "</td>"
            + "<td>"
            + `<button type="button" onclick="remove(${t.managerId})">Delete</button>`
            + `<button type="button" onclick="showupdate(${t.managerId})">Update</button>`
            + "</td>"
            + "</tr>"
    });
}
function remove(id) {
    fetch('http://localhost:15885/manager/' + id, {
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

    fetch('http://localhost:15885/manager', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                managerId: managerIdToUpdate,
                name: name,
                nationality: nationality,
                teamId: teamId

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
    document.getElementById('managernameupdate').value = managers.find(t => t['managerId'] == id)['name'];
    document.getElementById('managernationalityupdate').value = managers.find(t => t['managerId'] == id)['nationality'];
    document.getElementById('managerteamidupdate').value = managers.find(t => t['managerId'] == id)['teamId'];
    document.getElementById('formdiv').style.display = 'none'
    document.getElementById('updateformdiv').style.display = 'flex';
    managerIdToUpdate = id;
}


function create() {
    let managername = document.getElementById('managername').value;
    let nationality = document.getElementById('managernationality').value;
    let teamId = document.getElementById('managerteamid').value;

    fetch('http://localhost:15885/manager', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                name: managername,
                nationality: nationality,
                teamId: teamId

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
    document.getElementById('managername').placeholder = "Name";
    document.getElementById('managernationality').placeholder = "Nationality";
    document.getElementById('managerteamid').placeholder = "Team ID";
}