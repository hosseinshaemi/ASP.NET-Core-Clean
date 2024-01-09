using HR_Management.Application.DTOs.Common;
using HR_Management.Application.DTOs.LeaveType;
namespace HR_Management.Application.DTOs.LeaveAllocation;

public class CreateLeaveAllocationDto : BaseDto
{
    public int NumberOfDays { get; set; }
    public int LeaveTypeId { get; set; }
    public int Period { get; set; }
}