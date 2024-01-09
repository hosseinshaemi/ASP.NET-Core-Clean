using FluentValidation;
using HR_Management.Application.Persistence.Contracts;
namespace HR_Management.Application.DTOs.LeaveRequest.Validators;

public class UpdateLeaveRequestsDtoValidator : AbstractValidator<UpdateLeaveRequestDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public UpdateLeaveRequestsDtoValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        Include(new ILeaveRequestDtoValidator(_leaveTypeRepository));
        RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} is required.");
    }
}