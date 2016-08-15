using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using instinct360.Models;

namespace instinct360.Controllers
{
    //[Authorize]
    public class ReviewTemplatesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ReviewTemplates
        public ActionResult Index()
        {
            return View(db.ReviewTemplates.ToList());
        }

        // GET: ReviewTemplates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReviewTemplate reviewTemplate = db.ReviewTemplates.Find(id);
            if (reviewTemplate == null)
            {
                return HttpNotFound();
            }
            return View(reviewTemplate);
        }

        // GET: ReviewTemplates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReviewTemplates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,ReviewTemplateName")] ReviewTemplate reviewTemplate)
        {
            if (ModelState.IsValid)
            {
                db.ReviewTemplates.Add(reviewTemplate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reviewTemplate);
        }

        // GET: ReviewTemplates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReviewTemplate reviewTemplate = db.ReviewTemplates.Find(id);
            if (reviewTemplate == null)
            {
                return HttpNotFound();
            }
            return View(reviewTemplate);
        }

        // POST: ReviewTemplates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ReviewTemplateName")] ReviewTemplate reviewTemplate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reviewTemplate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reviewTemplate);
        }

        // GET: ReviewTemplates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReviewTemplate reviewTemplate = db.ReviewTemplates.Find(id);
            if (reviewTemplate == null)
            {
                return HttpNotFound();
            }
            return View(reviewTemplate);
        }

        // POST: ReviewTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReviewTemplate reviewTemplate = db.ReviewTemplates.Find(id);
            db.ReviewTemplates.Remove(reviewTemplate);
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
