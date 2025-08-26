namespace chat_realtime_backend.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; } // Ideia é que depois possa ser expandido para sala
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
