#nullable disable
using MediatR;
using HR_Management.Application.Responses;
using HR_Management.Application.DTOs.LeaveType;
namespace HR_Management.Application.Features.LeaveTypes.Requests.Commands;

public class CreateLeaveTypeCommand : IRequest<BaseCommandResponse>
{
    public CreateLeaveTypeDto LeaveTypeDto { get; set; }
}