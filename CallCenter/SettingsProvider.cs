using System;

namespace CallCenter
{
    class SettingsProvider
    {
        private static Random random = new Random(new DateTime().Millisecond);

        public static int CallDurationInMilliseconds => random.Next(1000, 2000);
        public static int CallPeriodInMilliseconds => random.Next(100, 200);
        public static int OperatorsCount => 4;
        public static int ManagersCount => 2;
        public static int DirectorsCount => 1;
    }
}
