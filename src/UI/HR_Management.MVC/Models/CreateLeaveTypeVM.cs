#nullable disable
using System.ComponentModel.DataAnnotations;

namespace HR_Management.MVC.Models;

public class CreateLeaveTypeVM
{
    [Required(ErrorMessage = "The name is required.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "default number of days is required.")]
    [Display(Name = "Default Number Of Days")]
    public int DefaultDay { get; set; }
}