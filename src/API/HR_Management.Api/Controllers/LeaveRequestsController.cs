using MediatR;
using Microsoft.AspNetCore.Mvc;
using HR_Management.Application.DTOs.LeaveRequest;
using HR_Management.Application.Features.LeaveRequests.Requests.Queries;
using HR_Management.Application.Features.LeaveRequests.Requests.Commands;
namespace HR_Management.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveRequestsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveRequestsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/LeaveRequests
    [HttpGet]
    public async Task<ActionResult<List<LeaveRequestDto>>> Get()
    {
        List<LeaveRequestDto> leaveRequests = await _mediator.Send(new GetLeaveRequestListRequest());
        return Ok(leaveRequests);
    }

    // GET: api/LeaveRequests/5
    [HttpGet("{id}", Name = "GetLeaveRequest")]
    public async Task<ActionResult<LeaveRequestDto>> Get(int id)
    {
        LeaveRequestDto leaveRequest = await _mediator.Send(new GetLeaveRequestDetailRequest() { Id = id });
        return Ok(leaveRequest);
    }

    // POST: api/LeaveRequests
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateLeaveRequestsDto leaveRequest)
    {
        CreateLeaveRequestCommand command = new() { LeaveRequestDto = leaveRequest };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    // PUT: api/LeaveRequests/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] UpdateLeaveRequestDto leaveRequest)
    {
        UpdateLeaveRequestCommand command = new() { Id = id, LeaveRequestDto = leaveRequest };
        await _mediator.Send(command);
        return NoContent();
    }

    // DELETE: api/LeaveRequests/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        DeleteLeaveRequestCommand command = new() { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("changeapproval/{id}")]
    public async Task<ActionResult> ChangeApproval(int id, [FromBody] ChangeLeaveRequestApprovalDto changeLeaveRequestApproval)
    {
        UpdateLeaveRequestCommand command = new() { Id = id, ChangeLeaveRequestApprovalDto = changeLeaveRequestApproval };
        await _mediator.Send(command);
        return NoContent();
    }

}

