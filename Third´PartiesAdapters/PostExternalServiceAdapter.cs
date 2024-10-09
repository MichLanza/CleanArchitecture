using Application;
using EnterpriseLayer;
using ThirdPartiesAdapters.Dtos;

namespace ThirdPartiesAdapters
{
    public class PostExternalServiceAdapter : IExternalServiceAdapter<Post>
    {
        private readonly IExternalService<PostServiceDto> _service;

        public PostExternalServiceAdapter(IExternalService<PostServiceDto> service)
        {
            _service = service;
        }

        public async Task<IEnumerable<Post>> GetAsync()
        {
            var result = await _service.GetContentAsync();
            var post = result.Select(p => new Post { Id = p.Id, Body = p.Body, Title = p.Title });
            return post;
        }
    }
}
