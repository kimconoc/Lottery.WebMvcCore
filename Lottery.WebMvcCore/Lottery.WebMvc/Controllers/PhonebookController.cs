using Lottery.DoMain.Constant;
using Lottery.WebMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lottery.WebMvc.Controllers
{
    public class PhonebookController : BaseController
    {
        public IActionResult ListPlayer()
        {
            return View();
        }
        public IActionResult CreatePlayer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ExecuteCreatePlayer(string playerJsons)
        {
            var userData = GetCurrentUser();
            var players = JsonConvert.DeserializeObject<List<PhonebookModel>>(playerJsons);
            // Tạo mới
            if (players[0].Id == null)
            {
                players[0].IsDeleted = false;
                players[0].PhoneNumber = "";
                var playerBase = provider.PutAsync<object>(ApiUri.POST_UserUpdatePhonebook, players);
                if (playerBase != null || playerBase.Result != null || playerBase.Result.IsSuccessful)
                {
                    return Json(Success_Request(playerBase.Result.IsSuccessful));
                }
            }
            // Update
            else if (players[0].Id != null && !players[0].IsDeleted)
            {
                if (string.IsNullOrEmpty(players[0].PhoneNumber))
                {
                    players[0].PhoneNumber = "";
                }
                var playerBase = provider.PutAsync<object>(ApiUri.POST_UserUpdatePhonebook, players);
                if (playerBase != null || playerBase.Result != null || playerBase.Result.IsSuccessful)
                {
                    return Json(Success_Request(playerBase.Result.IsSuccessful));
                }
            }

            return View(Server_Error());

        }
    }
}
