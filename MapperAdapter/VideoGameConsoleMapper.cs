using Application;
using EnterpriseLayer;
using MapperAdapter.Dto.Request;

namespace MapperAdapter
{
    public class VideoGameConsoleMapper : IMapper<VideoGameConsoleRequestDto, VideoGameConsole>
    {
        public VideoGameConsole Map(VideoGameConsoleRequestDto dto) => new VideoGameConsole
        {
            Name = dto.Name,
            LaunchDate = dto.LaunchDate,
        };
    }
}
