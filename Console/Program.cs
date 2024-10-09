using Application;
using DataAdapters;
using EnterpriseLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PresentersAdapters;
using RepositoryAdapters;

var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfiguration configuration = builder.Build();

var container = new ServiceCollection()
    .AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlServer(configuration.GetConnectionString("GameDb"));
    })
    .AddScoped<IRepository<VideoGameConsole>, Repository>()
    .AddScoped<GetVideoConsoles<VideoGameConsole, VideoGameConsoleDetailViewModel>>()
    .AddScoped<IPresenter<VideoGameConsole, VideoGameConsoleDetailViewModel>, VideoGameConsoleDetailPresenter>()
    .BuildServiceProvider();

var getVideoGameConsoleUseCase = container.GetService<GetVideoConsoles<VideoGameConsole, VideoGameConsoleDetailViewModel>>();
var consoles = await getVideoGameConsoleUseCase.ExecuteAsync();

foreach (var console in consoles)
{
    Console.WriteLine($"{console.Id} |{console.Name} | {console.LaunchDate} | {console.IsRetro}");
};

