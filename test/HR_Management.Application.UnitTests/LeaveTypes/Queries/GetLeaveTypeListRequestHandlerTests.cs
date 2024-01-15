using Moq;
using Shouldly;
using AutoMapper;
using HR_Management.Application.Profiles;
using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Application.UnitTests.Mocks;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.Features.LeaveTypes.Handlers.Queries;
using HR_Management.Application.Features.LeaveTypes.Requests.Queries;
namespace HR_Management.Application.UnitTests.LeaveTypes.Queries;

public class GetLeaveTypeListRequestHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<ILeaveTypeRepository> _mockLeaveTypeRepository;

    public GetLeaveTypeListRequestHandlerTests()
    {
        _mockLeaveTypeRepository = MockLeaveTypeRepository.GetLeaveTypeRepository();
        var mapperConfig = new MapperConfiguration(m => m.AddProfile<MappingProfile>());
        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task GetLeaveTypeListTest()
    {
        GetLeaveTypeListRequestHandler handler = new(_mockLeaveTypeRepository.Object, _mapper);
        List<LeaveTypeDto> result = await handler.Handle(new GetLeaveTypeListRequest(), CancellationToken.None);
        result.ShouldBeOfType<List<LeaveTypeDto>>();
        result.Count.ShouldBe(2);
    }

}