using AutoMapper;
using HR_Management.MVC.Models;
using HR_Management.MVC.Contracts;
using HR_Management.MVC.Services.Base;
namespace HR_Management.MVC.Services;

public class LeaveTypeService : BaseHttpService, ILeaveTypeService
{
    private readonly IMapper _mapper;

    public LeaveTypeService(ILocalStorageService localStorage, IClient client, IMapper mapper) : base(localStorage, client)
    {
        _mapper = mapper;
    }

    public async Task<Response<int>> CreateLeaveType(CreateLeaveTypeVM leaveType)
    {
        try
        {
            Response<int> response = new();
            CreateLeaveTypeDto createLeaveTypeDto = _mapper.Map<CreateLeaveTypeDto>(leaveType);

            //TODO authentication

            BaseCommandResponse apiResponse = await _client.LeaveTypesPOSTAsync(createLeaveTypeDto);
            if (apiResponse.Successfull)
            {
                response.Data = apiResponse.Id;
                response.Success = true;
            }
            else
            {
                foreach (string error in apiResponse.Errors)
                    response.ValidationErrors += error + Environment.NewLine;
            }

            return response;
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<int>(ex);
        }
    }

    public async Task<Response<int>> DeleteLeaveType(int id)
    {
        try
        {
            await _client.LeaveTypesDELETEAsync(id);
            return new Response<int> { Success = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<int>(ex);
        }
    }

    public async Task<LeaveTypeVM> GetLeaveTypeDetails(int id)
    {
        var leaveType = await _client.GetLeaveTypeAsync(id);
        return _mapper.Map<LeaveTypeVM>(leaveType);
    }

    public async Task<List<LeaveTypeVM>> GetLeaveTypes()
    {
        var leaveTypes = await _client.LeaveTypesAllAsync();
        return _mapper.Map<List<LeaveTypeVM>>(leaveTypes);
    }

    public async Task<Response<int>> UpdateLeaveType(int id, LeaveTypeVM leaveType)
    {
        try
        {
            LeaveTypeDto leaveTypeDto = _mapper.Map<LeaveTypeDto>(leaveType);
            await _client.LeaveTypesPUTAsync(id, leaveTypeDto);
            return new Response<int> { Success = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<int>(ex);
        }
    }
}