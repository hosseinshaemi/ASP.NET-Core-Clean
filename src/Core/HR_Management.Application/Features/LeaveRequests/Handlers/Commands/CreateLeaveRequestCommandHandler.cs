using MediatR;
using AutoMapper;
using HR_Management_Domain;
using HR_Management.Application.Exceptions;
using HR_Management.Application.Persistence.Contracts;
using HR_Management.Application.DTOs.LeaveRequest.Validators;
using HR_Management.Application.Features.LeaveRequests.Requests.Commands;
namespace HR_Management.Application.Features.LeaveRequests.Handlers.Commands;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        CreateLeaveRequestsDtoValidator validator = new(_leaveTypeRepository);
        var result = await validator.ValidateAsync(request.LeaveRequestDto);

        if (!result.IsValid)
            throw new ValidationException(result);

        var leaveRequest = _mapper.Map<LeaveRequest>(request.LeaveRequestDto);
        leaveRequest = await _leaveRequestRepository.Add(leaveRequest);
        return leaveRequest.Id;
    }
}