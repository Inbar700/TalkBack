using TalkBack.ChatControl.DTO;

namespace TalkBack.ChatControl.Services
{
    public interface IWebAPIService
    {
        Task<List<UserGet>> GetAll();
    }
}
