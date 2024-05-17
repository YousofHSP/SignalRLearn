let chatBox = $("#ChatBox");
let connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();

connection.start();

// connection.invoke("SendNewMessage", "بازدید کننده", "این یک پیام حدید از سمت کلاینت است");
function showChatDialog() {
    chatBox.css("display", "block")
}

function init(){
    setTimeout(showChatDialog, 1000);
    
    let newMessageForm = $("#NewMessageForm");
    newMessageForm.on("submit", function (e) {
        e.preventDefault();
        let message = e.target[0].value;
        e.target[0].value = '';
        sendMessage(message);
    })
    
}

// send message to server
function sendMessage(text) {
    connection.invoke('SendNewMessage',"بازدید کننده", text);
}

//receive message from server
connection.on("receiveNewMessage", receiveNewMessage);

function receiveNewMessage(sender, message, time) {
    $("#Messages").append(`<li><div><span class="name">${sender}</span><span class="time">${time}</span></div><div class="message">${message}</div></li>`)
}
$(document).ready(function(){
    init()
})