using MediatR;
using Microsoft.AspNetCore.Mvc;
using HR_Management.Application.DTOs.LeaveAllocation;
using HR_Management.Application.Features.LeaveAllocations.Requests.Queries;
using HR_Management.Application.Features.LeaveAllocations.Requests.Commands;
namespace HR_Management.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveAllocationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveAllocationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/LeaveAllocations
    [HttpGet]
    public async Task<ActionResult<List<LeaveAllocationDto>>> Get()
    {
        List<LeaveAllocationDto> leaveAllocations = await _mediator.Send(new GetLeaveAllocationListRequest());
        return Ok(leaveAllocations);
    }

    // GET: api/LeaveAllocations/5
    [HttpGet("{id}", Name = "GetLeaveAllocation")]
    public async Task<ActionResult<LeaveAllocationDto>> Get(int id)
    {
        LeaveAllocationDto leaveAllocation = await _mediator.Send(new GetLeaveAllocationDetailRequest() { Id = id });
        return Ok(leaveAllocation);
    }

    // POST: api/LeaveAllocations
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateLeaveAllocationDto leaveAllocation)
    {
        CreateLeaveAllocationCommand command = new() { LeaveAllocationDto = leaveAllocation };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    // PUT: api/LeaveAllocations/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] UpdateLeaveAllocationDto leaveAllocation)
    {
        UpdateLeaveAllocationCommand command = new() { LeaveAllocationDto = leaveAllocation };
        await _mediator.Send(command);
        return NoContent();
    }

    // DELETE: api/LeaveAllocations/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        DeleteLeaveAllocationCommand command = new() { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}

