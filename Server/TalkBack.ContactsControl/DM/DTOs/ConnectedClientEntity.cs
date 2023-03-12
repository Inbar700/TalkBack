namespace DM.DTOs
{
    public class ConnectedClientEntity
    {
        public Guid ChatClientId { get; set; }
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public ConnectedClientEntity(Guid chatClientId, string connectionId, string name)
        {
            ChatClientId = chatClientId;
            ConnectionId = connectionId;
            Name = name;
        }
        
    }
}
