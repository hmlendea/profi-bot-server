using System;

namespace ProfiBotServer.Service.Models
{
    public sealed class Prize
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
