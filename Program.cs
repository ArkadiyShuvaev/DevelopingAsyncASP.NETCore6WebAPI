using DbContexts;
using Microsoft.EntityFrameworkCore;
using Services;

var builder = WebApplication.CreateBuilder(args);

ThreadPool.SetMaxThreads(1, 1);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBooksRepository, BooksRepository>();

builder.Services.AddDbContext<BooksDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("BooksDbConnectionString"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
