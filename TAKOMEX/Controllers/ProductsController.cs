using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAKOMEX.Models;

namespace TAKOMEX.Controllers
{
    public class ProductsController : Controller
    {
        public int idCategoria = 0;
        // GET: Products
        public ActionResult Producto(int id)
        {
            
            try
            {
                var lst = VerifyProduct(id);
                var rec = VerifyRecommendation(idCategoria);
                if (lst == null || rec == null)
                {
                    return View();
                }
                else
                {
                    ViewBag.Recomendacion = rec;
                    ViewBag.Art = lst;
                    return View();
                }
            }
            catch (Exception )
            {

            }
            return View();
        }
        public ActionResult Products(string param)
        {
            try
            {
                if(param == null || param == "")
                {
                    ViewBag.Productos = GetProducts(param);
                }
                else
                {
                    ViewBag.Productos = GetProducts(param);
                    ViewBag.Mensaje = param;
                }

            }catch (Exception)
            {

            }
            return View();
        }
        public ActionResult OK()
        {
            return View("OK");
        }
        public ActionResult header()
        {
            return View("header");
        }
        public ActionResult footer()
        {
            return View("footer");
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
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

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Products/Edit/5
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

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Products/Delete/5
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
        public object VerifyProduct(int id)
        {
            try
            {
                DataBase db = new DataBase();
                //string cook = Request.Cookies["Cookie"].Value;
                var art = db.Articulos.FirstOrDefault(e => e.idArticulo == id);
                //var user = db.sp_l_persona(cook).ToList();
                idCategoria = art.IdCategoria;
                return art;
            }
            catch (Exception )
            {
                return null;
            }
        }
        public object VerifyRecommendation(int id)
        {
            try
            {
                DataBase db = new DataBase();
                //Obtener 3 registros que sean de la misma categoria del seleccionado
                //var rec = db.Articulos.OrderByDescending(e => e.IdCategoria == id).Take(3);
                
                //Obtener 3 registros aleatorios de la tabla articulos
                var rec = db.Articulos.OrderByDescending(e => Guid.NewGuid()).Take(3);
                return rec;
            }catch (Exception )
            {
                return null;
            }
        }
        public object GetProducts(string param)
        {
            DataBase db = new DataBase();
            if (param != "" && param != null)
            {
                var consulta = from Articulos in db.Articulos where Articulos.Nombre.Contains(param) select Articulos;
                consulta.ToList();
                //var consulta = db.Articulos.Where(e => e.Nombre == param).ToList();
                return consulta;
            }
            else
            {
                var products = db.Articulos.Take(25).ToList();
                return products;
            }
        }
        public ActionResult AgregarCarrito(int? id, string cantidad)
        {
            if (id != null && cantidad != null && cantidad != "")
            {
                List<int> codigoProductos;
                List<int> Cantidad;
                

                if (Session["Productos"] == null)
                {
                    codigoProductos = new List<int>();
                    Cantidad = new List<int>();
                }
                else
                {
                    codigoProductos = Session["Productos"] as List<int>;
                    Cantidad = Session["Cantidad"] as List<int>;
                }
                codigoProductos.Add(id.Value);
                Cantidad.Add(Convert.ToInt32(cantidad));
                Session["Productos"] = codigoProductos;
                Session["Cantidad"] = Cantidad;
                ViewBag.Mensaje = "Productos en el carrito: " + codigoProductos.Count();
            }
            ViewBag.Carrito = Session["Productos"];
            if(cantidad != null)
            {

            }else
            {

            }

            DataBase db = new DataBase();

            List<Articulos> prod = db.Articulos.ToList();
            
            return RedirectToAction("Cesta", "User");
            //return View(prod);
        }
    }
}
