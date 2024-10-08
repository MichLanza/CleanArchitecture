using Application;
using EnterpriseLayer;

namespace PresentersAdapters
{
    public class VideoGameConsolePresenter : IPresenter<VideoGameConsole, VideoGameConsoleViewModel>
    {
        public IEnumerable<VideoGameConsoleViewModel> Present(IEnumerable<VideoGameConsole> consoles)
        {
            return consoles.Select(console => new VideoGameConsoleViewModel
            {
                Id = console.Id,
                Name = console.Name,
                LaunchDate = console.LaunchDate.Date.ToString("dd/MM/yyy"),
                IsRetro = console.IsRetro()
            }).OrderBy( console => DateTime.Parse(console.LaunchDate));
        }
    }
}
