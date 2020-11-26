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
        public ActionResult Cesta()
        {
            return View();
        }
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
