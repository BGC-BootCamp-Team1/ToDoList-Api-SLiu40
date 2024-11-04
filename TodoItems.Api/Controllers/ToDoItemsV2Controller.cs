using Microsoft.AspNetCore.Mvc;
using TodoItems.Api.Model;
using TodoItems.Core;
using TodoItems.Core.Model;
using TodoItems.Core.Service;

namespace TodoItems.Api.Controllers
{
    [ApiController]
    [Route("api/v2/toDoItems")]
    public class ToDoItemsV2Controller : ControllerBase
    {
        private readonly ILogger<ToDoItemsController> _logger;
        private readonly ITodoItemService _service;
        public ToDoItemsV2Controller(ILogger<ToDoItemsController> logger, [FromKeyedServices("externalService")] ITodoItemService todoItemService)
        {
            _logger = logger;
            _service = todoItemService;
        }
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostAsync(ToDoItemCreateRequest request)
        {
            TodoItem todoItem = _service.Create(OptionEnum.Manual, request.Description, request.DueDay, request.UserId);
            return Created("", todoItem);
        }


    }
}
