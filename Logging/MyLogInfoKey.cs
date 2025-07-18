using NuciLog.Core;

namespace ProfiBotServer.Logging
{
    public sealed class MyLogInfoKey : LogInfoKey
    {
        MyLogInfoKey(string name) : base(name) { }

        public static LogInfoKey Balance => new MyLogInfoKey(nameof(Balance));

        public static LogInfoKey PrizeId => new MyLogInfoKey(nameof(PrizeId));

        public static LogInfoKey QrCodeId => new MyLogInfoKey(nameof(QrCodeId));

        public static LogInfoKey UserId => new MyLogInfoKey(nameof(UserId));

        public static LogInfoKey Recipient => new MyLogInfoKey(nameof(Recipient));
    }
}
