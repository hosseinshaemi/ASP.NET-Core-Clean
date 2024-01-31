using HR_Management.MVC.Contracts;
using HR_Management.MVC.Models;
using Microsoft.AspNetCore.Mvc;
namespace HR_Management.MVC.Controllers;

public class LeaveTypeController : Controller
{
    private readonly ILeaveTypeService _leaveTypeService;

    public LeaveTypeController(ILeaveTypeService leaveTypeService)
    {
        _leaveTypeService = leaveTypeService;
    }

    // GET: LeaveType
    public async Task<ActionResult> Index()
    {
        var leaveTypes = await _leaveTypeService.GetLeaveTypes();
        return View(leaveTypes);
    }

    // GET: LeaveType/Details/5
    public async Task<ActionResult> Details(int id)
    {
        var leaveType = await _leaveTypeService.GetLeaveTypeDetails(id);
        return View(leaveType);
    }

    // GET: LeaveType/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: LeaveType/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateLeaveTypeVM createLeave)
    {
        try
        {
            var response = await _leaveTypeService.CreateLeaveType(createLeave);
            if (response.Success)
                return RedirectToAction("Index");
            ModelState.AddModelError("", response.ValidationErrors);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }
        return View(createLeave);
    }

    // GET: LeaveType/Edit/5
    public async Task<ActionResult> Edit(int id)
    {
        var leaveType = await _leaveTypeService.GetLeaveTypeDetails(id);
        return View(leaveType);
    }

    // POST: LeaveType/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, LeaveTypeVM leaveType)
    {
        try
        {
            var response = await _leaveTypeService.UpdateLeaveType(id, leaveType);
            if (response.Success)
                return RedirectToAction("Index");
            ModelState.AddModelError("", response.ValidationErrors);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }
        return View(leaveType);
    }

    // GET: LeaveType/Delete/5
    public async Task<ActionResult> Delete(int id)
    {
        var leaveType = await _leaveTypeService.GetLeaveTypeDetails(id);
        return View(leaveType);
    }

    // POST: LeaveType/Delete/5
    [HttpPost("LeaveType/Delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteLeaveType(int id)
    {
        try
        {
            var response = await _leaveTypeService.DeleteLeaveType(id);
            if (response.Success)
                return RedirectToAction("Index");
            ModelState.AddModelError("", response.ValidationErrors);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }
        return BadRequest();
    }
}
