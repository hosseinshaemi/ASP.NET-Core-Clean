using Moq;
using AutoMapper;
using HR_Management.Application.Profiles;
using HR_Management.Application.UnitTests.Mocks;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.Features.LeaveTypes.Handlers.Commands;
using HR_Management.Application.Features.LeaveTypes.Requests.Commands;
using HR_Management.Application.DTOs.LeaveType;
using Shouldly;
using HR_Management.Domain;
namespace HR_Management.Application.UnitTests.LeaveTypes.Commands;

public class CreateLeaveTypeCommandHandlerTests
{
    private readonly IMapper _mapper;
    private readonly CreateLeaveTypeDto _createLeaveTypeDto;
    private readonly Mock<ILeaveTypeRepository> _mockLeaveTypeRepository;

    public CreateLeaveTypeCommandHandlerTests()
    {
        _mockLeaveTypeRepository = MockLeaveTypeRepository.GetLeaveTypeRepository();
        var mapperConfig = new MapperConfiguration(m => m.AddProfile<MappingProfile>());
        _mapper = mapperConfig.CreateMapper();
        _createLeaveTypeDto = new() { DefaultDay = 15, Name = "Test DTO" };
    }

    [Fact]
    public async Task CreateLeaveTypeTest()
    {
        CreateLeaveTypeCommandHandler handler = new(_mockLeaveTypeRepository.Object, _mapper);
        int result = await handler.Handle(
            new CreateLeaveTypeCommand { LeaveTypeDto = _createLeaveTypeDto },
            CancellationToken.None
        );

        result.ShouldBeOfType<int>();
        IReadOnlyList<LeaveType> leaveTypes = await _mockLeaveTypeRepository.Object.GetAll();
        leaveTypes.Count.ShouldBe(3);
    }

}