using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using TAKOMEX.Models;

namespace TAKOMEX.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(String estado)
        {
            DataBase db = new DataBase();
            var ListaRecomendaciones = db.Articulos.ToList();



            ViewData["Articulos"] = ListaRecomendaciones;


            if (estado == null)
            {
                return View(ListaRecomendaciones);
            }else if (estado == "0")
            {
                var cookie = new HttpCookie(Request.Cookies["Cookie"].Name);
                cookie.Expires = DateTime.Now.AddDays(-1);
                cookie.Value = string.Empty;
                Response.Cookies.Add(cookie);
                Session["Productos"] = null;
                return RedirectToAction("Index");
            }else
            {
                return View(ListaRecomendaciones);
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
        public ActionResult Verify(string cemail, string password)
        {
            if (!string.IsNullOrEmpty(cemail) && !string.IsNullOrEmpty(password))
            {
                DataBase db = new DataBase();
                var user = db.Persona.FirstOrDefault(e => e.Correo == cemail && e.Contraseña == password);
                //User Found
                if (user != null)
                {
                    if (Request.Cookies["Cookie"] != null)
                    {

                    }
                    else
                    {
                        HttpCookie cook = new HttpCookie("Cookie", user.Correo);
                        cook.Expires = DateTime.Now.AddMinutes(30);
                        Response.Cookies.Add(cook);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Menssaje = "Correo o contraseña incorrectos";
                    return View("Login");
                }


            }
            else
            {
                return View("login");
            }
        }

        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistroA(string nombre, string apellido, string edad, string correo, string sexo, string password)
        {
            if (IsEmpty(nombre,apellido,edad,correo,sexo,password))
            {
                if(IfExist(correo))
                {
                    ViewBag.Menssaje = "La direccion de correo ya esta registrada";
                    return View("Registro");
                }else
                {
                    DataBase db = new DataBase();
                    db.sp_i_Personas(1, nombre, apellido, Convert.ToInt32(edad), correo, sexo, password, 1);

                    var user = db.Persona.FirstOrDefault(e => e.Correo == correo && e.Contraseña == password);
                    //User Found
                    if (user != null)
                    {
                        if (Request.Cookies["Cookie"] != null)
                        {

                        }
                        else
                        {
                            HttpCookie cook = new HttpCookie("Cookie", user.Correo);
                            cook.Expires = DateTime.Now.AddMinutes(30);
                            Response.Cookies.Add(cook);
                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Menssaje = "Ocurrio un error inesperado por favor intenta mas tarde";
                        return View("Registro");
                    }
                }
            }
            else
            {
                ViewBag.Menssaje = "Debe completar todos los campos";
                return View("Registro");
            }
        }
        public Boolean IsEmpty(string nombre, string apellido, string edad, string correo, string sexo, string password)
        {
            bool value = false;
            if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(apellido) && !string.IsNullOrEmpty(edad))
            {
                if(!string.IsNullOrEmpty(correo) && !string.IsNullOrEmpty(sexo) && !string.IsNullOrEmpty(password))
                {
                    value = true;
                }
            }else
            {
                value = false;
            }


            return value;
        }
        public bool IfExist(string correo)
        {
            bool value = false;
            DataBase db = new DataBase();
            Persona user = new Persona();
            try
            {
                user = db.Persona.FirstOrDefault(e => e.Correo == correo);
            }catch (Exception e)
            {
                user = null;
            }
            
            if (user != null)
            {
                value = true;
            }else
            {
                value = false;
            }
            return value;
        }
    }
}