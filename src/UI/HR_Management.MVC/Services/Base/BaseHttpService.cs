using System.Net.Http.Headers;
using HR_Management.MVC.Contracts;

namespace HR_Management.MVC.Services.Base;

public class BaseHttpService
{
    protected readonly ILocalStorageService _localStorage;
    protected readonly IClient _client;

    public BaseHttpService(ILocalStorageService localStorage, IClient client)
    {
        _localStorage = localStorage;
        _client = client;
    }

    protected Response<Guid> ConvertApiExceptions<Guid>(ApiException exception)
    {
        if (exception.StatusCode == 400)
            return new Response<Guid>() { Message = "Validation errors have occured.", ValidationErrors = exception.Response, Success = false };
        else if (exception.StatusCode == 404)
            return new Response<Guid>() { Message = "Not Found ...", Success = false };
        else
            return new Response<Guid>() { Message = "Something went wrong, try again ...", Success = false };
    }

    protected void AddBearerToken()
    {
        if (_localStorage.Exists("token"))
            _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _localStorage.GetStorageValue<string>("token"));
    }

}