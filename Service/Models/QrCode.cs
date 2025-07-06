namespace ProfiBotServer.Service.Models
{
    public sealed class QrCode
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string Url => $"https://qr.profi.ro/checkin?l={Id}";

        public bool IsEnabled { get; set; }
    }
}
