using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkBack.AccessControl.Data.DTOs;

namespace TalkBack.AccessControl.Data.Services
{
    public class WebAPIService : IWebAPIService
    {
        HttpClient httpClient = new HttpClient();
        string basePath = "https://localhost:7014/";
        public async Task<UserFullDetails> GetUserAsyncById(Guid id)
        {
            HttpResponseMessage response = httpClient.GetAsync(basePath + $"/contacts/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var userJsonString=await response.Content.ReadAsStringAsync();
                var deserializedUser=JsonConvert.DeserializeObject<UserFullDetails>(userJsonString);
                return deserializedUser;
            }

            return null;
        }
    }
}
