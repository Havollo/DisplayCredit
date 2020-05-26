using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DisplayCredits.Models;

namespace DisplayCredits.Controllers
{
    public class LoginController : Controller
    {
        loansEntities entity = new loansEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string kullaniciMail, string parola)
        {
            var personel = (from p in entity.clients where p.emailAddress == kullaniciMail && p.password == parola select p).FirstOrDefault();

            if (personel != null)
            {
                Session["personelId"] = personel.client_no;
                Session["yetkiId"] = personel.authoriyId;
                Session["kullaniciAdi"] = personel.name + " " + personel.surname;


                //1 ise kullanıcı
                if (personel.authoriyId == 1 || personel.authoriyId == 2)
                {
                    return RedirectToAction("Index", "Credit");
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["personelId"] = null;
            Session["yetkiId"] = null;

            return RedirectToActionPermanent("Index");
        }
    }
}