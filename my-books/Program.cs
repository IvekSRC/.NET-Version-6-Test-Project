using Microsoft.EntityFrameworkCore;
using my_books.Data;
using my_books.Data.Models;
using my_books.Data.Services;
using my_books.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

string connString = builder.Configuration.GetConnectionString("DefaultConnectionString");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connString);
});

builder.Services.AddTransient<BooksService>();
builder.Services.AddTransient<AuthorService>();
builder.Services.AddTransient<PublishersService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//Exception Handling
app.ConfigureBuildInExceptionHandler();
//app.ConfigureCustomExceptionHandler();

app.MapControllers();

AppDbInitializer.Seed(app);

app.Run();
