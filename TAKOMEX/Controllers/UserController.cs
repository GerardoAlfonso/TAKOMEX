using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TAKOMEX.Models;

namespace TAKOMEX.Controllers
{
    public class UserController : Controller
    {
        object lista ;
        List<Persona> listaL;
        double total = 0;
        DataBase db;
        // GET: User
        public new ActionResult Profile(string estado)
        {
            ValidarSesion();
            string cook = "";
            try
            {
                cook = Request.Cookies["Cookie"].Value;
            }
            catch(Exception )
            {

            }
            try
            {  
                if (cook == "")
                {
                    return View();
                }
                else
                {
                    var lst = Verify(cook);
                    if(lst == null)
                    {
                        return View();
                    }else
                    {
                        ViewData.Model = lst;
                        return View();
                    }
                    
                }
            }
            catch (Exception )
            {

            }         
            return View();
        }
        public ActionResult Cesta(int? id)
        {
            ValidarSesion();
            if(id != null)
            {
                List<int> lista = (List<int>)Session["Productos"];
                List<int> cantidades = (List<int>)Session["Cantidad"];
                lista.Remove(id.GetValueOrDefault());
                if(lista.Count() == 0 && cantidades.Count() == 0)
                {
                    Session["Productos"] = null;
                    Session["Cantidades"] = null;
                }
                else
                {
                    Session["Productos"] = lista;
                    Session["Cantidad"] = cantidades;
                }
            }
            if(Session["Productos"] == null)
            {
                return View();
            }
            else
            {
                ObtenerRegistrosCarrito();
                
                ViewBag.Lista = lista;
                ViewBag.Total = total;
                lista = "";
                total = 0;
                return View();
            }
        }
        public ActionResult ProcesarPago(string Restaurante, string TipoPago)
        {
            ValidarSesion();
            try
            {
                if (Restaurante != "" && TipoPago != "")
                {
                    Session["Restaurante"] = Restaurante;
                    Session["TipoPago"] = TipoPago;
                    ViewBag.User = Verify(Request.Cookies["Cookie"].Value);
                }
                else
                {
                    return RedirectToAction("Cesta");
                }

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.User = "Error: " + ex;
                return View();
            }
        }
        public ActionResult RevisarCompra(string email, string telefono, string direccion, string facturacion)
        {
            ValidarSesion();
            try
            {
                Session["email"] = email;
                Session["telefono"] = telefono;
                Session["direccion"] = direccion;
                Session["facturacion"] = facturacion;

                ObtenerRegistrosCarrito();
                ViewBag.Lista = lista;
                Session["lista"] = lista;
                ViewBag.Total = total;

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Lista = "Error: "+ ex;
                ViewBag.Total = "0.00";
                return View();
            }
        }
        //Recuperar los Articulos usando los Id almacenados en la session productos
        public void ObtenerRegistrosCarrito()
        {
            try
            {
                List<int> list = (List<int>)Session["Productos"];
                List<int> cant = (List<int>)Session["Cantidad"];
                List<Articulos> prod = BuscarProducto(list);
                lista = prod;
                double subtotal = 0;
                int contador = 0;
                foreach (var p in prod)
                {
                    subtotal = Convert.ToDouble(p.Precio) * cant[contador];
                    p.Cantidad = cant[contador];
                    contador++;
                    p.SubTotal = subtotal;
                    total += Convert.ToDouble(subtotal);
                }
                contador = 0;
                
            }catch(Exception ex)
            {
                lista = "Error: " + ex;
                total = 0;
            }
        }
        //Consultar los articulos en la BD usando una lista de Id
        public List<Articulos> BuscarProducto(List<int> id)
        {
            try
            {
                db = new DataBase();
                var consulta = from Articulos in db.Articulos where id.Contains(Articulos.idArticulo) select Articulos;
                return consulta.ToList();
            }catch (Exception )
            {
                return null;
            }
        }
        //Obtener los datos del usuario que ha iniciado sesion
        public object Verify(string correo)
        {
            try
            {
                db = new DataBase();
                string cook = Request.Cookies["Cookie"].Value;
                var user = db.Persona.FirstOrDefault(e => e.Correo == correo);
                return user;
            }
            catch (Exception )
            {
                return null;
            }
        }
        public ActionResult RegistrarPedido(string total)
        {
            DataBase db = new DataBase();
            //var user = Verify(Request.Cookies["Cookie"].Value);
            Persona p = new Persona();
            p = (Persona)Verify(Request.Cookies["Cookie"].Value);

            List<int> list = (List<int>)Session["Productos"];
            List<int> cant = (List<int>)Session["Cantidad"];
            List<Articulos> prod = BuscarProducto(list);
            String detalles = "";
            int contador = 0;
            if(prod.Count() == 1)
            {
                foreach (var item in prod)
                {
                    detalles += cant[contador] + ": " + item.Nombre;
                }
            }
            else
            {
                foreach (var item in prod)
                {
                    if(contador == prod.Count()-1)
                    {
                        detalles += cant[contador] + ": " + item.Nombre;
                    }else
                    {
                        detalles += cant[contador] + ": " + item.Nombre + ", ";
                    }
                    contador++;
                }
            }
            contador = 0;
            double iva = Convert.ToDouble(total) * 0.13;
            string idUsuario = Convert.ToString(p.IdPersona);

            db.sp_i_venta(Convert.ToInt32(idUsuario), detalles, Convert.ToString(Convert.ToDouble(total) - iva) , Convert.ToString(iva), total );
            Session["email"] = null;
            Session["telefono"] = null;
            Session["direccion"] = null;
            Session["facturacion"] = null;
            Session["Productos"] = null;
            Session["Cantidad"] = null;
            Session["lista"] = null;
            Session["Restaurante"] = null;
            Session["TipoPago"] = null;
            //Session.Clear();
            //Session.Abandon();
            Session["Compra"] = "OK";
            return RedirectToAction("OK" , "Products");
        }

        public ActionResult header()
        {
            return View("header");
        }
        public ActionResult footer()
        {
            return View("footer");
        }
        public ActionResult Access()
        {
            int a = 1;
            return RedirectToAction("Login", "Home", new { a } );
        }
        [HandleError]
        public ActionResult Lost()
        {
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
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

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
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

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
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
        public void ValidarSesion()
        {
            if (Session["Rol"] == null)
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
        }
    }
}
