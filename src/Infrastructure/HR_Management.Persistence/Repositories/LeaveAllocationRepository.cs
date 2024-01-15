#nullable disable
using HR_Management.Domain;
using Microsoft.EntityFrameworkCore;
using HR_Management.Application.Contracts.Persistence;
namespace HR_Management.Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    private readonly LeaveManagementDbContext _context;

    public LeaveAllocationRepository(LeaveManagementDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
    {
        List<LeaveAllocation> leaveAllocations = await _context.LeaveAllocations.Include(e => e.LeaveType).ToListAsync();
        return leaveAllocations;
    }

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
    {
        LeaveAllocation leaveAllocation = await _context.LeaveAllocations.Include(e => e.LeaveType).FirstOrDefaultAsync(l => l.Id == id);
        return leaveAllocation;
    }
}