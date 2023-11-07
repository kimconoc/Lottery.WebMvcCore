using Lottery.DoMain.Models;

namespace Lottery.WebMvc.MemCached.Interface
{
    public interface IMemCached
    {
        void ExecuteSaveData(User userData, bool isSaveCookies);
        void RemoteCookies();
        User GetCurrentUser();
    }
}
