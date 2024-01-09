using MediatR;
using AutoMapper;
using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Application.Persistence.Contracts;
using HR_Management.Application.Features.LeaveTypes.Requests.Queries;
namespace HR_Management.Application.Features.LeaveTypes.Handlers.Queries;

public class GetLeaveTypeListRequestHandler : IRequestHandler<GetLeaveTypeListRequest, List<LeaveTypeDto>>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public GetLeaveTypeListRequestHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }

    public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypeListRequest request, CancellationToken cancellationToken)
    {
        var leavyTypeList = await _leaveTypeRepository.GetAll();
        return _mapper.Map<List<LeaveTypeDto>>(leavyTypeList);
    }
}