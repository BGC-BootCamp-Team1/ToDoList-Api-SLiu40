using Microsoft.AspNetCore.Mvc;
using TodoItems.Api.DTO;
using TodoItems.Api.Service;

namespace TodoItems.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]

    public class ToDoItemsController : ControllerBase
    {
        private readonly ILogger<ToDoItemsController> _logger;
        private readonly IToDoItemService _service;
        public ToDoItemsController(ILogger<ToDoItemsController> logger, IToDoItemService toDoItemService)
        {
            _logger = logger;
            _service = toDoItemService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItemDto>> GetAsync(string id)
        {
            return Ok(await _service.GetAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult<List<ToDoItemDto>>> GetAsync()
        {
            List<ToDoItemDto> results = await _service.GetAsync();
            return Ok(results);
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
            return Created("",toDoItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(string id, ToDoItemDto dto)
        {
            await _service.ReplaceAsync(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            await _service.RemoveAsync(id);
            return Ok();
        }

    }
}
