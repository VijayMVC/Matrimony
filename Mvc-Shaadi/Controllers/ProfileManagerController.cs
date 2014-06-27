using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcShaadi.Models;
using System.IO;
using PagedList;
namespace MvcShaadi.Controllers
{ 
    public class ProfileManagerController : Controller
    {
        private MvcShaadiContext db = new MvcShaadiContext();

        //
        // GET: /ProfileManager/

        public ViewResult Index(int? page, int? StateId)
        {
            var profiles = from p in db.Profiles.Include(p => p.MaritalStatus).Include(p => p.State).OrderBy(p => p.ProfileId)
                           select p;

            //filter
             if (StateId != null)
             {
                 profiles = profiles.Where(p => p.StateId == StateId);
             }

            ViewBag.States = db.States.ToList();

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(profiles.ToPagedList(pageNumber,pageSize));
        }

        //
        // GET: /ProfileManager/Details/5

        public ViewResult Details(int id)
        {
            Profile profile = db.Profiles.Find(id);
            return View(profile);
        }

        //
        // GET: /ProfileManager/Create

        public ActionResult Create()
        {
            
            ViewBag.MaritalStatusId = new SelectList(db.MaritalStatuses, "MaritalStatusId", "Name");
            ViewBag.StateId = new SelectList(db.States, "StateId", "Name");
          
            return View();
        } 

        //
        // POST: /ProfileManager/Create

        [HttpPost]
        public ActionResult Create(Profile profile,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        if ((file.ContentType == "image/jpeg") || (file.ContentType == "image/gif") ||
(file.ContentType == "image/png"))//check allow jpg, gif, png
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/Content/profileImage/"), fileName);
                            file.SaveAs(path);//save image in folder     
                            profile.ImageURL = fileName;
                          
                        }
                    }
                }


                db.Profiles.Add(profile);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

          //  ViewBag.ReligionId = new SelectList(db.Religions, "ReligionId", "Name", profile.ReligionId);
            ViewBag.MaritalStatusId = new SelectList(db.MaritalStatuses, "MaritalStatusId", "Name", profile.MaritalStatusId);
            ViewBag.StateId = new SelectList(db.States, "StateId", "Name", profile.StateId);
           
            return View(profile);
        }
        
        //
        // GET: /ProfileManager/Edit/5
 
        public ActionResult Edit(int id)
        {
            Profile profile = db.Profiles.Find(id);
          //  ViewBag.ReligionId = new SelectList(db.Religions, "ReligionId", "Name", profile.ReligionId);
            ViewBag.MaritalStatusId = new SelectList(db.MaritalStatuses, "MaritalStatusId", "Name", profile.MaritalStatusId);
            ViewBag.StateId = new SelectList(db.States, "StateId", "Name", profile.StateId);
         
            return View(profile);
        }

        //
        // POST: /ProfileManager/Edit/5

        [HttpPost]
        public ActionResult Edit(Profile profile,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        if ((file.ContentType == "image/jpeg") || (file.ContentType == "image/gif") ||
(file.ContentType == "image/png"))//check allow jpg, gif, png
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/Content/profileImage/"), fileName);
                            file.SaveAs(path);//save image in folder     
                            profile.ImageURL = fileName;

                        }
                    }
                }
                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           // ViewBag.ReligionId = new SelectList(db.Religions, "ReligionId", "Name", profile.ReligionId);
            ViewBag.MaritalStatusId = new SelectList(db.MaritalStatuses, "MaritalStatusId", "Name", profile.MaritalStatusId);
            ViewBag.StateId = new SelectList(db.States, "StateId", "Name", profile.StateId);
          
            return View(profile);
        }

        //
        // GET: /ProfileManager/Delete/5
 
        public ActionResult Delete(int id)
        {
            Profile profile = db.Profiles.Find(id);
            return View(profile);
        }

        //
        // POST: /ProfileManager/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Profile profile = db.Profiles.Find(id);
            db.Profiles.Remove(profile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}