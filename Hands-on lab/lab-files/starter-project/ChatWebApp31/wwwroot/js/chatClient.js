"use strict";
$(document).ready(function () {

    // This check is not really necessary as SignalR will first try WebSockets then Server Side Events and finally Long Polling
    // as a fallback mechanism.  This alert is just to inform you are not going to get the full experience in your current browser.
    if (!Modernizr.websockets) {
        alert("This browser doesn't support HTML5 Web Sockets! Falling back to long polling technique.");
    }

    var connection;
    var sessionId = '';
    var username = '';

    $("#btnJoin").click(function () {
        username = $("#username").val();

        if (username.length > 0) {
            $("#joinChatPanel").fadeOut();
            $("#chatPanel").fadeIn();
            $("#divHistory").empty();

            // Send initial message to Event Hub.
            openConnection();
        }
        else {
            alert('Please enter a username');
        }

    });


    function openConnection() {

        // configureLogging options:
        //signalR.LogLevel.Error: Error messages.Logs Error messages only.
        //signalR.LogLevel.Warning: Warning messages about potential errors.Logs Warning, and Error messages.
        //signalR.LogLevel.Information: Status messages without errors.Logs Information, Warning, and Error messages.
        //signalR.LogLevel.Trace: Trace messages.Logs everything, including data transported between hub and client.
        //signalR.LogLevel.Debug: Debug messages.Logs debug message, including data transported between hub and client.

        connection = new signalR.HubConnectionBuilder()
            .withUrl('/chatHub')
            .configureLogging(signalR.LogLevel.Debug)
            .withAutomaticReconnect()
            .build();

        // Set up the client-side SignalR event handlers

        // This event name has to match the event found in the ASP.NET ChatHub class.
        // Serialization is handled automatically.
        connection.on('ReceiveMessage', function (message) {
            console.log('ReceiveMessage was called.', message)
            receiveChatMessage(message);
        });

        connection.on('HubConnected', function (message) {
            console.log('HubConnected was called.')                
        });

        // Open the TCP socket connection to ChatHub.
        // This event handler needs to be last in order to make sure you are not missing any messages.
        connection.start().then(() => {
            joinChatSession();
            $('#divHistory').append('Connected to the chat service...');
            console.log('connected');
        });
    }


    function joinChatSession() {
        $("#chat-room").text($("#listChatRooms").children(':selected').text());
        sessionId = $("#listChatRooms").val();
        username = $("#username").val();

        sendMessage(sessionId, username, username + ' joined chat session.', messageType.JOIN);
        connection.invoke('RecieveMessage').catch(err => console.error(err));
    }


    function sendMessage(sessionId, username, message, messageType) {

        // This class has to match the JS client type, ChatHub message type, and function message type.
        var chatMessage = {
            'message': message,
            'username': username,
            'sessionId': sessionId,
            'messageType': messageType,
            'createDate': '',
            'score': -1.0
        };

        console.log(chatMessage);
        // This method has to match the method found in the ASP.NET ChatHub class.
        connection.invoke('SendMessage', JSON.stringify(chatMessage)).catch(err => console.error(err));
        $('#txtMsg').val('');
    }


    // Sending messages to ChatHub and then to Azure Event Hub.
    document.getElementById("btnSend").addEventListener("click", function (event) {
        var message = document.getElementById("txtMsg").value;

        if (username.toLocaleLowerCase() === 'hotellobby') {
            sendMessage(sessionId, username, message, messageType.ACK);
        } else {
            sendMessage(sessionId, username, message, messageType.CHAT);
        }
        
        event.preventDefault();
    });


    // Function handles the SignalR event raised from ChatHub.cs in the web app.
    function receiveChatMessage(jsonMessage) {
        var chatMessage = JSON.parse(jsonMessage);

        var chatHistory = $('#chat');
        var htmlChatBubble = '';

        htmlChatBubble = getChatMessageItem(chatMessage);

        if (chatMessage.score >= 0.5) {
            htmlChatBubble += '<p><span class="fas fa-thumbs-up" style="color:#19b321;"></span>&nbsp;';
        }
        else if (chatMessage.score > 0) {
            htmlChatBubble += '<p><span class="fas fa-thumbs-down" style="color:#eb4034;"></span>&nbsp;';
        } else {
            htmlChatBubble += '<p>'
        }

        if (chatMessage.messageType !== messageType.JOIN) {
            htmlChatBubble += '<span class="font-weight-bold">' + chatMessage.username + '</span> says: ';
        }

        htmlChatBubble += chatMessage.message + '</p>';
        htmlChatBubble += '</div></li>';

        chatHistory.append(htmlChatBubble);
    }


    /*
     * A better way to handle HTML view creation is to use a JavaScript framework like VueJS, Angular, ReactJS. The focus of this 
     * lab is on the Azure technology stack.
     */

    function getChatMessageItem(chatMessage) {
        var htmlChatBubble = '<li class="list-group-item"><span class="p0">';
        htmlChatBubble += '       <div class="d-flex">';
        htmlChatBubble += '            <div class="pl-0">';
        htmlChatBubble += '                  <div class="d-flex w-100 justify-content-between text-align-left">';
        htmlChatBubble += '                      <icon class="fas fa-clock mr-2"></icon> <small class="bold text-muted time">  ' + moment(chatMessage.createDate).format('h:mm:ss a') + ' </small>';
        htmlChatBubble += '                  </div>';
        htmlChatBubble += '            </div>';
        htmlChatBubble += '       </div >';
        return htmlChatBubble;
    }


    const messageType = {
        ACK: 'ack',
        BOT: 'bot',
        JOIN: 'join',
        CHAT: 'chat'
    }

});