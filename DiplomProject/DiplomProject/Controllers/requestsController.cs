using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiplomProject.Models;

namespace DiplomProject.Controllers
{
    public class requestsController : Controller
    {
        private DiplomEntities db = new DiplomEntities();

        // GET: requests
        public ActionResult Index()
        {
            var requests = db.requests.Include(r => r.requeststatus);
            return View(requests.ToList());
        }

        // GET: requests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            requests requests = db.requests.Find(id);
            if (requests == null)
            {
                return HttpNotFound();
            }
            return View(requests);
        }

        // GET: requests/Create
        public ActionResult Create(HttpPostedFileBase upload)
        {
            ViewBag.status = new SelectList(db.requeststatus, "statusid", "statusname");
            return View();
        }

        // POST: requests/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "requestid,status,requestdesc,requestfiles")] requests requests, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string filePath;
                if (upload != null)
                {
                    // получаем имя файла
                    string fileName = System.IO.Path.GetFileName(upload.FileName);
                    // сохраняем файл в папку Files в проекте
                    filePath = "~/files/" + fileName;
                    requests.requestfiles = filePath;
                    upload.SaveAs(Server.MapPath(filePath));
                }
                requests.requeststatus = db.requeststatus.Find(1);
                db.requests.Add(requests);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.status = new SelectList(db.requeststatus, "statusid", "statusname", requests.status);
            return View(requests);
        }

        // GET: requests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            requests requests = db.requests.Find(id);
            if (requests == null)
            {
                return HttpNotFound();
            }
            ViewBag.status = new SelectList(db.requeststatus, "statusid", "statusname", requests.status);
            return View(requests);
        }

        // POST: requests/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "requestid,status,requestdesc,requestfiles")] requests requests)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requests).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.status = new SelectList(db.requeststatus, "statusid", "statusname", requests.status);
            return View(requests);
        }

        // GET: requests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            requests requests = db.requests.Find(id);
            if (requests == null)
            {
                return HttpNotFound();
            }
            return View(requests);
        }

        // POST: requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            requests requests = db.requests.Find(id);
            db.requests.Remove(requests);
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
