using MediatR;
using HR_Management.Application.DTOs.LeaveRequest;
namespace HR_Management.Application.Features.LeaveRequests.Requests.Queries;

public class GetLeaveRequestListRequest : IRequest<List<LeaveRequestDto>>
{

}