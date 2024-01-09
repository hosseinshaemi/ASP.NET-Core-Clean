using MediatR;
using HR_Management_Domain;
using HR_Management.Application.Exceptions;
using HR_Management.Application.Persistence.Contracts;
using HR_Management.Application.Features.LeaveTypes.Requests.Commands;
namespace HR_Management.Application.Features.LeaveTypes.Handlers.Commands;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
    }

    public async Task Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var leaveType = await _leaveTypeRepository.Get(request.Id);

        if (leaveType == null)
            throw new NotFoundException(nameof(LeaveType), request.Id);

        await _leaveTypeRepository.Delete(leaveType);
    }
}