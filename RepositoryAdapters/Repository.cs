using DataAdapters;
using Application;
using Microsoft.EntityFrameworkCore;
using ModelAdapters;
using EnterpriseLayer;

namespace RepositoryAdapters
{
    public class Repository : IRepository<VideoGameConsole>
    {
        private readonly AppDbContext _dbContext;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(VideoGameConsole videoGameConsole)
        {
            var videoGameConsoleModel = new VideoGameConsoleModel()
            {
                Id = videoGameConsole.Id,
                Name = videoGameConsole.Name,
                LaunchDate = videoGameConsole.LaunchDate,
            };
            await _dbContext.VideoGameConsoles.AddAsync(videoGameConsoleModel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<VideoGameConsole>> GetAllAsync()
        {
            return await _dbContext.VideoGameConsoles
                .Select(v => new VideoGameConsole
                {
                    Id = v.Id,
                    Name = v.Name,
                    LaunchDate = v.LaunchDate,
                })
            .ToListAsync();
        }

        public async Task<VideoGameConsole> GetByIdAsync(int id)
        {
            var videoGameConsoleModel = await _dbContext.VideoGameConsoles.FirstOrDefaultAsync(x => x.Id == id);
            return new VideoGameConsole
            {
                Id = videoGameConsoleModel.Id,
                Name = videoGameConsoleModel.Name,
                LaunchDate = videoGameConsoleModel.LaunchDate,
            };
        }
    }
}
