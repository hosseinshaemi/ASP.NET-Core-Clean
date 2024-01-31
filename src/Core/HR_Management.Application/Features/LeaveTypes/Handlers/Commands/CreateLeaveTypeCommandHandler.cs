using MediatR;
using AutoMapper;
using HR_Management.Domain;
using HR_Management.Application.Exceptions;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveType.Validators;
using HR_Management.Application.Features.LeaveTypes.Requests.Commands;
using HR_Management.Application.Responses;
namespace HR_Management.Application.Features.LeaveTypes.Handlers.Commands;

public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, BaseCommandResponse>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public CreateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        BaseCommandResponse response;

        CreateLeaveTypeValidator validator = new();
        var result = await validator.ValidateAsync(request.LeaveTypeDto);

        if (!result.IsValid)
        {
            response = new()
            {
                Successfull = false,
                Message = "Creation failed.",
                Errors = result.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }
        else
        {
            var leaveType = _mapper.Map<LeaveType>(request.LeaveTypeDto);
            leaveType = await _leaveTypeRepository.Add(leaveType);

            response = new()
            {
                Successfull = true,
                Message = "Creation successfull.",
                Id = leaveType.Id
            };
        }

        return response;
    }
}