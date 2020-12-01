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
                List<int> lista = (List<int>)Session["Productos"];
                DataBase db = new DataBase();
                List<Articulos> prod = BuscarProducto(lista);
                ViewBag.Lista = prod;
                double total = 0;
                foreach(var p in prod)
                {
                    total += Convert.ToDouble(p.Precio);
                }
                ViewBag.Total = total;
                return View();
            }

            //return View();
        }
        public List<Articulos> BuscarProducto(List<int> id)
        {
            DataBase db = new DataBase();
            var consulta = from Articulos in db.Articulos where id.Contains(Articulos.idArticulo) select Articulos;
            return consulta.ToList();
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

        public object Verify(string correo)
        {
            try
            {
                DataBase db = new DataBase();
                string cook = Request.Cookies["Cookie"].Value;
                var user = db.Persona.FirstOrDefault(e => e.Correo == correo );
                //var user = db.sp_l_persona(cook).ToList();
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
