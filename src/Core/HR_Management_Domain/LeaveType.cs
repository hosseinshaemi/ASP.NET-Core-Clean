#nullable disable
using HR_Management_Domain.Common;
namespace HR_Management_Domain;

public class LeaveType : BaseDomainEntity
{
    public string Name { get; set; }
    public int DefaultDay { get; set; }
}
