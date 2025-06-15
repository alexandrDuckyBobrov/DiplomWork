using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DiplomProject.Models
{
    public class contactsController : Controller
    {
        private DiplomEntities db = new DiplomEntities();

        // GET: contacts/Details/5
        public ActionResult Details(int? user)
        {
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var id= (from p in db.users join q in db.contacts on p.userid equals q.users where p.userid == user select q.id).Single();
            contacts contacts = db.contacts.Find(id);
            while (contacts == null)
            {
                contacts contact = new contacts
                {
                    users = user.Value,
                    phone = ""
                };
                db.contacts.Add(contact);
                db.SaveChanges();
                id = (from p in db.users join q in db.contacts on p.userid equals q.users where p.userid == user select q.id).Single();
                contacts = db.contacts.Find(id);
            }
            return View(contacts);
        }

        // GET: contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contacts contacts = db.contacts.Find(id);
            if (contacts == null)
            {
                return HttpNotFound();
            }
            ViewBag.users = new SelectList(db.users, "userid", "login", contacts.users);
            return View(contacts);
        }

        // POST: contacts/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,users,phone,email")] contacts contacts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contacts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.users = new SelectList(db.users, "userid", "login", contacts.users);
            return View(contacts);
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
