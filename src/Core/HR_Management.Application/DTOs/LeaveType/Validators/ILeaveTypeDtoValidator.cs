using FluentValidation;
namespace HR_Management.Application.DTOs.LeaveType.Validators;

public class ILeaveTypeDtoValidator : AbstractValidator<ILeaveTypeDto>
{
    public ILeaveTypeDtoValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("{PropertyName} is required.").NotNull()
        .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50.");

        RuleFor(p => p.DefaultDay).NotEmpty().WithMessage("{PropertyName} is required.")
        .GreaterThan(0).WithMessage("{PropertyName} must be greater than 1.")
        .LessThan(100).WithMessage("{PropertyName} mudt be less than 100.");
    }
}