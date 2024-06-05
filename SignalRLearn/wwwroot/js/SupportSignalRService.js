let activeRoomId = '';

let supportConnection = new signalR.HubConnectionBuilder()
    .withUrl("/supporthub")
    .build();
let chatConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

function init() {   
    supportConnection.start();
    chatConnection.start();
}
$(document).ready(function(){
    init();
})

chatConnection.on("receiveNewMessage", showMessage)

supportConnection.on('getNewMessage', addMessages);

function addMessages(messages) {
    if(!messages) return;
    messages.forEach((m) => {
        showMessage(m.sender, m.message, m.time);
    })
}

function showMessage(sender, message, time) {
    $("#chatMessage").append(`<li><div><span class="name">${sender}</span><span class="time">${time}</span></div><div class="message">${message}</div></li>`)
}

supportConnection.on("GetRooms", loadRooms)


function loadRooms(rooms) {
    if(!rooms) return;
    let roomIds = Object.keys(rooms)
    
    console.log(roomIds)
    removeAllChildren(roomListEl)
    roomIds.forEach((id) => {
        console.log(id)
        let roomInfo = rooms[id]
        if(!roomInfo) return;
        
        $("#roomList").append(`<a class='list-group-item list-group-item-action d-flex justify-content-between align-items-center' data-id="${roomInfo}" href="#">${roomInfo}</a>`)
    })
    
}

let  roomListEl = document.getElementById("roomList");
let roomMessageEl = document.getElementById("chatMessage");

function removeAllChildren(node) {
    if(!node) return;
    while (node.lastChild) {
        node.removeChild(node.lastChild);
    }
}

function setActiveRoomButton(el) {
    let allButtons = roomListEl.querySelectorAll('a.list-group-item');
    allButtons.forEach(btn => {
        btn.classList.remove('active')
    })
    el.classList.add('active');
}

function switchActiveRoomTo(id) {
    if(id === activeRoomId) return;
    removeAllChildren(roomMessageEl)
    if(activeRoomId)
        chatConnection.invoke("LeaveRoom", activeRoomId)
    activeRoomId = id;
    chatConnection.invoke("JoinRoom", activeRoomId)
    supportConnection.invoke('LoadMessage', activeRoomId);
    
}

roomListEl.addEventListener('click', function(e){
    roomMessageEl.style.display = 'block';
    setActiveRoomButton(e.target);
    let roomId = e.target.getAttribute('data-id');
    switchActiveRoomTo(roomId)
})