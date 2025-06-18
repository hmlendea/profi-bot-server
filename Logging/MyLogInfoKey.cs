using NuciLog.Core;

namespace ProfiBotServer.Logging
{
    public sealed class MyLogInfoKey : LogInfoKey
    {
        MyLogInfoKey(string name) : base(name) { }

        public static LogInfoKey UserId => new MyLogInfoKey(nameof(UserId));

        public static LogInfoKey PrizeId => new MyLogInfoKey(nameof(PrizeId));
    }
}
