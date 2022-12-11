using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DTOs
{
    public class ChatMessageDto
    {
        public string ConnectionId { get; set; }
        public string Message { get; set; }

        public bool Validate()
        {
            return !string.IsNullOrWhiteSpace(ConnectionId) && !string.IsNullOrWhiteSpace(Message);
        }
    }
}
