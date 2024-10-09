using Application;
using EnterpriseLayer;

namespace PresentersAdapters
{
    public class VideoGameConsoleDetailPresenter : IPresenter<VideoGameConsole, VideoGameConsoleDetailViewModel>
    {
        public IEnumerable<VideoGameConsoleDetailViewModel> Present(IEnumerable<VideoGameConsole> consoles)
        {
            return consoles.Select(console => new VideoGameConsoleDetailViewModel
            {
                Id = console.Id,
                Name = console.Name,
                LaunchDate = console.LaunchDate.Date.ToString("dd/MM/yyy"),
                IsRetro = console.IsRetro()
            }).OrderBy(console => DateTime.Parse(console.LaunchDate));
        }
    }
}
