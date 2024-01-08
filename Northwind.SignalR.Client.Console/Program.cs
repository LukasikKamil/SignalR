using Microsoft.AspNetCore.SignalR.Client; // HubConnection
using Northwind.Chat.Models; // UserModel, MessageModel

Write("Enter a username (required): ");
string? username = ReadLine();

if(string.IsNullOrEmpty(username))
{
    WriteLine("You must enter a username to register with chat!");
    return;
}

Write("Enter your group (optional): ");
string? groups = ReadLine();

HubConnection hubConnection = new HubConnectionBuilder()
    .WithUrl("https://localhost:5131/chat")
    .Build();

hubConnection.On<MessageModel>("ReceiveMessage", message =>
{
    WriteLine($"To {message.To}, From {message.From}: {message.Body}");
});

await hubConnection.StartAsync();

WriteLine("Successfully started.");

UserModel registration = new()
{
    Name = username,
    Groups = groups
};

await hubConnection.InvokeAsync("Register", registration);

WriteLine("Successfully registered.");

// New code for sending messages
while(true)
{
    Write("Enter recipient (optional): ");
    string? recipient = ReadLine();

    Write("Enter message (required): ");
    string? messageBody = ReadLine();

    if(string.IsNullOrEmpty(messageBody))
    {
        WriteLine("Message is required to send a message!");
    }
    else
    {
        MessageModel message = new()
        {
            From = username,
            To = string.IsNullOrEmpty(recipient) ? "Everyone" : recipient,
            Body = messageBody
        };

        await hubConnection.InvokeAsync("SendMessage", message);
        WriteLine("Message sent.");
    }

    Write("Do you want to send another message? (y/n): ");
    string? answer = ReadLine();

    if(answer?.ToLower() != "y")
    {
        break;
    }
}
