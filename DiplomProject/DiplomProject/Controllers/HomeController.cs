using DiplomProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DiplomProject.Controllers
{
    public class HomeController : Controller
    {
        private DiplomEntities db = new DiplomEntities();
        public ActionResult Index()
        {
            var requests = db.catalogue;
            return View(requests.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult IndexAdmin()
        {
            return View();
        }
        public ActionResult IndexUser()
        {
            var querry = from p in db.requests where p.users.Equals(AccountManager.Id) select p;


            return View(querry.ToList());
        }
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
        public ActionResult DetailsManager(int? id)
        {
            var requestViewModel = (from p in db.requests
                                    join q in db.contacts on p.users equals q.users
                                    where p.requestid == id // Простое сравнение
                                    select new RequestsViewModel
                                    {
                                        requestid = p.requestid,
                                        status = p.status,
                                        requeststatus = p.requeststatus,
                                        requestdesc = p.requestdesc,
                                        requestfiles = p.requestfiles,
                                        userlogin = p.users1.login,
                                        phone = q.phone,
                                        email = q.email
                                    }).FirstOrDefault();

            if (requestViewModel == null)
            {
                return HttpNotFound();
            }

            return View(requestViewModel);
        }
        public ActionResult EditManager(int? id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditManager(int id, int status)
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
        public ActionResult IndexManager()
        {

            var query = from p in db.requests
                        join q in db.contacts on p.users equals q.users
                        select new RequestsViewModel
                        {
                            requestid = p.requestid,
                            status = p.status,
                            requeststatus = p.requeststatus,
                            requestdesc = p.requestdesc,
                            requestfiles = p.requestfiles,
                            userlogin = p.users1.login,
                            phone = q.phone,
                            email = q.email
                        };

            return View(query.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}