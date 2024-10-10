using API.Middleware;
using API.Validators;
using Application;
using DataAdapters;
using EnterpriseLayer;
using ExternalServiceFrameworkDriver;
using FluentValidation;
using FluentValidation.AspNetCore;
using MapperAdapter;
using MapperAdapter.Dto.Request;
using Microsoft.EntityFrameworkCore;
using ModelAdapters;
using PresentersAdapters;
using RepositoryAdapters;
using ThirdPartiesAdapters;
using ThirdPartiesAdapters.Dtos;

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
builder.Services.AddScoped<IPresenter<VideoGameConsole, VideoGameConsoleDetailViewModel>, VideoGameConsoleDetailPresenter>();
builder.Services.AddScoped<GetVideoConsoles<VideoGameConsole, VideoGameConsoleViewModel>>();
builder.Services.AddScoped<GetVideoConsoles<VideoGameConsole, VideoGameConsoleDetailViewModel>>();
builder.Services.AddScoped<AddVideoGameConsole<VideoGameConsoleRequestDto>>();
builder.Services.AddScoped<IExternalService<PostServiceDto>, PostService>();
builder.Services.AddScoped<IExternalServiceAdapter<Post>, PostExternalServiceAdapter>();
builder.Services.AddScoped<GetPosts>();
builder.Services.AddScoped<IMapper<VideoGameConsoleRequestDto, VideoGameConsole>, VideoGameConsoleMapper>();
builder.Services.AddValidatorsFromAssemblyContaining<VideoGameConsoleValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddScoped<GenerateSale<SaleRequestDto>>();
builder.Services.AddScoped<GetSales>();
builder.Services.AddScoped<GetSalesSearch<SaleModel>>();

builder.Services.AddScoped<IMapper<SaleRequestDto, Sale>, SaleMapper>();
builder.Services.AddScoped<IRepository<Sale>,SaleRepository>();
builder.Services.AddScoped<IRepositorySearch<SaleModel,Sale>, SaleRepository>();





builder.Services.AddHttpClient<IExternalService<PostServiceDto>,PostService>(op =>
{
    op.BaseAddress = new Uri(builder.Configuration["ExternalService"]);
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();

app.MapGet("/videogameconsole", async (GetVideoConsoles<VideoGameConsole, VideoGameConsoleViewModel> getVideoConsoles) =>
{
    return await getVideoConsoles.ExecuteAsync();
})
.WithName("GetVideoGameConsole")
.WithOpenApi();

app.MapGet("/videogameconsole/extended", async (GetVideoConsoles<VideoGameConsole, VideoGameConsoleDetailViewModel> getVideoConsolesDetail) =>
{
    return await getVideoConsolesDetail.ExecuteAsync();
})
.WithName("GetVideoGameConsoleExtended")
.WithOpenApi();

app.MapPost("/videogameconsole", async (VideoGameConsoleRequestDto request,
    AddVideoGameConsole<VideoGameConsoleRequestDto> addVideoGameConsole,
    IValidator<VideoGameConsoleRequestDto> validate) =>
{
    var result = await validate.ValidateAsync(request);

    if (!result.IsValid)
        return Results.ValidationProblem(result.ToDictionary());

    await addVideoGameConsole.ExecuteAsync(request);
    return Results.Created();
})
.WithName("AddVideoGameConsole")
.WithOpenApi();


app.MapGet("/post", async (GetPosts getPost) =>
{
    return await getPost.ExecuteAsync();
})
.WithName("GetPosts")
.WithOpenApi();


app.MapPost("/sale", async (SaleRequestDto saleDto , GenerateSale<SaleRequestDto> saleUseCase) =>
{
     await saleUseCase.ExecuteAsync(saleDto);
    return Results.Created();
})
.WithName("GenerateSale")
.WithOpenApi();

app.MapGet("/sale", async (GetSales saleUseCase) =>
{
    return await saleUseCase.ExecuteAsync();
})
.WithName("GetSales")
.WithOpenApi();

app.MapGet("/sale/{total}", async (GetSalesSearch<SaleModel> saleUseCase, decimal total) =>
{
    return await saleUseCase.ExecuteAsync(s => s.Total > total);
})
.WithName("GetSalesSearch")
.WithOpenApi();

app.Run();

