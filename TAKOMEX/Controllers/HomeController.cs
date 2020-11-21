using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAKOMEX.Models;

namespace TAKOMEX.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(String estado)
        {
            if(estado == null)
            {
                return View();
            }else if (estado == "0")
            {
                var cookie = new HttpCookie(Request.Cookies["Cookie"].Name);
                cookie.Expires = DateTime.Now.AddDays(-1);
                cookie.Value = string.Empty;
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index");
            }else
            {
                return View();
            }
            
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult header()
        {
            return View();
        }
        public ActionResult footer()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Verify(ModelLogin user)
        {
            user.nombre = "gerardoalfonso_@hotmail.com";
            user.pass = "asdfasdf";

            if (user.nombre == "gerardoalfonso_@hotmail.com" || user.pass == "asdfasdf")
            {
                if (Request.Cookies["Cookie"] != null)
                {

                }
                else
                {
                    HttpCookie cook = new HttpCookie("Cookie" , user.nombre);
                    cook.Expires = DateTime.Now.AddMinutes(30);
                    Response.Cookies.Add(cook);
                }
                return RedirectToAction("Index");
            }else
            {
                return View("Login");
            }
        }
        public ActionResult Registro()
        {
            return View();
        }
    }
}