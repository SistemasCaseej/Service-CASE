using Microsoft.AspNetCore.Mvc;
using Service_CASE.Dto;
using Service_CASE.Models.Department;
using Service_CASE.Services;

namespace Service_CASE.Controllers;

[ApiController]
[Route("api/department")]

public class DepartmentController : ControllerBase
{
    private readonly DepartmentService _departmentService;

    public DepartmentController(DepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet]
    public async Task<List<ReadDepartmentDto>> GetAllDepartments() => await _departmentService.GetAllDepartments();
    
    [HttpPost]
    public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentDto department)
    {
        var createdDepartment = await _departmentService.CreateDepartment(department);
        return CreatedAtAction(nameof(GetAllDepartments), new { id = department.Id }, createdDepartment);
    }
    
}