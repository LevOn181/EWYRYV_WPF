let arr = [];

const defaultDescription = "Here you will see the description of the chosen stat query...";
const defaultContent = "Here you will see the results...";

async function countPlayers() {
     await fetch('http://localhost:15885/stat/countPlayers')
        .then(x => x.json())
        .then(y => {
            arr = y;
            console.log(arr)
            displayCountPlayers(arr)
        });
}

async function teamValue() {
    await fetch('http://localhost:15885/Stat/teamValue')
        .then(x => x.json())
        .then(y => {
            arr = y;
            console.log(arr)
            displayTeamValue(arr)
        });
}
async function mostValuable() {
    await fetch('http://localhost:15885/Stat/mostValuable')
        .then(x => x.json())
        .then(y => {
            arr = y;
            console.log(arr)
            displayMostValuable(arr)
        });
}

async function hungarianManagers() {
    await fetch('http://localhost:15885/Stat/hungarianManagers')
        .then(x => x.json())
        .then(y => {
            arr = y;
            console.log(arr)
            displayHungarianManagers(arr)
        });
}

async function hungarianManagers() {
    await fetch('http://localhost:15885/Stat/hungarianManagers')
        .then(x => x.json())
        .then(y => {
            arr = y;
            console.log(arr)
            displayHungarianManagers(arr)
        });
}
async function topPlayerData() {
    await fetch('http://localhost:15885/Stat/topPlayerData')
        .then(x => x.json())
        .then(y => {
            arr = y;
            console.log(arr)
            displayTopPlayerData(arr)
        });
}
function displayTopPlayerData(array){
    document.getElementById("statTable").innerHTML = "";
    document.getElementById("statTable").innerHTML += "<thead><tr><th>Player</th><th>Team</th><th>Manager</th></tr><thead><tbody>";
    array.forEach(t => {
        document.getElementById('statTable').innerHTML +=
            "<tr>"
            + "<td>" + t.playerName + "</td>"
            + "<td>" + t.teamName + "</td>"
            + "<td>" + t.managerName + "</td>"
            + "</tr>"
    });
    document.getElementById("statTable").innerHTML += "</tbody>";
}

function displayHungarianManagers(array) {
    document.getElementById("statTable").innerHTML = "";
    document.getElementById("statTable").innerHTML += "<thead><tr><th>Team Name</th><th>Manager Name</th></tr><thead><tbody>";
    array.forEach(t => {
        document.getElementById('statTable').innerHTML +=
            "<tr>"
            + "<td>" + t.teamName + "</td>"
            + "<td>" + t.managerName + "</td>"
            + "</tr>"
    });
    document.getElementById("statTable").innerHTML += "</tbody>";
}
function displayMostValuable(array) {
    document.getElementById("statTable").innerHTML = "";
    document.getElementById("statTable").innerHTML += "<thead><tr><th>Name</th><th>Manager</th></tr><thead><tbody>";
    array.forEach(t => {
        document.getElementById('statTable').innerHTML +=
            "<tr>"
            + "<td>" + t.playerName + "</td>"
            + "<td>" + t.managerName + "</td>"
            + "</tr>"
    });
    document.getElementById("statTable").innerHTML += "</tbody>";
}


function displayCountPlayers(array) {
    document.getElementById("statTable").innerHTML = "";
    document.getElementById("statTable").innerHTML += "<thead><tr><th>Team Name</th><th>Player Count</th></tr><thead><tbody>";
    array.forEach(t => {
        document.getElementById('statTable').innerHTML +=
            "<tr>"
            + "<td>" + t.teamName + "</td>"
            + "<td>" + t.playerCount + "</td>"
            + "</tr>"
    });
    document.getElementById("statTable").innerHTML += "</tbody>";
}

function displayTeamValue(array){
    document.getElementById("statTable").innerHTML = "";
    document.getElementById("statTable").innerHTML += "<thead><tr><th>Team Name</th><th>Total Value</th></tr><thead><tbody>";
    array.forEach(t => {
        document.getElementById('statTable').innerHTML +=
            "<tr>"
            + "<td>" + t.teamName + "</td>"
            + "<td>" + t.teamValue + "</td>"
            + "</tr>"
    });
    document.getElementById("statTable").innerHTML += "</tbody>";
}

function showSelection() {
        let statSelect = document.getElementById('statSelection');
        let selectedStat = statSelect.options[statSelect.selectedIndex].value;
    if (selectedStat == "countPlayers") {
        countPlayers();
    } else if (selectedStat == "teamValue") {
        teamValue();
    } else if (selectedStat == "mostValuable") {
        mostValuable();
    } else if (selectedStat == "hungarianManagers") {
        hungarianManagers();
    } else if (selectedStat == "topPlayerData") {
        topPlayerData();
    }
}