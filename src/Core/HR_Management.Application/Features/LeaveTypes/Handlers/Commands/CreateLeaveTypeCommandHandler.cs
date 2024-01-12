using MediatR;
using AutoMapper;
using HR_Management_Domain;
using HR_Management.Application.Exceptions;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveType.Validators;
using HR_Management.Application.Features.LeaveTypes.Requests.Commands;
namespace HR_Management.Application.Features.LeaveTypes.Handlers.Commands;

public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public CreateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        CreateLeaveTypeValidator validator = new();
        var result = await validator.ValidateAsync(request.LeaveTypeDto);

        if (!result.IsValid)
            throw new ValidationException(result);

        var leaveType = _mapper.Map<LeaveType>(request.LeaveTypeDto);
        leaveType = await _leaveTypeRepository.Add(leaveType);
        return leaveType.Id;
    }
}