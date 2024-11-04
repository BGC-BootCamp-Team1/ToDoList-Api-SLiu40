using TodoItems.Core.Model;

namespace TodoItems.Core.Repository
{
    public interface ITodoItemsRepository
    {
        List<TodoItemDTO> FindAllTodoItemsByUserIdAndDueDay(string userId, DateTime dueDay);
        TodoItemDTO? FindById(string id);
        List<TodoItemDTO> FindTodoItemsInFiveDaysByUserId(string userId);
        TodoItemDTO Save(TodoItemDTO todoItem);
        void Update(TodoItemDTO dto);
    }
}
