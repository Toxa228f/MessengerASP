const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

connection.start().catch(function (err) {
    return console.error(err.toString())
});


/*connection.on("ReceiveMessageToAll", function (user, message, data) {

    let msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");

    dt = new Date(data);

    let newMessage = CreateMessage(user, message, dt);

    document.getElementById("messages").appendChild(newMessage);

});


document.getElementById("sendAll").addEventListener("click", function (event) {
    let user = document.getElementById("userInput").value;
    let message = document.getElementById("messageInput").value;

    let data = new Date();
    connection.invoke("SendMessageAll", user, message, data.toString()).catch(function (err) { return console.error(err.toString()) });
    event.preventDefault();
});*/


connection.on("ReceiveMessageToGroup", function (user, message, data, groopName) {

    let msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");

    dt = new Date(data);

    let newMessage = CreateMessage(user, message, dt);

    document.getElementById("messages").appendChild(newMessage);

});


document.getElementById("sendGroup").addEventListener("click", function (event) {
    let user = @Model.Item2.UserName;
    let message = document.getElementById("messageInput").value;
    let groopName = @Model.Item2.ChatName;
    let data = new Date();
    connection.invoke("ReceiveMessageToGroup", user, message, data.toString(), groopName).catch(function (err) { return console.error(err.toString()) });
    event.preventDefault();
});




function CreateMessage(user, message, date) {
    let newMessage = document.createElement("span");

    let msg = document.createElement("div");
    msg.classList.add("user-group-message");

    let messageBr = document.createElement("br");

    let name = document.createElement("span");
    name.classList.add("user-group-message-name");
    name.innerHTML = "<b>" + user + "</b>";

    let time = document.createElement("span");
    time.classList.add("user-group-message-time");
    time.innerText = date.getHours().toString() + ":" + date.getMinutes().toString();

    let text = document.createElement("span");
    text.classList.add("user-group-message-text");
    text.innerText = message;
    msg.append(name, time, text);
    newMessage.append(messageBr, msg);

    return newMessage;
}

