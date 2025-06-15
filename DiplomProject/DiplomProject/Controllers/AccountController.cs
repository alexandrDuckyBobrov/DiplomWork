using DiplomProject.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Services.Description;


namespace DiplomProject
{
    public class AccountController : Controller
    {
        DiplomEntities DBTest = new DiplomEntities();
        // GET: Account
        [AllowAnonymous]
        public ActionResult Register(string returnUrl)
        {
            try
            {
                if (this.Request.IsAuthenticated)
                {
                    return this.RedirectToLocal(returnUrl);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return this.View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DBTest.AddUser(model.Login, model.Password);
                    int id = (from p in DBTest.users
                              where p.login.Equals(model.Login)
                              select p.userid).Single();
                    contacts contact = new contacts
                    {
                        users = id,
                        phone = model.Phone,
                        email = model.EMail
                    };
                    DBTest.contacts.Add(contact);
                    DBTest.SaveChanges();


                    return this.RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return this.View(model);

        }
        public ActionResult Login(string returnUrl)
        {
            try
            {
                if (this.Request.IsAuthenticated)
                {
                    return this.RedirectToLocal(returnUrl);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return this.View();
        }

        public int? status;
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var loginInfo = this.DBTest.GetLogin(model.Login, model.Password).ToList();
                    if (loginInfo != null && loginInfo.Count() > 0)
                    {
                        int id = (from p in DBTest.users
                                  where p.login.Equals(model.Login)
                                  select p.userid).Single();
                        int? role;
                        try
                        {
                            role = (from q in DBTest.users join p in DBTest.roleusers on q.userid equals p.users where q.login.Equals(model.Login) select p.roles).Single();
                        }
                        catch
                        {
                            // Если вызвано исключение - роль равна null
                            role = null;
                        }
                        string login = model.Login;

                        AccountManager.Login(id, login, role);

                        string page = "IndexUser";
                        try
                        {
                            switch (role)
                            {
                                case 1: page = "IndexAdmin"; break;
                                case 2: page = "IndexManager"; break;
                            }

                        }
                        catch
                        {
                            page = "IndexUser";
                            ViewBag.Message = "You have no role";
                        }

                        return this.RedirectToAction(page, "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid username or password");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return this.View(model);

        }
        public ActionResult LogOff()
        {
            AccountManager.Logout();
            return this.RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            try
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index", "Home");
        }
    }
}
