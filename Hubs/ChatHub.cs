using chat_realtime_backend.Services;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace chat_realtime_backend.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatService _chatService;

        public ChatHub(ChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task SendMessage(int receiverId, string message)
        {
            var senderIdClaim = Context.User.Claims.FirstOrDefault(c => c.Type == "id");
            if (senderIdClaim == null) return;

            int senderId = int.Parse(senderIdClaim.Value);

            // Salvar mensagem no banco
            await _chatService.SaveMessage(senderId, receiverId, message);

            await Clients.User(receiverId.ToString())
                .SendAsync("ReceiveMessage", senderId, message);

            await Clients.Caller.SendAsync("ReceiveMessage", senderId, message);
        }

    }
}
