using System.ComponentModel.DataAnnotations;
using TodoItems.Core;

namespace TodoItems.Api.Model
{
    public record ToDoItemCreateRequest
    {
        [Required]
        [StringLength(50)]
        public required string Description { get; init; }
        public bool Done { get; init; } = false;
        public bool Favorite { get; init; } = false;

        public DateTime? DueDay { get; set; }
        public string? UserId { get;set; }

        public OptionEnum option { get; set; } = OptionEnum.Manual;
    }
}
