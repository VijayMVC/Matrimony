using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcShaadi.Models;
using System.Web.Security;

namespace MvcShaadi.Controllers
{
    public class LogOnController : Controller
    {
        //
        // GET: /LogOn/

        MvcShaadiContext _db = new MvcShaadiContext();
        public ActionResult Index()
        {
            //ViewBag.Message = "Welcome to Login Page";
            //var v = ViewData.Model = _db.Employee.ToList();
            // return View(v);
            return View();
        }
        [HttpPost]
        public ActionResult Index(Profile model, string returnUrl)
        {
            // Lets first check if the Model is valid or not
            if (ModelState.IsValid)
            {
               
                    string email = model.Email;
                    string password = model.Password;

                    // Now if our password was enctypted or hashed we would have done the
                    // same operation on the user entered password here, But for now
                    // since the password is in plain text lets just authenticate directly

                    bool userValid =_db.Profiles.Any(p => p.Email== email && p.Password == password);

                    // User found in the database
                    if (userValid)
                    {

                        FormsAuthentication.SetAuthCookie(email, false);
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("index", "profilemanager");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    }
                }
            

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public ActionResult Create()
        {
            ViewBag.MaritalStatusId = new SelectList(_db.MaritalStatuses, "MaritalStatusId", "Name");
            ViewBag.StateId = new SelectList(_db.States, "StateId", "Name");

            return View();
        }

        //
        // POST: /Home/Create
        [HttpPost]
        public ActionResult Create(Profile profile)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {

                    //Save Registration
                    _db.Profiles.Add(profile);
                    _db.SaveChanges();

                    return RedirectToAction("Index");
                }

                //return RedirectToAction("Index");

                //ViewBag.Route = _db.Cab.OrderBy(g => g.JourneyTime).ToList();
                // ViewBag.DropPoint = _db.Employee.OrderBy(a => a.EmployeeName).ToList();
                return View(profile);
            }
            catch
            {
                return View();
            }
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "LogOn");
        }

    }
}
