using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAKOMEX.Models;

namespace TAKOMEX.Controllers
{
    public class AdminController : Controller
    {
        DataBase db;
        // GET: Admin
        public ActionResult Mensajes()
        {
            if (Session["Rol"] == null)
            {
                ValidarSesion();
            }
            db = new DataBase();
            List<MensajesDetalles> ms = new List<MensajesDetalles>();
            //var mensaje = db.Mensajes.Take(50).ToList();
            //ViewBag.Mensajes = mensaje;
            var mensajes = from Mensajes in db.Mensajes
                                 join Persona in db.Persona on Mensajes.idPersona equals Persona.IdPersona
                                 select new { Mensajes.idMensaje, Mensajes.Asunto, Mensajes.Mensaje,Mensajes.Estado, Mensajes.Created_at, Persona.Correo };

            mensajes.ToList();
            foreach(var m in mensajes)
            {
                MensajesDetalles obj = new MensajesDetalles();
                obj.IdMensaje = m.idMensaje;
                obj.Correo = m.Correo;
                obj.Asunto = m.Asunto;
                obj.Mensaje = m.Mensaje;
                obj.Estado = m.Estado;
                obj.Created_at = Convert.ToString(m.Created_at);
                ms.Add(obj);
            }
            ViewBag.Mensajes = ms;



            return View();
        }

        public void UpdateMensaje(int id)
        {
            int estado = 1;
            db = new DataBase();
            db.sp_u_mensajes(id, estado);
        }
        public ActionResult ContenidoMensaje(int id)
        {
            if (Session["Rol"] == null)
            {
                ValidarSesion();
            }
            UpdateMensaje(id);
            db = new DataBase();
            List<MensajesDetalles> ms = new List<MensajesDetalles>();
            var mensajes = from Mensajes in db.Mensajes
                           join Persona in db.Persona on Mensajes.idPersona equals Persona.IdPersona
                           where Mensajes.idMensaje == id
                           select new { Mensajes.idMensaje, Mensajes.Asunto, Mensajes.Mensaje, Mensajes.Estado, Mensajes.Created_at, Persona.Correo };

            mensajes.ToList();
            foreach (var m in mensajes)
            {
                MensajesDetalles obj = new MensajesDetalles();
                obj.IdMensaje = m.idMensaje;
                obj.Correo = m.Correo;
                obj.Asunto = m.Asunto;
                obj.Mensaje = m.Mensaje;
                obj.Estado = m.Estado;
                obj.Created_at = Convert.ToString(m.Created_at);
                ms.Add(obj);
            }
            ViewBag.Mensajes = ms;

            return View();
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
        public ActionResult Usuarios(int? mensaje, int? all)
        {
            if (Session["Rol"] == null)
            {
                ValidarSesion();
            }
            if (Session["Estado"] != null)
            {
                if (Session["Estado"].ToString() == "1")
                {
                    ViewBag.Mensaje = "Usuario Eliminado correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error durante el proceso por favor intenta de nuevo mas tarde";
                }
                Session["Estado"] = null;
            }
            db = new DataBase();
            List<Persona> personas = new List<Persona>();
            var listado = from Personas in db.Persona
                          join Rol in db.Roles on Personas.idRol equals Rol.idRol
                          select new { Rol.NombreRol,Personas.IdPersona, Personas.Nombre, Personas.Apellido, Personas.Edad, Personas.Correo, Personas.Sexo, Personas.Estado, Personas.Created_at, Personas.Updated_at };
            listado.ToList();
            foreach(var p in listado)
            {
                Persona obj = new Persona();
                obj.NombreRol = p.NombreRol;
                obj.IdPersona = p.IdPersona;
                obj.Nombre = p.Nombre;
                obj.Apellido = p.Apellido;
                obj.Edad = p.Edad;
                obj.Correo = p.Correo;
                obj.Sexo = p.Sexo;
                obj.Estado = p.Estado;
                obj.Created_at = p.Created_at;
                obj.Updated_at = p.Updated_at;
                personas.Add(obj);
            }
            ViewBag.Personas = personas;
            if(all != null)
            {
                ViewBag.Active = all;
            }
            else
            {
                ViewBag.Active = null;
            }

            return View();
        }
        public ActionResult DetallesUsuario(int id)
        {
            if (Session["Rol"] == null)
            {
                ValidarSesion();
            }
            db = new DataBase();
            List<Persona> personas = new List<Persona>();
            var listado = from Personas in db.Persona
                          join Rol in db.Roles on Personas.idRol equals Rol.idRol
                          where Personas.IdPersona == id
                          select new { Rol.NombreRol, Personas.IdPersona, Personas.Nombre, Personas.Apellido, Personas.Edad, Personas.Correo, Personas.Sexo, Personas.Estado, Personas.Created_at, Personas.Updated_at };
            listado.ToList();
            foreach (var p in listado)
            {
                Persona obj = new Persona();
                obj.NombreRol = p.NombreRol;
                obj.IdPersona = p.IdPersona;
                obj.Nombre = p.Nombre;
                obj.Apellido = p.Apellido;
                obj.Edad = p.Edad;
                obj.Correo = p.Correo;
                obj.Sexo = p.Sexo;
                obj.Estado = p.Estado;
                obj.Created_at = p.Created_at;
                obj.Updated_at = p.Updated_at;
                personas.Add(obj);
            }
            ViewBag.Persona = personas;

            return View();
        }
        public ActionResult Productos()
        {
            if (Session["Rol"] == null)
            {
                ValidarSesion();
            }
            db = new DataBase();
            List<Articulos> productos = new List<Articulos>();
            var listado = from Productos in db.Articulos
                          join Categoria in db.Categorias on Productos.IdCategoria equals Categoria.idCategoria
                          select new { Categoria.NombreCategoria, Productos.idArticulo , Productos.Nombre,
                              Productos.Descripcion_corta, Productos.Descripcion_larga, Productos.IMG,
                              Productos.Precio, Productos.Estado, Productos.Created_at, Productos.Updated_at };  
            listado.ToList();
            foreach (var p in listado)
            {
                Articulos obj = new Articulos();
                obj.NombreCategoria = p.NombreCategoria;
                obj.idArticulo = p.idArticulo;
                obj.Nombre = p.Nombre;
                obj.Descripcion_corta = p.Descripcion_corta;
                obj.Descripcion_larga = p.Descripcion_larga;
                obj.IMG = p.IMG;
                obj.Precio = p.Precio;
                obj.Estado = p.Estado;
                obj.Created_at = p.Created_at;
                obj.Updated_at = p.Updated_at;
                productos.Add(obj);
            }
            ViewBag.Personas = productos;
            return View();
        }
        public ActionResult DetallesProducto(int id)
        {
            if (Session["Rol"] == null)
            {
                ValidarSesion();
            }
            db = new DataBase();
            List<Articulos> productos = new List<Articulos>();
            var listado = from Productos in db.Articulos
                          join Categoria in db.Categorias on Productos.IdCategoria equals Categoria.idCategoria
                          where Productos.idArticulo == id
                          select new
                          {
                              Categoria.NombreCategoria,
                              Productos.idArticulo,
                              Productos.Nombre,
                              Productos.Descripcion_corta,
                              Productos.Descripcion_larga,
                              Productos.IMG,
                              Productos.Precio,
                              Productos.Estado,
                              Productos.Created_at,
                              Productos.Updated_at
                          };
            listado.ToList();
            foreach (var p in listado)
            {
                Articulos obj = new Articulos();
                obj.NombreCategoria = p.NombreCategoria;
                obj.idArticulo = p.idArticulo;
                obj.Nombre = p.Nombre;
                obj.Descripcion_corta = p.Descripcion_corta;
                obj.Descripcion_larga = p.Descripcion_larga;
                obj.IMG = p.IMG;
                obj.Precio = p.Precio;
                obj.Estado = p.Estado;
                obj.Created_at = p.Created_at;
                obj.Updated_at = p.Updated_at;
                productos.Add(obj);
            }
            ViewBag.Producto = productos;
            return View();
        }
        public ActionResult DesactivarUsuario(int id)
        {
            try
            {
                db = new DataBase();
                db.sp_e_Personas(id, 0);
                Session["Estado"] = 1;
            }
            catch (Exception)
            {
                Session["EStado"] = 0;
            }

            return RedirectToAction("Usuarios");
        }
        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult Access()
        {
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

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
