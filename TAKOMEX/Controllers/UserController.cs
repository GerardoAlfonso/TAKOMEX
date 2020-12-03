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
        double total = 0;
        DataBase db;
        // GET: User
        public ActionResult Profile(string estado)
        {
            string cook = "";
            try
            {
                cook = Request.Cookies["Cookie"].Value;
            }
            catch(Exception e)
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
            catch (Exception e)
            {

            }         
            return View();
        }
        public ActionResult Cesta(int? id)
        {
            if(id != null)
            {
                List<int> lista = (List<int>)Session["Productos"];
                lista.Remove(id.GetValueOrDefault());
                if(lista.Count() == 0)
                {
                    Session["Productos"] = null;
                }
                else
                {
                    Session["Productos"] = lista;
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
            try
            {
                Session["email"] = email;
                Session["telefono"] = telefono;
                Session["direccion"] = direccion;
                Session["facturacion"] = facturacion;

                ObtenerRegistrosCarrito();
                ViewBag.Lista = lista;
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
                db = new DataBase();
                List<Articulos> prod = BuscarProducto(list);
                lista = prod;
                foreach (var p in prod)
                {
                    total += Convert.ToDouble(p.Precio);
                }
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
            }catch (Exception ex)
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
            catch (Exception ex)
            {
                return null;
            }
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
            return View();
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
    }
}
