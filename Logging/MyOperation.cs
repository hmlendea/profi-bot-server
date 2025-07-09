using NuciLog.Core;

namespace ProfiBotServer.Logging
{
    public sealed class MyOperation : Operation
    {
        MyOperation(string name) : base(name) { }

        public static Operation RecordPrize => new MyOperation(nameof(RecordPrize));

        public static Operation AddQrCode => new MyOperation(nameof(AddQrCode));

        public static Operation GetBalance => new MyOperation(nameof(GetBalance));

        public static Operation GetQrCode => new MyOperation(nameof(GetQrCode));

        public static Operation SmtpNotification => new MyOperation(nameof(SmtpNotification));
    }
}
