using DiplomProject.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;


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

        

        //
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
                        Global.logged = true;
                        return this.RedirectToAction("Index", "events1");
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
            Global.logged = false;
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
