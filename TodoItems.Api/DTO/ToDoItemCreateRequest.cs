using System.ComponentModel.DataAnnotations;

namespace TodoList.DTO
{
    public record ToDoItemCreateRequest
    {
        [Required]
        [StringLength(50)]
        public required string Description { get; init; }
        public bool Done { get; init; } = false;
        public bool Favorite { get; init; } = false;
    }
}
