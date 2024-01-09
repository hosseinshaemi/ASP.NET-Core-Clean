#nullable disable
using MediatR;
using HR_Management.Application.DTOs.LeaveRequest;
namespace HR_Management.Application.Features.LeaveRequests.Requests.Commands;

public class UpdateLeaveRequestCommand : IRequest<Unit>
{
    public LeaveRequestDto LeaveRequestDto { get; set; }
}