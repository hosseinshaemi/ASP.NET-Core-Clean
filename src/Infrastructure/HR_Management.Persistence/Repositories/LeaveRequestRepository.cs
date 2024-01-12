#nullable disable
using HR_Management_Domain;
using Microsoft.EntityFrameworkCore;
using HR_Management.Application.Contracts.Persistence;
namespace HR_Management.Persistence.Repositories;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    private readonly LeaveManagementDbContext _context;

    public LeaveRequestRepository(LeaveManagementDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? approvalStatus)
    {
        leaveRequest.Approved = approvalStatus;
        _context.Entry(leaveRequest).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
    {
        List<LeaveRequest> leaveRequests = await _context.LeaveRequests.Include(e => e.LeaveType).ToListAsync();
        return leaveRequests;
    }

    public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
    {
        LeaveRequest leaveRequest = await _context.LeaveRequests.Include(e => e.LeaveType).FirstOrDefaultAsync(l => l.Id == id);
        return leaveRequest;
    }
}