using FluentValidation;
using HR_Management.Application.Persistence.Contracts;
namespace HR_Management.Application.DTOs.LeaveAllocation.Validators;

public class ILeaveAllocationDtoValidator : AbstractValidator<ILeaveAllocationDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public ILeaveAllocationDtoValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(p => p.NumberOfDays).GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(p => p.Period).GreaterThanOrEqualTo(DateTime.Now.Year)
        .WithMessage("{PropertyName} must be after {ComparisionValue}");

        RuleFor(p => p.LeaveTypeId).GreaterThan(0)
        .MustAsync(async (id, token) =>
        {
            var leaveTypeExist = await _leaveTypeRepository.Exist(id);
            return !leaveTypeExist;
        })
        .WithMessage("{PropertyName} does not exist.");
    }
}