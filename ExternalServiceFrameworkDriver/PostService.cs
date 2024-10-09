using System.Text.Json;
using ThirdPartiesAdapters;
using ThirdPartiesAdapters.Dtos;

namespace ExternalServiceFrameworkDriver
{
    public class PostService : IExternalService<PostServiceDto>
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;


        public PostService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };

        }

        public async Task<IEnumerable<PostServiceDto>> GetContentAsync()
        {
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();  
            return JsonSerializer.Deserialize<IEnumerable<PostServiceDto>>(responseData,_options);

        }
    }
}
