using Moq;
using HR_Management.Domain;
using HR_Management.Application.Contracts.Persistence;
namespace HR_Management.Application.UnitTests.Mocks;

public static class MockLeaveTypeRepository
{
    public static Mock<ILeaveTypeRepository> GetLeaveTypeRepository()
    {
        List<LeaveType> leaveTypes = new()
        {
            new LeaveType {Id = 1, DefaultDay = 10, Name = "Test Vacation"},
            new LeaveType {Id = 2, DefaultDay = 15, Name = "Test Sick"}
        };

        Mock<ILeaveTypeRepository> mockRepo = new();
        mockRepo.Setup(r => r.GetAll()).ReturnsAsync(leaveTypes);
        mockRepo.Setup(r => r.Add(It.IsAny<LeaveType>())).ReturnsAsync((LeaveType leaveType) =>
        {
            leaveTypes.Add(leaveType);
            return leaveType;
        });

        return mockRepo;
    }
}