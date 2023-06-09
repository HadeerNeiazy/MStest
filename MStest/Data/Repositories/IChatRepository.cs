using MStest.Areas.Identity.Data;
using MStest.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MStest.Data.Repositories
{
    public interface IChatRepository
    {
        Task<List<ApplicationUser>> GetChatConnections();
        Task<List<ChatMessage>> GetChatMessages(string partnerId);
    }
}
