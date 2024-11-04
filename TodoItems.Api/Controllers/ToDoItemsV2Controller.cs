using Microsoft.AspNetCore.Mvc;
using TodoItems.Api.DTO;
using TodoItems.Api.Service;

namespace TodoItems.Api.Controllers
{
    [ApiController]
    [Route("api/v2/toDoItems")]
    public class ToDoItemsV2Controller : ControllerBase
    {
        private readonly ILogger<ToDoItemsController> _logger;
        private readonly IToDoItemService _service;
        public ToDoItemsV2Controller(ILogger<ToDoItemsController> logger, [FromKeyedServices("externalService")] IToDoItemService toDoItemService)
        {
            _logger = logger;
            _service = toDoItemService;
        }
        [HttpPost]
        public async Task<ActionResult<ToDoItemDto>> PostAsync(ToDoItemCreateRequest request)
        {
            ToDoItemDto dto = new()
            {
                Description = request.Description,
                Id = Guid.NewGuid().ToString(),
                Favorite = request.Favorite,
                Done = request.Done,
            };
            ToDoItem toDoItem = await _service.CreateAsync(dto);
            return Created("", toDoItem);
        }


    }
}
