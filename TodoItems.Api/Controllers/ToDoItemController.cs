using Microsoft.AspNetCore.Mvc;
using TodoItems.Api.Model;
using TodoItems.Api.Service;
using TodoItems.Core.Model;
using TodoItems.Core;
using TodoItems.Core.Service;

namespace TodoItems.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]

    public class ToDoItemsController : ControllerBase
    {
        private readonly ILogger<ToDoItemsController> _logger;
        private readonly IToDoItemService _localService;
        private readonly ITodoItemService _externalService;

        public ToDoItemsController(ILogger<ToDoItemsController> logger,
            [FromKeyedServices("localService")] IToDoItemService localToDoItemService,
            [FromKeyedServices("externalService")] ITodoItemService exTodoItemService)
        {
            _logger = logger;
            _localService = localToDoItemService;
            _externalService = exTodoItemService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItemDto>> GetAsync(string id)
        {
            return Ok(await _localService.GetAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult<List<ToDoItemDto>>> GetAsync()
        {
            List<ToDoItemDto> results = await _localService.GetAsync();
            return Ok(results);
        }

        //[HttpPost]
        //public async Task<ActionResult<ToDoItemDto>> PostAsync(ToDoItemCreateRequest request)
        //{
        //    ToDoItemDto dto = new()
        //    {
        //        Description = request.Description,
        //        Id = Guid.NewGuid().ToString(),
        //        Favorite = request.Favorite,
        //        Done = request.Done,
        //    };
        //    ToDoItem toDoItem = await _localService.CreateAsync(dto);
        //    return Created("",toDoItem);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult> PutAsync(string id, ToDoItemDto dto)
        //{
        //    await _localService.ReplaceAsync(id, dto);
        //    return Ok();
        //}

        [HttpPost]
        public ActionResult<TodoItemVO> Post(ToDoItemCreateRequest request)
        {
            TodoItemDTO dto = _externalService.Create(request.option, request.Description, request.DueDay, request.UserId);
            TodoItemVO vo = new TodoItemVO
            {
                Id = dto.Id,
                Description = dto.Description,
                CreatedTime = dto.CreatedTime,
                DueDay = dto.DueDay,
                Favorite = dto.Favorite,
                Done = dto.Done,
                ModificationList = dto.ModificationList,
                UserId = dto.UserId
            };
            return Created("", vo);
        }

        [HttpPut("{id}")]
        public ActionResult<TodoItemVO> Put(string id, TodoItemUpdateRequest request)
        {

            TodoItemDTO dto = _externalService.Update(id, request);
            return Ok(new TodoItemVO
            {
                Id = dto.Id,
                Description = dto.Description,
                CreatedTime = dto.CreatedTime,
                DueDay = dto.DueDay,
                Favorite = dto.Favorite,
                Done = dto.Done,
                ModificationList = dto.ModificationList,
                UserId = dto.UserId
            });
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            await _localService.RemoveAsync(id);
            return Ok();
        }

    }
}
