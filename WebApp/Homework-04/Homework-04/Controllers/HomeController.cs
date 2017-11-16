using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homework_04.Controllers
{
    public class HomeController : Controller
    {
        private IAuthenticationManager AuthenticationManager { get { return HttpContext.GetOwinContext().Authentication; } }
        public ActionResult Index()
        {
            if(Session["accessToken"] != null)
            {
                return RedirectToAction("Admin", "Dashboard");
            }
            if (Request.IsAuthenticated)
            {
                
            }
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            Session.Clear();
            Session.Abandon();
           return RedirectToAction("Index", "Home");
        }
    }
}
