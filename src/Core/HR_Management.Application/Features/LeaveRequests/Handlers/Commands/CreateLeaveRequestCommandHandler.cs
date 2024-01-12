using MediatR;
using AutoMapper;
using HR_Management_Domain;
using HR_Management.Application.Responses;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveRequest.Validators;
using HR_Management.Application.Features.LeaveRequests.Requests.Commands;
using HR_Management.Application.Contracts.Infrastructure;
using HR_Management.Application.Models;
namespace HR_Management.Application.Features.LeaveRequests.Handlers.Commands;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IEmailSender _emailSender;
    private readonly IMapper _mapper;

    public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper, IEmailSender emailSender)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
        _emailSender = emailSender;
    }

    public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        BaseCommandResponse response = new();
        CreateLeaveRequestsDtoValidator validator = new(_leaveTypeRepository);
        var result = await validator.ValidateAsync(request.LeaveRequestDto);

        if (!result.IsValid)
        {
            // throw new ValidationException(result);
            response.Successfull = false;
            response.Message = "Creation failed.";
            response.Errors = result.Errors.Select(e => e.ErrorMessage).ToList();
        }

        var leaveRequest = _mapper.Map<LeaveRequest>(request.LeaveRequestDto);
        leaveRequest = await _leaveRequestRepository.Add(leaveRequest);

        response.Successfull = true;
        response.Message = "Creation successfull.";
        response.Id = leaveRequest.Id;

        Email email = new()
        {
            To = "johndoe@gmail.com",
            Subject = "leave request submitted",
            Body = $"Your leave request for {request.LeaveRequestDto.StartDate} to {request.LeaveRequestDto.EndDate} has been submitted"
        };

        try
        {
            await _emailSender.SendEmail(email);
        }
        catch (Exception)
        {
            throw;
        }

        return response;
    }
}