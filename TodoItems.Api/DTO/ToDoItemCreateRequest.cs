using System.ComponentModel.DataAnnotations;

namespace TodoItems.Api.DTO
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
