using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using todolist.Models;

namespace todolist.Controllers
{
    public class ToDOesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ToDOes
        public ActionResult Index()
        {
            string currentUserId = User.Identity.GetUserId();

            ApplicationUser currentUser = db.Users.FirstOrDefault
                (x => x.Id == currentUserId);

            return View(db.ToDos.ToList().Where(x=> x.User == currentUser));
        }




        public ActionResult BuildToDoTable()
        {
            string currentUserId = User.Identity.GetUserId();

            ApplicationUser currentUser = db.Users.FirstOrDefault
                (x => x.Id == currentUserId);

            return PartialView("_ToDoTablw", 
                db.ToDos.ToList().Where(x => x.User == currentUser));

        }

        // GET: ToDOes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDO toDO = db.ToDos.Find(id);
            if (toDO == null)
            {
                return HttpNotFound();
            }
            return View(toDO);
        }

        // GET: ToDOes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDOes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,IsDone")] ToDO toDO)
        {
            if (ModelState.IsValid)
            {

                string currentUserId = User.Identity.GetUserId();
                
                ApplicationUser currentUser = db.Users.FirstOrDefault(
                    x=>x.Id==currentUserId );
                toDO.User = currentUser;

                db.ToDos.Add(toDO);

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(toDO);
        }

        // GET: ToDOes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDO toDO = db.ToDos.Find(id);
            if (toDO == null)
            {
                return HttpNotFound();
            }
            return View(toDO);
        }

        // POST: ToDOes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,IsDone")] ToDO toDO)
        {
            if (ModelState.IsValid)
            {


                db.Entry(toDO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toDO);
        }

        // GET: ToDOes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDO toDO = db.ToDos.Find(id);
            if (toDO == null)
            {
                return HttpNotFound();
            }
            return View(toDO);
        }

        // POST: ToDOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ToDO toDO = db.ToDos.Find(id);
            db.ToDos.Remove(toDO);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
