using System.Threading.Tasks;

namespace CallCenter
{
    interface IEmployee
    {
        EmployeeType Type { get; }
        int Order { get; }
        Task HandleCallAsync(Call call);
    }
}
