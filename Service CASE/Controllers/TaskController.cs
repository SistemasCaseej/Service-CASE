    using Microsoft.AspNetCore.Mvc;
    using Service_CASE.Models.Task;
    using Service_CASE.Services;

    namespace Service_CASE.Controllers;

    [ApiController]
    [Route("api/user-tasks")]

    public class TaskController : ControllerBase
    {   
        private readonly UserTaskService _userTaskService;

        public TaskController(UserTaskService userTaskService)
        {
            _userTaskService = userTaskService;
        }

        [HttpGet]   
        public async Task<List<UserTask>> GetAllTasks() => await _userTaskService.GetAllTaskAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<UserTask>> GetTaskById(string id)
        {
            var task = await _userTaskService.GetTaskByIdAsync(id);
            if(task == null) return NotFound();
            return Ok(task);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTask(UserTask task)
        {
            await _userTaskService.CreateTaskAsync(task);
            return CreatedAtAction(nameof(GetAllTasks), new { id = task.Id }, task);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateTask(string id, [FromBody] UserTask task)
        {
            var success = await _userTaskService.UpdateTaskAsync(id, task);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(string id)
        {
            var deletedTask = await _userTaskService.DeleteTaskAsync(id);
            if(deletedTask == null) return NotFound();
            return NoContent();
        }
    }