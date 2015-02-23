using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace MvcApplication1.Controllers
{
    public class userController : Controller
    {
        UserInterface user;
        public userController(UserInterface user)
        {
            this.user = user;
        }
        
        public ActionResult show_message(int id,string username)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "account");
            }
            message m=user.get_message(id,username);
            ViewBag.username = username;
            return View(m);
        }
        
        
        public ActionResult reply_message(int id, string username)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "account");
            }
            message m = user.get_message(id, username);
            ViewBag.username = username;
            return View(m);
        }
        
        
        public ActionResult messages(string username)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "account");
            }
            ViewBag.username = username;
            user u;
            if (username == null)
            {
                u = (user)TempData["user"];
                ViewBag.username = u.username;
            }
            else
                u = user.getuser(username);
            return View(u);
        }
        
        
        public ActionResult deleted(string username)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "account");
            }
            ViewBag.username = username;
            user u;
            if (username == null)
            {
                u = (user)TempData["user"];
                ViewBag.username = u.username;
            }
            else
                u = user.getuser(username);
            return View(u);
        }
        
        
        public ActionResult settings(string username)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "account");
            }
            ViewBag.username = username;
            BindCountry();
            BindState(1);
            BindCity(1);
            user u = user.getuser(username);
            return View(u);
        }
        
        
        public ActionResult savechanges(string username)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "account");
            }
            String usrname = Request["username"];
            String state=  Request["state"];
            String city = Request["city"];
            string name = Request["name"];
            return RedirectToAction("myads","ads",username);
        }
        
        
        public ActionResult viewmessage(int id, string username)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "account");
            }
            String usrname = Request["username"];
            String state = Request["state"];
            String city = Request["city"];
            string name = Request["name"];
            return RedirectToAction("myads", "ads", username);
        }
        
        
        public ActionResult reply()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "account");
            }
            String email = Request["email"];
            String subject = Request["subject"];
            String message = Request["message"];
            String user = Request["username"];
            //send message
            TempData["user"] = this.user.getuser(user);
            return RedirectToAction("messages", "user");
        }
        
        
        public ActionResult archiev(int id,string username)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "account");
            }
            user.message_Archive(id,username);
            TempData["user"] = user.getuser(username);
            return RedirectToAction("deleted", "user");
        }
        
        
        public ActionResult delete(int id,string username)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "account");
            }
            user.message_delet(id, username);
            TempData["user"] = user.getuser(username);
            return RedirectToAction("deleted", "user");
        }
        
        
        public ActionResult inbox(int id,string username)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "account");
            }
            user.message_inbox(id, username);
            TempData["user"] = user.getuser(username);
            return RedirectToAction("messages", "user");
        }


        public ActionResult personal_details()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "account");
            }
            string phone = Request["phone"];
            string name = Request["name"];
            string city = Request["addre.city1.Id"];
            string state = Request["addre.state1.Id"];
            string country = Request["addre.Country1.Id"];
            string username = Request["username"];
            user.personal_details(username,name,phone,city,state,country);
            TempData["username"] = username;
            return RedirectToAction("setting_saved", "user");
        }
        
        
        public ActionResult change_pass()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "account");
            }
            string pas1 = Request["password"];
            string pas2 = Request["password2"];
            string username = Request["username"];
            user.change_pass(username,pas2);
            TempData["username"] = username;
            return RedirectToAction("setting_saved", "user");
        }
        
        
        public ActionResult change_email()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "account");
            }
            string email = Request["email"];
            string username = Request["username"];
            user.change_email(username,email);
            TempData["username"] = username;
            return RedirectToAction("setting_saved", "user");
        }
        
        
        public ActionResult setting_saved()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "account");
            }
            ViewBag.username = (string)TempData["username"];
            return View();
        }


        public JsonResult checkpass(string pass)
        {
            string username= ((user)Session["user"]).username;

            if (user.getpass(username).Equals(pass))
                return Json(true, JsonRequestBehavior.AllowGet);
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }

        #region Private Methods
        void BindCountry()
        {
            List<Country> lstCountry = this.user.get_countries();
            ViewBag.Country = lstCountry;
        }

        void BindCity(int mState)
        {
                List<city> lstCity = this.user.get_cities(mState);
                ViewBag.City = lstCity;
        }


        void BindState(int mCountry)
        {
                List<state> lstState = this.user.get_states(mCountry);
                ViewBag.state = lstState;
        }
        #endregion

    }
}
