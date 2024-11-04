using MongoDB.Driver;
using System.Text;
using TodoItems.Api.Model;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using System.Net;
using TodoItems.Core.Model;
using TodoItems.Infrastructure;
using Google.Protobuf.WellKnownTypes;
using TodoItems.Core;


namespace TodoItems.ApiTest
{
    public class TodoItemApiTest : IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        private IMongoCollection<ToDoItem> _mongoCollection;

        public TodoItemApiTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();

            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var mongoDatabase = mongoClient.GetDatabase("TodoItems");
            _mongoCollection = mongoDatabase.GetCollection<ToDoItem>("TodoItems");
        }

        public async Task InitializeAsync()
        {
            await _mongoCollection.DeleteManyAsync(FilterDefinition<ToDoItem>.Empty);
        }

        public Task DisposeAsync() => Task.CompletedTask;

        [Fact]
        public async void Should_create_todo_item()
        {
            var todoItemRequst = new ToDoItemCreateRequest()
            {
                Description = "test create",
                Done = false,
                Favorite = true,
                UserId = "user1",
                option = OptionEnum.Freest
            };

            var json = JsonSerializer.Serialize(todoItemRequst);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/todoitems", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();

            var returnedTodos = JsonSerializer.Deserialize<TodoItemVO>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.NotNull(returnedTodos);
            Assert.Equal("test create", returnedTodos.Description);
            Assert.False(returnedTodos.Favorite);
            Assert.False(returnedTodos.Done);
        }

        [Fact]
        public async void Should_create_todo_item_v2()
        {
            var todoItemRequst = new ToDoItemCreateRequest()
            {
                Description = "test create",
                Done = false,
                Favorite = true,
                DueDay = DateTime.Now,
                UserId = "user1"
            };

            var json = JsonSerializer.Serialize(todoItemRequst);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v2/todoitems", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();

            var todoItemReponse = JsonSerializer.Deserialize<TodoItemVO>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.NotNull(todoItemReponse);
            Assert.Equal("test create", todoItemReponse.Description);

        }

        [Fact]
        public async void Should_Update_todo_item_v2()
        {
            string id = "5f9a7d8e2d3b4a1eb8a7d8e2";

            var mockItem = new ToDoItem
            {
                Id = id,
                Description = "Buy groceries",
                Favorite = false,
                Done = false,
                CreatedTime= DateTime.Now,
            }; ;
            await _mongoCollection.InsertOneAsync(mockItem);

            var todoItemRequst = new TodoItemUpdateRequest()
            {
                Id = id,
                Description = "test create",
                Done = false,
                Favorite = true,
                UserId = "user1"
            };

            var json = JsonSerializer.Serialize(todoItemRequst);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync($"/api/v2/todoitems/{id}", content);


            var responseContent = await response.Content.ReadAsStringAsync();

            var todoItemReponse = JsonSerializer.Deserialize<TodoItemVO>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.NotNull(todoItemReponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.Equal("test create", todoItemReponse.Description);
        }



    }
}
