namespace DM.DTOs
{
    public class NewChatClientConnectionDto
    {
        public Guid ChatClientId { get; set; }
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public bool Validate()
        {
            return !string.IsNullOrWhiteSpace(ConnectionId);
        }
        public override string ToString()
        {
            return $"ChatClientId: {ChatClientId}, ConnectionId: {ConnectionId}";
        }
    }
}
