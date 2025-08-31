using chat_realtime_backend.Data;
using chat_realtime_backend.Models;
using System;
using System.Threading.Tasks;

namespace chat_realtime_backend.Services
{
    public class ChatService
    {
        private readonly AppDbContext _context;

        public ChatService(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveMessage(int senderId, int receiverId, string content)
        {
            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content,
                Timestamp = DateTime.UtcNow
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }
    }
}
