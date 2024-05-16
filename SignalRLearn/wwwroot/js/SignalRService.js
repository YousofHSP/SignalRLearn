let chatBox = $("#ChatBox");
let connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();

connection.start();

// connection.invoke("SendNewMessage", "بازدید کننده", "این یک پیام حدید از سمت کلاینت است");
function showChatDialog() {
    chatBox.css("display", "block")
}

function init(){
    setTimeout(showChatDialog, 1000);
    
}
$(document).ready(function(){
    init()
})