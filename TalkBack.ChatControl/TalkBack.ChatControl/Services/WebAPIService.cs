using Newtonsoft.Json;
using TalkBack.ChatControl.DTO;

namespace TalkBack.ChatControl.Services
{
    public class WebAPIService : IWebAPIService
    {
        HttpClient httpClient = new HttpClient();
        string basePath = "https://localhost:7299/api/";

        public async Task<List<UserGet>> GetAll()
        {
            HttpResponseMessage response = httpClient.GetAsync(basePath + "user/get-all").Result;
            if (response.IsSuccessStatusCode)
            {
                var usersJsonString = await response.Content.ReadAsStringAsync();
                var deserializedUsres = JsonConvert.DeserializeObject<List<UserGet>>(usersJsonString);
                return deserializedUsres;
            }
            return null;

        }
    }
}
