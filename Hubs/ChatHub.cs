using chat_realtime_backend.Services;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;

namespace chat_realtime_backend.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatService _chatService;

        public ChatHub(ChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task SendMessage(string message)
        {
            // Pegando o ID do usuário a partir do JWT
            var userIdClaim = Context.User.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim == null) return;

            int userId = int.Parse(userIdClaim.Value);

            // Salvar mensagem no banco (receiverId pode ser null para teste)
            await _chatService.SaveMessage(userId, 0, message);

            // Enviar para todos conectados
            await Clients.All.SendAsync("ReceiveMessage", userId, message);
        }
    }
}
