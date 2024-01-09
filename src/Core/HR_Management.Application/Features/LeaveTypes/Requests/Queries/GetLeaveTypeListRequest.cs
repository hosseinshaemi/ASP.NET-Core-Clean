using MediatR;
using HR_Management.Application.DTOs.LeaveType;
namespace HR_Management.Application.Features.LeaveTypes.Requests.Queries;

public class GetLeaveTypeListRequest : IRequest<List<LeaveTypeDto>>
{
    
}