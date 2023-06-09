using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MStest.Areas.Identity.Data;
using MStest.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MStest.Data.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly MStestContext mStestContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ChatRepository(MStestContext mStestContext, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.mStestContext = mStestContext;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<ApplicationUser>> GetChatConnections()
        {
            var user = userManager.GetUserId(httpContextAccessor.HttpContext.User);
            var connections = await mStestContext.ChatMessages
                   .Where(chatMessage => chatMessage.SenderId == user || chatMessage.ReceiverId == user)
                   .Select(chatMessage => chatMessage.SenderId!=user? chatMessage.Sender: chatMessage.Receiver)
                   .ToListAsync();

            return connections.Distinct().ToList();
        }

        public async Task<List<ChatMessage>> GetChatMessages(string partnerId)
        {
            var user = userManager.GetUserId(httpContextAccessor.HttpContext.User);
            var x= await mStestContext.ChatMessages.Where(chatMessage => (chatMessage.SenderId == user && chatMessage.ReceiverId == partnerId)|| (chatMessage.SenderId == partnerId && chatMessage.ReceiverId == user)).ToListAsync();
            return x;
        }
    }
}
