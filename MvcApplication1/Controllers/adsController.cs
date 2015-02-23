using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class adsController : Controller
    {
        UserInterface user;

        public adsController(UserInterface user)
        {
            this.user = user;
        }

        public ActionResult myads(string username)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "account");
            }

            user u = (user)TempData["user"];
            if (u != null)
            {
                ViewBag.username = u.username;
            }
            else
            {
                ViewBag.username = username;
                u = user.getuser(username);
            }
            return View(u);
        }

        public ActionResult inactive(string username)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "account");
            }
            user u = (user)TempData["user"];
            if (u != null)
            {
                ViewBag.username = u.username;
            }
            else
            {
                ViewBag.username = username;
                u = user.getuser(username);
            }
            return View(u);
        }
        public ActionResult newad(string username)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "account");
            }
            ViewBag.username = username;
            ViewBag.Catagory = user.get_catagories();
            ad a = new ad();
            return View(a);
        }
        public ActionResult viewad(int id, string username)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "account");
            }
            ad ad = user.getad(id, username);
            ViewBag.username = ad.user;
            return View(ad);
        }
        public ActionResult edit(int id, string username)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "account");
            }
            ad ad = user.getad(id, username);
            ViewBag.Catagory = user.get_catagories();
            ViewBag.username = ad.user;
            return View(ad);
        }

        public ActionResult adposted()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "account");
            }
            ViewBag.id = (int)TempData["id"];
            ViewBag.username = (string)TempData["username"]; ;
            return View();
        }

        //======================================================
        public ActionResult newpost()
        {
            String usr = Request["username"];
            string path = "/images/no_pic_available.jpg";
            HttpPostedFileBase file = Request.Files["uploaded_file"];
            if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
            {
                string fileName = file.FileName;
                fileName = usr + "-" + fileName;
                // string fileContentType = file.ContentType;
                path = Path.Combine(Server.MapPath("/images"), fileName);
                //  byte[] fileBytes = new byte[file.ContentLength];
                //  file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                file.SaveAs(path);
                path = "/images/" + fileName;
            }

            String title = Request["title"];
            String description = Request["description"];
            String catagory = Request["catagory1.Id"];

            TempData["id"] = user.newad(usr, title, catagory, description, path);
            TempData["username"] = usr;
            return RedirectToAction("adposted", "ads");
        }

        public ActionResult deactivate(int id, string username)
        {
            user.ad_deactivate(id, username);
            TempData["user"] = user.getuser(username);
            return RedirectToAction("inactive", "ads");
        }
        public ActionResult activate(int id, string username)
        {
            user.ad_activate(id, username);
            TempData["user"] = user.getuser(username);
            return RedirectToAction("myads", "ads");
        }

        public ActionResult delete(int id, string username)
        {
            user.ad_delet(id, username);
            TempData["user"] = user.getuser(username);
            return RedirectToAction("inactive", "ads");
        }
        public ActionResult editpost()
        {
            String usr = Request["username"];
            String id = Request["id"];
            String title = Request["title"];
            String description = Request["description"];
            String catagory = Request["catagory1.Id"];
            TempData["id"] = user.edit(usr, id, title, catagory, description);
            TempData["username"] = usr;
            return RedirectToAction("adposted", "ads");
        }

    }
}
