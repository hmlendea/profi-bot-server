namespace ProfiBotServer.Service.Models
{
    public sealed class QrCode
    {
        public long Id { get; set; }

        public string Description { get; set; }

        public string Url => $"https://qr.profi.ro/checkin?l={Id}";
    }
}
