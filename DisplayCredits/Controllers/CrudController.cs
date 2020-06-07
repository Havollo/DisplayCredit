using DisplayCredits.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace DisplayCredits.Controllers
{
    public class CrudController : Controller
    {
        // GET: Crud
        public ActionResult Index()
        {
            if (Session["personelId"] == null || Session["yetkiId"] == null)
            {
                return RedirectToActionPermanent("Index", "Login");
            }

            IEnumerable<JoinClass> crdObj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44300/api/CreditCrud/");

            var consumeApi = hc.GetAsync("CreditCrud");
            consumeApi.Wait();

            var readData = consumeApi.Result;
            if (readData.IsSuccessStatusCode)
            {
                var displayData = readData.Content.ReadAsAsync<IList<JoinClass>>();
                displayData.Wait();
                crdObj = displayData.Result;
            }
            return View(crdObj);
        }

        public ActionResult Create()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Create(credit insertCredit)
        {
            if (Session["personelId"] == null || Session["yetkiId"] == null)
            {
                return RedirectToActionPermanent("Index", "Login");
            }
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44300/api/CreditCrud/");

            var insertRecord = hc.PostAsJsonAsync<credit>("CreditCrud", insertCredit);
            insertRecord.Wait();

            var saveData = insertRecord.Result;
            if (saveData.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Create");
        }

        //1.si get edit metod
        public ActionResult Edit(int id)
        {
            if (Session["personelId"] == null || Session["yetkiId"] == null)
            {
                return RedirectToActionPermanent("Index", "Login");
            }
            IEnumerable<JoinClass> crdObj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44300/api/");
            var consumeApi = hc.GetAsync("CreditCrud?id=" + id.ToString());
            consumeApi.Wait();

            var readData = consumeApi.Result;
            if (readData.IsSuccessStatusCode)
            {
                var displayData = readData.Content.ReadAsAsync<IList<JoinClass>>();
                displayData.Wait();
                crdObj = displayData.Result;
            }
            return View(crdObj);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(credit ct)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44300/api/CreditCrud/");
            var insertRecord = hc.PutAsJsonAsync<credit>("CreditCrud", ct);
            insertRecord.Wait();

            var saveData = insertRecord.Result;
            if (saveData.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message = "Kredi güncellenemedi";
            }

            return View(ct);
        }

        public ActionResult Delete(int id)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44300/api/CreditCrud");

            var delRecord = hc.DeleteAsync("CreditCrud/" + id.ToString());
            delRecord.Wait();

            var displayData = delRecord.Result;
            if (displayData.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        ////public ActionResult Details(int id)
        ////{
        ////    if (Session["personelId"] == null || Session["yetkiId"] == null)
        ////    {
        ////        return RedirectToActionPermanent("Index", "Login");
        ////    }

        ////    diary crdObj = null;
        ////    HttpClient hc = new HttpClient();
        ////    hc.BaseAddress = new Uri("https://localhost:44300/api/");
        ////    var consumeApi = hc.GetAsync("CreditCrud?id=" + id.ToString());
        ////    consumeApi.Wait();

        ////    var readData = consumeApi.Result;
        ////    if (readData.IsSuccessStatusCode)
        ////    {
        ////        var displayData = readData.Content.ReadAsAsync<diary>();
        ////        displayData.Wait();
        ////        crdObj = displayData.Result;
        ////    }
        ////    return View(crdObj);
        ////}

    }
}