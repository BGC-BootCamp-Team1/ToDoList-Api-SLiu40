using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoItems.Api.DTO
{
    [BsonIgnoreExtraElements]
    public record ToDoItem
    {
        [BsonId]
        public required string Id { get; init; }
        public required string Description { get; set; }
        public required bool Done { get; set; }
        public required bool Favorite { get; set; }

        [BsonRepresentation(BsonType.String)]
        public required DateTimeOffset CreatedTime { get; init; }

        public static ToDoItem Map(ToDoItemDto dto)
        {
            return new ToDoItem
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
