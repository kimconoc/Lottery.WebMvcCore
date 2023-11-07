using Lottery.DoMain.Constant;
using Lottery.DoMain.Enum;
using Lottery.DoMain.Extentions;
using Lottery.DoMain.FileLog;
using Lottery.DoMain.Models;
using Lottery.Service.ServiceProvider.Interface;
using Lottery.WebMvc.MemCached.Interface;
using Lottery.WebMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Lottery.WebMvc.Controllers
{
    public class PhonebookController : BaseController
    {
        public PhonebookController(IProvider provider, IMemCached memCached) : base(provider, memCached)
        {
        }

        public IActionResult ListPlayer()
        {
            return View();
        }
        public IActionResult ExecutionPlayer(int? playerId, bool isCopy = false)
        {
            var phonebook = new Phonebook();
            if (playerId != null)
            {
                var userData = _memCached.GetCurrentUser();
                List<Phonebook> phonebooks = new List<Phonebook>();
                var dataBase = _provider.GetAsync<List<Phonebook>>(string.Format(ApiUri.Get_Phonebook + "/{0}", userData.Id));
                if (dataBase != null && dataBase.Result != null && dataBase.Result.Data != null)
                {
                    phonebooks = dataBase.Result.Data;
                }
                phonebook = phonebooks.FirstOrDefault(x => x.Id == playerId);
            }
            phonebook.IsCopy = isCopy;
            if (phonebook.IsCopy)
            {
                phonebook.Name = null;
                phonebook.PhoneNumber = null;
            }
            
            return View(phonebook);
        }

        public IActionResult RegionPlayer(int region)
        {
            ViewBag.Region = region;
            return View();
        }

        [HttpPost]
        public IActionResult ExecutePlayer(string playerJsons)
        {

            try
            {
                var userData = _memCached.GetCurrentUser();
                var players = JsonConvert.DeserializeObject<List<PhonebookModel>>(playerJsons);
                // Tạo mới and cập nhật
                players[0].UserID = userData.Id;
                if (string.IsNullOrEmpty(players[0].PhoneNumber))
                {
                    players[0].PhoneNumber = "";
                }
                var playerBase = _provider.PutAsync<object>(ApiUri.POST_UserUpdatePhonebook, players);
                if (playerBase != null || playerBase.Result != null || playerBase.Result.IsSuccessful)
                {
                    return Json(Success_Request(playerBase.Result.IsSuccessful));
                }
            }
            catch(Exception ex)
            {
                FileHelper.GeneratorFileByDay(ex.ToString(), MethodBase.GetCurrentMethod().Name);
            }

            return View(Server_Error());

        }

        [HttpPost]
        public IActionResult ExecuteDeletePlayer(int playerId)
        {
            var userData = _memCached.GetCurrentUser();
            List<Phonebook> phonebooks = new List<Phonebook>();
            var dataBase = _provider.GetAsync<List<Phonebook>>(string.Format(ApiUri.Get_Phonebook + "/{0}", userData.Id));
            if (dataBase != null && dataBase.Result != null && dataBase.Result.Data != null)
            {
                phonebooks = dataBase.Result.Data;
            }
            var phonebook = phonebooks.FirstOrDefault(x => x.Id == playerId);
            phonebook.IsDeleted = true;
            var players = new List<Phonebook>();
            players.Add(phonebook);

            // Xóa
            var playerBase = _provider.PutAsync<object>(ApiUri.POST_UserUpdatePhonebook, players);
            if (playerBase != null || playerBase.Result != null || playerBase.Result.IsSuccessful)
            {
                return Json(Success_Request(playerBase.Result.IsSuccessful));
            }

            return View(Server_Error());
        }
    }
}
