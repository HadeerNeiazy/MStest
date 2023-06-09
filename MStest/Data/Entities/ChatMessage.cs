using Microsoft.VisualBasic;
using MStest.Areas.Identity.Data;
using System;

namespace MStest.Data.Entities
{
    public class ChatMessage
    {
        public ChatMessage(string senderId, string receiverId, string message)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
            Message = message;
        }

        public int Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now.AddHours(2);
        public string Message { get; set; }
        public ApplicationUser Sender { get; set; }
        public ApplicationUser Receiver { get; set; }
    }
}
