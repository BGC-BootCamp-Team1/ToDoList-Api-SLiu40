using TodoItems.Core.Model;

namespace TodoItems.Core.Repository
{
    public interface ITodoItemsRepository
    {
        List<TodoItemDTO> FindAllTodoItemsByUserIdAndDueDay(string userId, DateTime dueDay);
        List<TodoItemDTO> FindTodoItemsInFiveDaysByUserId(string userId);
        TodoItemDTO Save(TodoItemDTO todoItem);
    }
}
