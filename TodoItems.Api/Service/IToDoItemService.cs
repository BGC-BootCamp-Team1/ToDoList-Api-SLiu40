using TodoItems.Api.DTO;

namespace TodoItems.Api.Service
{
    public interface IToDoItemService
    {
        Task<ToDoItem> CreateAsync(ToDoItemDto newToDoItem);
        Task<List<ToDoItemDto>> GetAsync();
        Task<ToDoItemDto?> GetAsync(string id);
        Task<bool> RemoveAsync(string id);
        Task ReplaceAsync(string id, ToDoItemDto updatedToDoItem);
    }
}
