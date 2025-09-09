using chat_realtime_backend.Models;
using chat_realtime_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chat_realtime_backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly ChatService _chatService;

        public MessagesController(ChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("{receiverId}")]
        public async Task<IActionResult> GetMessages(int receiverId)
        {
            var senderId = int.Parse(User.FindFirst("id").Value);

            var messages = await _chatService.GetMessages(senderId, receiverId);

            return Ok(messages);
        }
    }
}
