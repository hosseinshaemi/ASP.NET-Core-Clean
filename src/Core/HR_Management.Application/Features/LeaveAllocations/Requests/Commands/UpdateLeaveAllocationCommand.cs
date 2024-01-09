#nullable disable
using MediatR;
using HR_Management.Application.DTOs.LeaveAllocation;
namespace HR_Management.Application.Features.LeaveAllocations.Requests.Commands;

public class UpdateLeaveAllocationCommand : IRequest<Unit>
{
    public UpdateLeaveAllocationDto LeaveAllocationDto { get; set; }
}