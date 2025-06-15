using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
        public ActionResult Create([Bind(Include = "requestid,status,requestdesc,requestfiles,users")] requests requests, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string fileName, filePath, fileExtension;

                if (upload != null)
                {
                    fileExtension = System.IO.Path.GetExtension(upload.FileName);
                    fileName = "attachement_" + db.requests.Count() + Path.GetExtension(upload.FileName);
                    filePath = "~/files/" + fileName;
                    upload.SaveAs(Server.MapPath(filePath));
                }
                else
                {
                    fileName = string.Empty;
                }
                requests.status = 2;
                requests.users = AccountManager.Id;
                requests.requestfiles = fileName;
                db.requests.Add(requests);
                db.SaveChanges();
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
        public ActionResult Edit(int id, int status)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(requests).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            var requestToUpdate = db.requests.Find(id);
            if (requestToUpdate == null)
            {
                return HttpNotFound(id.ToString());
            }

            // Обновляем только статус (или другие разрешенные поля)
            requestToUpdate.status = status;

            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.status = new SelectList(db.requeststatus, "statusid", "statusname", requestToUpdate);
            return View(requestToUpdate);
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
