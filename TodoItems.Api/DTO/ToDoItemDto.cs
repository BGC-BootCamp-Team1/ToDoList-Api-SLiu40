using System.ComponentModel.DataAnnotations;

namespace TodoItems.Api.DTO
{
    public record ToDoItemDto
    {
        public required string Id { get; init; }
        [Required]
        [StringLength(50)]
        public required string Description { get; set; }
        public bool Done { get; set; } = false;
        public bool Favorite { get; set; } = false;
        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.UtcNow;


        public static ToDoItemDto Map(ToDoItem dto)
        {
            return new ToDoItemDto
            {
                Description = dto.Description,
                Id = dto.Id,
                Done = dto.Done,
                Favorite = dto.Favorite,
                CreatedTime = dto.CreatedTime,

            };
        }
    }
}
