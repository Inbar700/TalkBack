using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkBack.ContactsControl.Data.DTO;

namespace TalkBack.ContactsControl.Data.Services
{
    public class WebAPIService:IWebAPIService
    {
        HttpClient httpClient = new HttpClient();
        string basePath = "https://localhost:7299/api/";

        public async Task<List<UserGet>> GetAll()
        {
            HttpResponseMessage response = httpClient.GetAsync(basePath + "user/get-all").Result;
            if (response.IsSuccessStatusCode)
            {
                var usersJsonString=await response.Content.ReadAsStringAsync();
                var deserializedUsres=JsonConvert.DeserializeObject<List<UserGet>>(usersJsonString);
                return deserializedUsres;
            }
            return null;
            
        }
        //public async Task<UserGet> GetUserAsyncById(Guid id)
        //{
        //    UserGet userGet = null;
        //    var result = await httpClient.GetAsync(basePath + $"/{id}");
        //    if (result.IsSuccessStatusCode)
        //    {
        //        userGet = await result.Content.ReadAsAsync<UserGet>();
        //    }

        //    return userGet;
        //}
    }
}
