using Application;
using DataAdapters;
using EnterpriseLayer;
using MapperAdapter;
using MapperAdapter.Dto.Request;
using Microsoft.EntityFrameworkCore;
using ModelAdapters;
using PresentersAdapters;
using RepositoryAdapters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependencias
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("GameDb"));
});
builder.Services.AddScoped<IRepository<VideoGameConsole>, Repository>();
builder.Services.AddScoped<IPresenter<VideoGameConsole, VideoGameConsoleViewModel>, VideoGameConsolePresenter>();
builder.Services.AddScoped<GetVideoConsoles<VideoGameConsole, VideoGameConsoleViewModel>>();
builder.Services.AddScoped<AddVideoGameConsole<VideoGameConsoleRequestDto>>();
builder.Services.AddScoped<IMapper<VideoGameConsoleRequestDto, VideoGameConsole>, VideoGameConsoleMapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/videogameconsole", async (GetVideoConsoles<VideoGameConsole, VideoGameConsoleViewModel> getVideoConsoles) =>
{
    return await getVideoConsoles.ExecuteAsync();
})
.WithName("GetVideoGameConsole")
.WithOpenApi();

app.MapPost("/videogameconsole", async (VideoGameConsoleRequestDto request, 
    AddVideoGameConsole<VideoGameConsoleRequestDto> addVideoGameConsole) =>
{
     await addVideoGameConsole.ExecuteAsync(request);
    return Results.Created();
})
.WithName("AddVideoGameConsole")
.WithOpenApi();

app.Run();

