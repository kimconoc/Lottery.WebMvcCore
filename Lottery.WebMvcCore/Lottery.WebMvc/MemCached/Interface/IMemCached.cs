using Lottery.DoMain.Models;
using Lottery.WebMvc.Models;

namespace Lottery.WebMvc.MemCached.Interface
{
    public interface IMemCached : IDisposable
    {
        void ExecuteSaveData(User userData);
        void ExecuteSaveUserPassword(LoginViewModel loginViewModel);
        void RemoveSavedData();
        User GetCurrentUser();
        LoginViewModel GetCurrentUserPassword();
    }
}
