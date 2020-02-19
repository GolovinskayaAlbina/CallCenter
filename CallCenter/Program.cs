using System;
using System.Threading;
using System.Threading.Tasks;

namespace CallCenter
{
    class Program
    {
        static void Main(string[] args)
        {
            var center = new CallCenter(new Employees(
                SettingsProvider.OperatorsCount,
                SettingsProvider.ManagersCount,
                SettingsProvider.DirectorsCount), new CallLine());

            var number = 0;
            while (number < 300)
            {
                var order = ++number;
                Task.Factory.StartNew(() => center.HandleCallAsync(new Call(order)));
                Thread.Sleep(SettingsProvider.CallPeriodInMilliseconds);
            }
            Console.ReadLine();
        }
    }
}
