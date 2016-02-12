using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ModelManagerADFS.Models;

namespace ModelManagerADFS.Controllers
{
    [Authorize]
    public class ModelEditorController : Controller
    {
        private BikeStoreContext db = new BikeStoreContext();

        // GET: ModelEditor
        public ActionResult Index()
        {
            var models = db.Models.Include(m => m.Category).Include(m => m.Manufacturer).Include(m => m.Status);
            return View(models.ToList());
        }

        // GET: ModelEditor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Model model = db.Models.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: ModelEditor/Create
        [Authorize(Roles = "ModelEditorRole")]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "ManufacturerId", "Name");
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Name");
            return View();
        }

        // POST: ModelEditor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ModelEditorRole")]
        public ActionResult Create([Bind(Include = "ModelId,Name,ManufacturerCode,CategoryId,Description,Features,StatusId,ManufacturerId,ListPrice,ImageCollection,CategoryCustomData,ManufacturerCustomData,DateModified,DateCreated")] Model model)
        {
            if (ModelState.IsValid)
            {
                db.Models.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", model.CategoryId);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "ManufacturerId", "Name", model.ManufacturerId);
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Name", model.StatusId);
            return View(model);
        }

        // GET: ModelEditor/Edit/5
        [Authorize(Roles = "ModelEditorRole")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Model model = db.Models.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", model.CategoryId);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "ManufacturerId", "Name", model.ManufacturerId);
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Name", model.StatusId);
            return View(model);
        }

        // POST: ModelEditor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ModelEditorRole")]
        public ActionResult Edit([Bind(Include = "ModelId,Name,ManufacturerCode,CategoryId,Description,Features,StatusId,ManufacturerId,ListPrice,ImageCollection,CategoryCustomData,ManufacturerCustomData,DateModified,DateCreated")] Model model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", model.CategoryId);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "ManufacturerId", "Name", model.ManufacturerId);
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Name", model.StatusId);
            return View(model);
        }

        // GET: ModelEditor/Delete/5
        [Authorize(Roles = "ModelEditorRole")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Model model = db.Models.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: ModelEditor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ModelEditorRole")]
        public ActionResult DeleteConfirmed(int id)
        {
            Model model = db.Models.Find(id);
            db.Models.Remove(model);
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
