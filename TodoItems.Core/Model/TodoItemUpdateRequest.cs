using TodoItems.Core.Model;

namespace TodoItems.Api.Model
{
    public class TodoItemUpdateRequest
    {
        public string Id;
        public string? Description { get; set; }
        public DateTime? DueDay { get; set; }
        public string? UserId { get; set; }
        public bool? Done { get; set; }
        public bool? Favorite { get; set; }
        public DateTime? CreatedTime { get; set; }

    }
}
