using HR_Management_Domain;
namespace HR_Management.Application.Contracts.Persistence;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);
    Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails();
}