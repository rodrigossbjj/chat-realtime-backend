
using Microsoft.AspNetCore.SignalR;
using System.Linq;

namespace chat_realtime_backend.Services
{
    public class NameUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            // Usar o claim "id" do JWT como UserIdentifier
            return connection.User?.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        }
    }
}
