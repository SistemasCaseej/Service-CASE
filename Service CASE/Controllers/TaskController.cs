    using Microsoft.AspNetCore.Mvc;
    using Service_CASE.Models.Task;
    using Service_CASE.Services;

    namespace Service_CASE.Controllers;

    [ApiController]
    [Route("api/[controller]")]

    public class TaskController : ControllerBase
    {   
        private readonly UserTaskService _userTaskService;

        public TaskController(UserTaskService userTaskService)
        {
            _userTaskService = userTaskService;
        }

        [HttpGet]   
        public async Task<List<UserTask>> GetAllTasks() => await _userTaskService.GetAllTaskAsync();

        [HttpPost]
        public async Task<IActionResult> CreateTask(UserTask task)
        {
            await _userTaskService.CreateTaskAsync(task);
            return CreatedAtAction(nameof(GetAllTasks), new { id = task.Id }, task);
        }
    }