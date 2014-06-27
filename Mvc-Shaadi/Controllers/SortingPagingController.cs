using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcShaadi.Models;
using PagedList;

namespace MvcShaadi.Controllers
{ 
    public class SortingPagingController : Controller
    {
        private MvcShaadiContext db = new MvcShaadiContext();

        //
        // GET: /SortingPaging/

        public ViewResult Index(string sortOrder, string searchstring,string currentFilter,int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";

            if (searchstring != null)
            {
                page = 1;
            }
            else
            {
                searchstring = currentFilter;
            }

            ViewBag.CurrentFilter = searchstring;

            var students = from s in db.Persons
                           select s;
            //filter
            if (!string.IsNullOrEmpty(searchstring))
            {
                students = students.Where(s => s.FirstName.ToUpper().Contains(searchstring.ToUpper())
                    || s.LastName.ToUpper().Contains(searchstring.ToUpper()));
            }

            //sort
            switch (sortOrder)
            {
                case "Name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "Date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);


            return View(students.ToPagedList(pageNumber,pageSize));
        }

        //
        // GET: /SortingPaging/Details/5

        public ViewResult Details(int id)
        {
            Person person = db.Persons.Find(id);
            return View(person);
        }

        //
        // GET: /SortingPaging/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /SortingPaging/Create

        [HttpPost]
        public ActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                db.Persons.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(person);
        }
        
        //
        // GET: /SortingPaging/Edit/5
 
        public ActionResult Edit(int id)
        {
            Person person = db.Persons.Find(id);
            return View(person);
        }

        //
        // POST: /SortingPaging/Edit/5

        [HttpPost]
        public ActionResult Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        //
        // GET: /SortingPaging/Delete/5
 
        public ActionResult Delete(int id)
        {
            Person person = db.Persons.Find(id);
            return View(person);
        }

        //
        // POST: /SortingPaging/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Person person = db.Persons.Find(id);
            db.Persons.Remove(person);
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