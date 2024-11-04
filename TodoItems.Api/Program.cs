using TodoItems.Api.Config;
using TodoItems.Api.Filter;
using TodoItems.Api.Service;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers(
            options =>
            {
                options.Filters.Add<CustomExceptionFilter>();
            }
        );
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.Configure<ToDoItemDatabaseSettings>(builder.Configuration.GetSection("ToDoItemDatabase"));
        builder.Services.AddSingleton<IToDoItemService, ToDoItemService>();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        app.MapControllers();

        app.Run();
    }
}