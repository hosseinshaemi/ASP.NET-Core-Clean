#nullable disable
using MediatR;
using HR_Management.Application.DTOs.LeaveRequest;
namespace HR_Management.Application.Features.LeaveRequests.Requests.Commands;

public class CreateLeaveRequestCommand : IRequest<int>
{
    public LeaveRequestDto LeaveRequestDto { get; set; }
}