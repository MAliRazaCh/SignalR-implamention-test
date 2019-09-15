using signalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace signalR.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult GetMessages()
        {
            notificationRepositery _messageRepository = new notificationRepositery();
            List<notificationTable> list = _messageRepository.GetAllMessages();

            return Json(new
            {
                list = list
            });

        }
         [HttpPost]
        public JsonResult GetNewMessages()
        {
            DatabaseEntities db = new DatabaseEntities();
            List<notificationTable> obj = db.notificationTables.Where(r => r.name == "raza").ToList();
                

            return Json(new
            {
                list = obj
            });

        }


    }
}
