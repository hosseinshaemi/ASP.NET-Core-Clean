using FluentValidation;
using HR_Management.Application.Persistence.Contracts;
namespace HR_Management.Application.DTOs.LeaveRequest.Validators;

public class CreateLeaveRequestsDtoValidator : AbstractValidator<CreateLeaveRequestsDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveRequestsDtoValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        Include(new ILeaveRequestDtoValidator(_leaveTypeRepository));
    }
}