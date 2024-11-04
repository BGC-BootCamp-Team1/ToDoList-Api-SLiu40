using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TodoItems.Api.Config;
using TodoItems.Api.DTO;

namespace TodoItems.Api.Service
{
    public class ToDoItemService : IToDoItemService
    {

        private readonly IMongoCollection<ToDoItem> _ToDoItemsCollection;

        public ToDoItemService(
            IOptions<ToDoItemDatabaseSettings> ToDoItemStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                ToDoItemStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ToDoItemStoreDatabaseSettings.Value.DatabaseName);

            _ToDoItemsCollection = mongoDatabase.GetCollection<ToDoItem>(
                ToDoItemStoreDatabaseSettings.Value.CollectionName);
        }

        public async Task<ToDoItem> CreateAsync(ToDoItemDto newToDoItem)
        {
            ToDoItem toDoItem = ToDoItem.Map(newToDoItem);
            await _ToDoItemsCollection.InsertOneAsync(toDoItem);
            return toDoItem;
        }

        public async Task<List<ToDoItemDto>> GetAsync()
        {
            List<ToDoItem> toDoItems = _ToDoItemsCollection.FindAsync<ToDoItem>(Builders<ToDoItem>.Filter.Empty).Result.ToList();

            List<ToDoItemDto> toDoItemDtos = new List<ToDoItemDto>();

            toDoItems.ForEach(toDoItem => toDoItemDtos.Add(ToDoItemDto.Map(toDoItem)));
            return toDoItemDtos;

        }

        public async Task<ToDoItemDto?> GetAsync(string id)
        {
            var filter = Builders<ToDoItem>.Filter
                .Eq(r => r.Id, id);

            ToDoItem toDoItem = _ToDoItemsCollection.FindAsync(filter).Result.FirstOrDefault();
            if (toDoItem != null)
            {
                return ToDoItemDto.Map(toDoItem);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var filter = Builders<ToDoItem>.Filter
                .Eq(r => r.Id, id);

            DeleteResult deleteResult = await _ToDoItemsCollection.DeleteOneAsync(filter);

            if (deleteResult.DeletedCount >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public async Task ReplaceAsync(string id, ToDoItemDto updatedToDoItem)
        {
            var filter = Builders<ToDoItem>.Filter
                .Eq(r => r.Id, id);

            ToDoItem toDoItem = _ToDoItemsCollection.FindAsync(filter).Result.FirstOrDefault();

            if (toDoItem != null)
            {
                await _ToDoItemsCollection.ReplaceOneAsync(filter, ToDoItem.Map(updatedToDoItem));

            }
            else
            {
                await _ToDoItemsCollection.InsertOneAsync(ToDoItem.Map(updatedToDoItem));

            }
        }
    }
}
