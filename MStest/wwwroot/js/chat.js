"use strict";
var reciever = ""
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established  
document.getElementById("sendBtn").disabled = true;

connection.on("ReceiveMessage", function (user, message, type) {
    AppendMessage(message, user, type);
});

connection.start().then(function () {
    document.getElementById("sendBtn").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendBtn").addEventListener("click", function (event) {
    var message = document.getElementById("messageInput").value;
    var uploadedFile = document.getElementById("messageFile").files[0];

    if (uploadedFile == undefined) {
        connection.invoke("SendMessage", message, localStorage.getItem("reciever"),null).catch(function (err) {
            return console.error(err.toString());
        });
    } else {
        // call getBase64 and pass a callback function that will be executed when getBase64 is done
        getBase64(uploadedFile, function (base64) {
            console.log(base64)
            // inside the callback, you can use the base64 string
            connection.invoke("SendMessage", message, localStorage.getItem("reciever"), base64).catch(function (err) {
                return console.error(err.toString());
            });
        });
    }
    
    event.preventDefault();
});

function AppendMessage(message, user, type) {
    var msg = message.replace(/&/g, "&").replace(/</g, "<").replace(/>/g, ">");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.className = type;
    li.innerHTML = '<p>' + msg + '</p ><span class="time">' + getCurrentTime() + '</span>'
    document.getElementById("messagesList").appendChild(li);
}

function getCurrentTime() {
    var now = new Date();
    var hours = now.getHours();
    var minutes = now.getMinutes();

    // Pad the minutes with a leading 0 if necessary
    minutes = (minutes < 10) ? "0" + minutes : minutes;

    // Convert the hour to 12-hour format
    hours = (hours > 12) ? hours - 12 : hours;

    return hours + ":" + minutes + " " + (hours >= 12 ? "PM" : "AM");
}

function SetActiveConnection(receiverEmail, receiverId, userName,image) {
    debugger
    localStorage.setItem("reciever", receiverEmail)
    localStorage.setItem("userName", userName)
    localStorage.setItem("image", image)
    if (window.location.href != "https://localhost:44370/home/chat#") {
        window.location.href = "/home/chat#";
    } else {
        displayActiveUser(userName, image)
    }

    GetChatMessages(receiverId)
}

function GetChatMessages(partnerId) {
    //$.ajax({
    //    url: "/home/getChatMessages",
    //    type: "GET",
    //    data: {
    //        partnerId: partnerId
    //    },
    //    success: function (result) {
    //        console.log(result)
    //        // Handle the success response.
    //    },
    //    error: function (error) {
    //        // Handle the error response.
    //    }
    //});
    connection.invoke("DisplayOldMessages", partnerId).catch(function (err) {
        return console.error(err.toString());
    });
}

function displayActiveUser(activeUserName, image) {

    const activeUserDiv = document.getElementById("activeUser");
    const activeUserNameElement = document.getElementById("activeUserName");
    activeUserNameElement.textContent = activeUserName;

    const activeUserImageElement = document.getElementById("activeUserImage");
    activeUserImageElement.setAttribute("src", image)

    activeUserDiv.style.display = "block";
}

function getBase64(file, callback) {
    var reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = function () {
        callback(reader.result); // pass the base64 string to the callback function
    };
    reader.onerror = function (error) {
        console.log('Error: ', error);
    };
}

$(document).ready(function () {
    if (localStorage.getItem("userName")!=null)
        displayActiveUser(localStorage.getItem("userName"), localStorage.getItem("image"))
});