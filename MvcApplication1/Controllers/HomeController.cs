using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        UserInterface user;
        public HomeController(UserInterface user)
        {
            this.user = user;
        }
        public ActionResult viewad(int id, string username)
        {
            ad ad = user.getad(id, username);
            ViewBag.username = ad.user;
            return View(ad);
        }
        public ActionResult sent_message()
        {
            string email = Request["user1.email"];
            string message = Request["message"];
            string username = Request["username"];
            user.send_email(username,email,message);
            ViewBag.username = username;
            return View();
        }
        public ActionResult Index()
        {
            List<ad> lst = user.get_ads();
            ViewBag.cities = this.user.get_cities();
            ViewBag.catagory = this.user.get_catagries();
            return View(lst);
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
    }
}
