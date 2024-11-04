using TodoItems.Core.Model;

namespace TodoItems.Core.Strategy;

public interface ITodoItemGenerator
{
    TodoItemDTO Generate(string description, DateTime? dueDay, string userId);
}
