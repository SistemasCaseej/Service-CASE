using Microsoft.AspNetCore.Mvc;
using Service_CASE.Dto;
using Service_CASE.Dto.Department;
using Service_CASE.Models.Department;
using Service_CASE.Services;

namespace Service_CASE.Controllers;

[ApiController]
[Route("api/departments")]

public class DepartmentController : ControllerBase
{
    private readonly DepartmentService _departmentService;

    public DepartmentController(DepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet]
    public async Task<List<ReadDepartmentDto>> GetAllDepartments() => await _departmentService.GetAllDepartments();

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDepartmentById(string id)
    {
        var department = await _departmentService.GetDepartmentById(id);
        if (department == null)
            return NotFound("Departamento não encontrado.");
        
        return Ok(department);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentDto department)
    {
        var createdDepartment = await _departmentService.CreateDepartment(department);
        return CreatedAtAction(nameof(GetAllDepartments), new { id = department.Id }, createdDepartment);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateDepartament(string id, [FromBody] UpdateDepartmentDto updateDepartmentDto)
    {
        var updated = await _departmentService.UpdateDepartment(id, updateDepartmentDto);
        
        if(updated == null)
            return NotFound("Departamento não encontrado");
        
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(string id)
    {
        try
        {
            var deletedDepartment = await _departmentService.DeleteDepartment(id);
            return Ok(deletedDepartment);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Departamento não encontrado.");
        }
    }
    
}