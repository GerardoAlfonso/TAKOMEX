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
            DataBase db = new DataBase();
            var ListaRecomendaciones = db.Articulos.ToList().Take(6);
            if (Session["Rol"] == null)
            {
                ValidarSesion();
            }

            ViewData["Articulos"] = ListaRecomendaciones;

            if (estado == null)
            {
                return View(ListaRecomendaciones);
            }else if (estado == "0")
            {
                Session["Productos"] = null;
                try
                {
                    if (Request.Cookies["Cookie"].Name != null)
                    {
                        var cookie = new HttpCookie(Request.Cookies["Cookie"].Name);
                        cookie.Expires = DateTime.Now.AddDays(-1);
                        cookie.Value = string.Empty;
                        Response.Cookies.Add(cookie);
                    }
                } catch (Exception) { }
                return RedirectToAction("Index");
            }else
            {
                return View(ListaRecomendaciones);
            }
        }
        public void ValidarSesion()
        {
            try
            {
                if (Request.Cookies["Cookie"].Name != null)
                {
                    var cookie = new HttpCookie(Request.Cookies["Cookie"].Name);
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    cookie.Value = string.Empty;
                    Response.Cookies.Add(cookie);
                }
            }
            catch (Exception)
            {

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
        public ActionResult GuardarMensaje(string asunto, string message )
        {
            DataBase db = new DataBase();
            Persona p = new Persona();
            p = GetUser(Request.Cookies["Cookie"].Value);

            db.sp_i_mensaje(p.IdPersona ,asunto, message, 0);
            Session["Mensaje"] = "OK";
            return RedirectToAction("OK" , "Home");
        }
        public ActionResult OK()
        {
            return View("OK");
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
        public ActionResult Login(int? a)
        {
            if(a != null)
            {
                ViewBag.Mensaje = "Debe iniciar sesion para continuar.";
            }
            return View();
        }

        public Persona GetUser(string correo)
        {
            try
            {
                DataBase db = new DataBase();
                Persona p = new Persona();
                p = db.Persona.FirstOrDefault(e => e.Correo == correo);                
                
                return p;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult Verify(string cemail, string password)
        {
            if (!string.IsNullOrEmpty(cemail) && !string.IsNullOrEmpty(password))
            {
                DataBase db = new DataBase();
                ViewBag.Mensaje = null;
                var user = db.Persona.FirstOrDefault(e => e.Correo == cemail && e.Contraseña == password);
                //User Found
                if (user != null)
                {
                    if(user.Estado == 1)
                    {
                        if (Request.Cookies["Cookie"] != null)
                        {

                        }
                        else
                        {
                            HttpCookie cook = new HttpCookie("Cookie", user.Correo);
                            cook.Expires = DateTime.Now.AddMinutes(30);
                            Session["Rol"] = user.idRol;
                            Response.Cookies.Add(cook);
                        }
                        if (user.idRol == 4)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return RedirectToAction("Mensajes", "Admin");
                        }
                    }else
                    {
                        ViewBag.Mensaje = "La cuenta a la que intentas acceder se encuentra deshabilitada, ponte en contacto con los administradores del sistema.";
                        return View("Login");
                    }
                }
                else
                {
                    ViewBag.Mensaje = "Correo o contraseña incorrectos";
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
                            Session["Rol"] = user.idRol;
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
            }catch (Exception)
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