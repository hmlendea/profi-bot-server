using NuciLog.Core;

namespace ProfiBotServer.Logging
{
    public sealed class MyOperation : Operation
    {
        MyOperation(string name) : base(name) { }

        public static Operation RecordPrize => new MyOperation(nameof(RecordPrize));

        public static Operation SmtpNotification => new MyOperation(nameof(SmtpNotification));
    }
}
