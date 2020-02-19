using System.Threading.Tasks;

namespace CallCenter
{
    class Employee : IEmployee
    {
        public EmployeeType Type { get; private set; }
        public int Order { get; private set; }

        public Employee(EmployeeType type, int order)
        {
            Type = type;
            Order = order;
        }

        public async Task HandleCallAsync(Call call)
        {
             await call.AnswerAsync(this);
        }
    }
}
