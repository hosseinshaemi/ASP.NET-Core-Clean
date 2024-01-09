#nullable disable
using MediatR;
using HR_Management.Application.DTOs.LeaveAllocation;
namespace HR_Management.Application.Features.LeaveAllocations.Requests.Commands;

public class CraeteLeaveAllocationCommand : IRequest<int>
{
    public LeaveAllocationDto LeaveAllocationDto { get; set; }
}