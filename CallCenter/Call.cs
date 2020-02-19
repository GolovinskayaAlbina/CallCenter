using System;
using System.Threading.Tasks;

namespace CallCenter
{
    class Call
    {
        public int Order { get; private set; }
        public int DurationInMilliseconds { get; private set; }

        public Call(int order)
        {
            Order = order;
            DurationInMilliseconds = SettingsProvider.CallDurationInMilliseconds;
        }
       
        public async Task AnswerAsync(IEmployee employee)
        {
            Console.WriteLine($"{employee.Type} {employee.Order}: answer the call number {Order}");
            await Task.Delay(DurationInMilliseconds);
        }
    }
}
