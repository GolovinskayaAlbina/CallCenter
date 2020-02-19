using System;
using System.Threading.Tasks;

namespace CallCenter
{
    class CallCenter
    {
        private readonly object _locker = new object();
        private readonly Employees _employees;
        private readonly CallLine _callLine;

        public CallCenter(Employees employees, CallLine callLine)
        {
            _employees = employees;
            _callLine = callLine;
        }

        public async Task HandleCallAsync(Call call)
        {
            IEmployee employee;
            if (_employees.TryGetAvailable(out employee))
            {
                await HandleCallAsync(call, employee);
            }
            else
            {
                lock (_locker)
                {
                    _callLine.Enqueue(call);

                    Console.WriteLine($"Call {call.Order} position number {_callLine.Count}, " +
                        $"waiting ~ {_callLine.TotalWaitingDurationInMilliseconds / _employees.TotalCount / 1000} sec");
                }
            }
            await CheckCallLineAsync();
        }

        private async Task CheckCallLineAsync()
        {
            Call call;
            IEmployee employee;
            while (_callLine.Count > 0)
            {
                lock (_locker)
                {
                    if (!_callLine.TryPeek(out call))
                    {
                        return;
                    }
                    if (!_employees.TryGetAvailable(out employee))
                    {
                        return;
                    }
                    _callLine.Dequeue();
                }

                await HandleCallAsync(call, employee);
            }
        }

        private async Task HandleCallAsync(Call call, IEmployee employee)
        {
            await employee.HandleCallAsync(call);
            _employees.SetAvailable(employee);
        }
    }
}
