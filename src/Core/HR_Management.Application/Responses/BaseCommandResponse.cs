#nullable disable
namespace HR_Management.Application.Responses;

public class BaseCommandResponse
{
    public int Id { get; set; }
    public bool Successfull { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; }
}