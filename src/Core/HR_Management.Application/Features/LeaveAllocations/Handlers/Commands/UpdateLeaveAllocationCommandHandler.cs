using MediatR;
using AutoMapper;
using HR_Management.Application.Exceptions;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveAllocation.Validators;
using HR_Management.Application.Features.LeaveAllocations.Requests.Commands;
namespace HR_Management.Application.Features.LeaveAllocations.Handlers.Commands;

public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public UpdateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        UpdateLeaveAllocationDtoValidator validator = new(_leaveTypeRepository);
        var result = await validator.ValidateAsync(request.LeaveAllocationDto);

        if (!result.IsValid)
            throw new ValidationException(result);

        var leaveAllocation = await _leaveAllocationRepository.Get(request.LeaveAllocationDto.Id);
        _mapper.Map(request.LeaveAllocationDto, leaveAllocation);
        await _leaveAllocationRepository.Update(leaveAllocation);
        return Unit.Value;
    }
}
