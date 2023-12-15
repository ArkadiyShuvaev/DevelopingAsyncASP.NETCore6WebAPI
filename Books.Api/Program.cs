using Books.Api.Services;
using DbContexts;
using Microsoft.EntityFrameworkCore;
using Polly;
using Services;

var builder = WebApplication.CreateBuilder(args);

// To test the behavior of the application when it is under load
ThreadPool.SetMaxThreads(1, 1);

builder.Services.AddHttpClient("BookCoversClient", client =>
    {
        client.BaseAddress = new Uri("http://localhost:8001");
    })
    .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))))
    .AddTransientHttpErrorPolicy(builder => builder.CircuitBreakerAsync(2, TimeSpan.FromSeconds(10)));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBooksRepository, BooksRepository>();
builder.Services.AddScoped<IBookCoversProvider, BookCoversProvider>();

builder.Services.AddDbContext<BooksDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("BooksDbConnectionString"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
