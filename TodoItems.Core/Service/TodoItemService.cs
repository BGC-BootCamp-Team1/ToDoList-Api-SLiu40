using TodoItems.Api.Model;
using TodoItems.Core.Model;
using TodoItems.Core.Repository;

namespace TodoItems.Core.Service
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemsRepository _repository;

        public TodoItemService(ITodoItemsRepository repository)
        {
            _repository = repository;
        }

        public TodoItemDTO Create(OptionEnum option, string description, DateTime? dueDay, string userId)
        {
            var generator = new TodoItemGeneratorFactory(_repository).GetGenerator(option, dueDay);
            var todoItem = generator.Generate(description, dueDay, userId);
            return _repository.Save(todoItem);
        }

        public void Update(string id, TodoItemUpdateRequest request)
        {
            TodoItemDTO? dto=null;

            try
            {
                dto = _repository.FindById(id);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

            if (dto==null)
            {
                Create(OptionEnum.Manual, request.Description, request.DueDay, request.UserId);
            }
            dto.Modify(request.Description);




            _repository.Update(dto);

        }
    }
}
