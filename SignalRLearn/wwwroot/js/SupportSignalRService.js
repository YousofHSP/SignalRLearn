let supportConnection = new signalR.HubConnectionBuilder()
    .withUrl("/supporthub")
    .build();

function init() {
    supportConnection.start();
}
$(document).ready(function(){
    init();
})

supportConnection.on("GetRooms", loadRooms)


function loadRooms(rooms) {
    if(!rooms) return;
    let roomIds = Object.keys(rooms)
    if(!rooms) return;
    
    removeAllChildren(roomListEl)
    
    roomIds.forEach((id) => {
        let roomInfo = rooms[id]
        if(!roomInfo) return;
        
        return $("#roomList").append(`<a class='list-group-item list-group-item-action d-flex justify-content-between align-items-center' data-id="${id}" href="#">${roomInfo}</a>`)
    })
    
}

let  roomListEl = $("#roomList");

function removeAllChildren(node) {
    if(!node) reutrn;
    node.html(null);
}