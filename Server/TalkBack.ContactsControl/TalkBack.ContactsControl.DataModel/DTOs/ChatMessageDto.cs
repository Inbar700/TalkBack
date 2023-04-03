namespace DM.DTOs
{
    public class ChatMessageDto
    {
        public string ConnectionId { get; set; }
        public string Message { get; set; }
        public string? FromUser { get; set; }

        public bool Validate()
        {
            return !string.IsNullOrWhiteSpace(ConnectionId) && !string.IsNullOrWhiteSpace(Message);
        }
        public bool ValidateUsers()
        {
            return !string.IsNullOrWhiteSpace(ConnectionId) && !string.IsNullOrWhiteSpace(FromUser);
        }
    }
}
