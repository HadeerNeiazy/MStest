using Castle.MicroKernel.Registration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MStest.Areas.Identity.Data;
using MStest.Data;
using MStest.Data.Entities;
using MStest.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace MStest.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IChatRepository chatRepository;
        private readonly MStestContext _mStestContext;
        private readonly HttpContextAccessor _httpContext;
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();
        private readonly static List<ChatUser> users = new List<ChatUser>();
        public ChatHub(ILogger<ChatHub> logger, MStestContext mStestContext, UserManager<ApplicationUser> userManager, IChatRepository chatRepository)
        {
            _logger = logger;
            _userManager = userManager;
            this.chatRepository = chatRepository;
            _mStestContext = mStestContext;
            //_httpContext = httpContext;
        }

        //public async Task SendMessage(string senderId, string receiverId, string message)
        //{
        //    _logger.LogInformation($"{senderId} sent message: {message}");

        //    // get the ApplicationUser objects for the sender and receiver
        //    var sender = await _userManager.FindByIdAsync(senderId);
        //    var receiver = await _userManager.FindByIdAsync(receiverId);

        //    // get the Doctor and Patient objects for the sender and receiver
        //    var senderDoctor = await _mStestContext.Doctors.SingleOrDefaultAsync(d => d.ApplicationUserId == sender.Id);
        //    var senderPatient = await _mStestContext.Patients.SingleOrDefaultAsync(p => p.ApplicationUserId == sender.Id);
        //    var receiverDoctor = await _mStestContext.Doctors.SingleOrDefaultAsync(d => d.ApplicationUserId == receiver.Id);
        //    var receiverPatient = await _mStestContext.Patients.SingleOrDefaultAsync(p => p.ApplicationUserId == receiver.Id);

        //    // send the message to the receiver
        //    if (senderDoctor != null && receiverPatient != null)
        //    {
        //        await Clients.User(receiver.Id).SendAsync("ReceiveMessage", senderDoctor.Description, message);
        //    }
        //    else if (senderPatient != null && receiverDoctor != null)
        //    {
        //        await Clients.User(receiver.Id).SendAsync("ReceiveMessage", senderPatient.Medicines, message);
        //    }
        //}
        public string GetCurrentUserId()
        {
            var user = _httpContext.HttpContext.User.Identity.Name;
            return user;
        }

        public async Task SendMessage(string message, string recieverName, string uploadedFile)
        {
            if (uploadedFile != null)
            {

                // remove the data uri part if present
                uploadedFile = uploadedFile.Split(",")[1];
                // convert base64 string to byte array
                var fileBytes = Convert.FromBase64String(uploadedFile);
                // generate a file name with extension based on the content type
                using (MemoryStream ms = new MemoryStream(fileBytes))
                {
                    var bytes = ms.ToArray();
                    var fileNmae = $"{Guid.NewGuid()}.png";
                    File.WriteAllBytes($"./wwwroot/img/{fileNmae}", bytes);
                    message = $"<img class=\"img-fluid\" style=\"width: 200px;margin-top: 5px\" src=\"/img/{fileNmae}\" alt=\"user img\">";
                }

            }


            var senderName = Context.User.Identity.Name;
            ChatMessage chatMessage = null;
            if (recieverName != senderName)
            {
                var sender = users.First(x => x.Name == senderName);
                var reciever = users.FirstOrDefault(x => x.Name == recieverName);

                if (reciever != null)
                {
                    await Clients.Client(reciever.ConnectionId).SendAsync("ReceiveMessage", Context.User.Identity.Name ?? "anonymous", message, "sender");
                    await Clients.Client(sender.ConnectionId).SendAsync("ReceiveMessage", Context.User.Identity.Name ?? "anonymous", message, "repaly");
                    chatMessage = new ChatMessage(sender.Id, reciever.Id, message);
                }
                else
                {
                    await Clients.Client(sender.ConnectionId).SendAsync("ReceiveMessage", Context.User.Identity.Name ?? "anonymous", message, "repaly");
                    var receiverUser = _userManager.Users.FirstOrDefault(x => x.UserName == recieverName);
                    chatMessage = new ChatMessage(sender.Id, receiverUser.Id, message);
                }

                await _mStestContext.ChatMessages.AddAsync(chatMessage);
                await _mStestContext.SaveChangesAsync();
            }
        }

        public override Task OnConnectedAsync()
        {
            var userId = _userManager.GetUserId(Context.User);
            if (!users.Any(x => x.Name == Context.User.Identity.Name) && Context.User.Identity.IsAuthenticated)
                users.Add(new ChatUser() { Name = Context.User.Identity.Name, ConnectionId = Context.ConnectionId, Id = userId });
            else
            {
                var user = users.First(x => x.Id == userId);
                user.ConnectionId = Context.ConnectionId;
            }

            return base.OnConnectedAsync();
        }

        public async Task DisplayOldMessages(string partnerId)
        {
            var messages = await chatRepository.GetChatMessages(partnerId);
            foreach (var message in messages)
            {
                var sender = users.FirstOrDefault(x => x.Id == message.SenderId);
                var reciever = users.FirstOrDefault(x => x.Id == message.ReceiverId);
                if (reciever != null)
                    await Clients.Client(reciever.ConnectionId).SendAsync("ReceiveMessage", Context.User.Identity.Name ?? "anonymous", message.Message, "sender");

                if (sender != null)
                    await Clients.Client(sender.ConnectionId).SendAsync("ReceiveMessage", Context.User.Identity.Name ?? "anonymous", message.Message, "repaly");

            }
        }

    }
}
