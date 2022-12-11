using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DTOs
{
    public class NewChatClientConnectionDto
    {
        public Guid ChatClientId { get; set; }
        public string ConnectionId { get; set; }
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
