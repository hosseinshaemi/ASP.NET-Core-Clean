using MediatR;
using Microsoft.AspNetCore.Mvc;
using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Application.Features.LeaveTypes.Requests.Queries;
using HR_Management.Application.Features.LeaveTypes.Requests.Commands;
namespace HR_Management.Api.Controller;

[Route("api/[controller]")]
[ApiController]
public class LeaveTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/LeaveTypes
    [HttpGet]
    public async Task<ActionResult<List<LeaveTypeDto>>> Get()
    {
        var leaveTypes = await _mediator.Send(new GetLeaveTypeListRequest());
        return Ok(leaveTypes);
    }

    // GET: api/LeaveTypes/5
    [HttpGet("{id}", Name = "GetLeaveType")]
    public async Task<ActionResult<LeaveTypeDto>> Get(int id)
    {
        var leaveType = await _mediator.Send(new GetLeaveTypeDetailRequest() { Id = id });
        return Ok(leaveType);
    }

    // POST: api/LeaveTypes
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateLeaveTypeDto leaveType)
    {
        CreateLeaveTypeCommand command = new() { LeaveTypeDto = leaveType };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    // PUT: api/LeaveTypes/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] LeaveTypeDto leaveType)
    {
        UpdateLeaveTypeCommand command = new() { LeaveTypeDto = leaveType };
        await _mediator.Send(command);
        return NoContent();
    }

    // DELETE: api/LeaveTypes/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        DeleteLeaveTypeCommand command = new() { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}

