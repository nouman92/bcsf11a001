using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class AccountController : Controller
    {
        UserInterface user;

        public AccountController(UserInterface user)
        {
            this.user = user;
        }

        public ActionResult Login()
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("myads", "ads");
            }
            return View();
        }

        public ActionResult Newaccount()
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("myads", "ads");
            }
            BindCountry();
            BindState(1);
            BindCity(1);
            return View();
        }

        public ActionResult newpassword()
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("myads", "ads");
            }
            return View();
        }

        public ActionResult invalid()
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("myads", "ads");
            }
            return View();
        }

        public ActionResult Logout(string username)
        {
            Session.Remove("user");
            return RedirectToAction("Index", "home");
        }

        //==================================

        [HttpPost]
        public ActionResult resetpass()
        {
            string email = Request["email"];
            string pass = Request["password"];
            string pass2 = Request["password2"];
            //email new password
            ViewBag.email = email;
            return View();
        }

        [HttpPost]
        public ActionResult register()
        {
            string username = Request["username"];
            string pass = Request["password"];
            string name = Request["name"];
            string city = Request["addre.city1.Id"];
            string state = Request["addre.state1.Id"];
            string country = Request["addre.Country1.Id"];
            string email = Request["email"];
            string phone = Request["phone"];
            TempData["user"] = user.adduser(username, pass, email, name, country, state, city, phone);
            return RedirectToAction("myads", "ads");
        }

        public JsonResult check(string username)
        {
            user u = user.getuser(username);
            if (u == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult validdate()
        {
            user u = user.validate(Request["username"], Request["password"]);
            if (u != null)
            {
                TempData["user"] = u;
                Session["user"] = u;
                return RedirectToAction("myads", "ads");
            }
            else
                return RedirectToAction("invalid", "Account");
        }

        [HttpGet]
        public JsonResult CityList(int mState)
        {
            List<city> lst = user.get_cities(mState);
            return Json(new SelectList(lst.ToArray(), "Id", "name"), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult StateList(int mCountry)
        {
            List<state> lst = user.get_states(mCountry);
            return Json(new SelectList(lst.ToArray(), "Id", "name"), JsonRequestBehavior.AllowGet);
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
