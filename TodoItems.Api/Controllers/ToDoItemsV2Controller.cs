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
        public async Task<ActionResult<TodoItemVO>> PostAsync(ToDoItemCreateRequest request)
        {
            TodoItemDTO dto = _service.Create(OptionEnum.Manual, request.Description, request.DueDay, request.UserId);
            TodoItemVO ao = new TodoItemVO
            {
                Id=dto.Id,
                Description=dto.Description,
                CreatedTime= dto.CreatedTime,
                DueDay=dto.DueDay,
                Favorite=dto.Favorite,
                Done=dto.Done,
                ModificationList=dto.ModificationList,
                UserId=dto.UserId
            };
            return Created("", ao);
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult<TodoItemVO>> PutAsync(string id, TodoItemUpdateRequest request)
        //{
        //    new TodoItemDTO
        //    {
        //        Id = id,
        //        Description=request.Description,
        //        CreatedTime=request.CreatedTime,
        //        Favorite=request.Favorite,
        //        Done=request.Done,

        //    }



        //    TodoItemDTO dto = _service.Update(id, request);
        //    TodoItemVO ao = new TodoItemVO
        //    {
        //        Id = dto.Id,
        //        Description = dto.Description,
        //        CreatedTime = dto.CreatedTime,
        //        DueDay = dto.DueDay,
        //        Favorite = dto.Favorite,
        //        Done = dto.Done,
        //        ModificationList = dto.ModificationList,
        //        UserId = dto.UserId
        //    };

        //    return Ok(ao);
        //}



    }
}
