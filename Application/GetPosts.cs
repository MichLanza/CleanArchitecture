
using EnterpriseLayer;

namespace Application
{
    public class GetPosts
    {
        private readonly IExternalServiceAdapter<Post> _adapter;

        public GetPosts(IExternalServiceAdapter<Post> adapter)
        {
            _adapter = adapter;
        }

        public async Task<IEnumerable<Post>> ExecuteAsync()
        {
            return await _adapter.GetAsync();
        }


    }
}
