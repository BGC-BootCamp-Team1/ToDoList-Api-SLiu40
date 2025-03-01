using TodoItems.Api.Model;
using TodoItems.Core.Model;

namespace TodoItems.Core.Service;

public interface ITodoItemService
{
    TodoItemDTO Create(OptionEnum option, string description, DateTime? dueDay, string userId);
    TodoItemDTO Update(string id, TodoItemUpdateRequest request);
}