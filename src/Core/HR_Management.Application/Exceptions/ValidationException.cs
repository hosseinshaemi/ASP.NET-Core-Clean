using FluentValidation.Results;
namespace HR_Management.Application.Exceptions;

public class ValidationException : ApplicationException
{
    public List<string> Errors { get; set; } = new();

    public ValidationException(ValidationResult validationResult)
    {
        foreach (ValidationFailure err in validationResult.Errors)
            Errors.Add(err.ErrorMessage);
    }
}