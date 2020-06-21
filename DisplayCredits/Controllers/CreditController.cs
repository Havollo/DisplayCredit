using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using DisplayCredits.Models;

namespace DisplayCredits.Controllers
{
    public class CreditController : Controller
    {
        [System.Web.Mvc.HttpGet]
        public ActionResult Index()
        {
            if (Session["personelId"] == null || Session["yetkiId"] == null)
            {
                return RedirectToActionPermanent("Index", "Login");
            }

            int perId = (int)Session["personelId"];
            int authId = (int)Session["yetkiId"];

            IEnumerable<JoinClass> jc = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44300/api/ListsCredit/");

            var consumeApi = hc.GetAsync($"getCredit?clntNo={perId}&authId={authId}");
            consumeApi.Wait();

            var readData = consumeApi.Result;
            if (readData.IsSuccessStatusCode)
            {
                var displayData = readData.Content.ReadAsAsync<IList<JoinClass>>();
                displayData.Wait();
                jc = displayData.Result;
            }
            return View(jc);
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Create()
        {
            CreditViewModel creditModel = new CreditViewModel();

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44300/api/ListsCredit/");
            var consumeApi = hc.GetAsync("getEntityList");
            consumeApi.Wait();

            var readData = consumeApi.Result;
            if (readData.IsSuccessStatusCode)
            {
                var displayData = readData.Content.ReadAsAsync<CreditViewModel>();
                displayData.Wait();
                creditModel = displayData.Result;
            }
            return View(creditModel);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Create(CreditViewModel insertCredit)
        {
            if (Session["personelId"] == null || Session["yetkiId"] == null)
            {
                return RedirectToActionPermanent("Index", "Login");
            }
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44300/api/ListsCredit/");

            // bekleyen
            insertCredit.Credit.status = "W";

            //insertCredit metodu çağrılıyor ve veritabanında create işlemi gerçekleştiriliyor.
            var insertRecord = hc.PostAsJsonAsync<credit>("ListsCredit", insertCredit.Credit);
            insertRecord.Wait();

            var saveData = insertRecord.Result;
            if (saveData.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(insertCredit);
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Edit(int id)
        {
            CreditViewModel creditModel = new CreditViewModel();

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44300/api/ListsCredit/");
            var consumeApi = hc.GetAsync($"getSelectedCredit?creditId={id}");
            consumeApi.Wait();

            var readData = consumeApi.Result;
            if (readData.IsSuccessStatusCode)
            {
                var displayData = readData.Content.ReadAsAsync<CreditViewModel>();
                displayData.Wait();
                creditModel = displayData.Result;
            }
            return View(creditModel);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(CreditViewModel creditModel)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44300/api/ListsCredit/");
            var insertRecord = hc.PutAsJsonAsync("ListsCredit", creditModel.Credit);
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
            return View(creditModel);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Delete(int id)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44300/api/ListsCredit");

            var delRecord = hc.DeleteAsync("ListsCredit/" + id.ToString());
            delRecord.Wait();

            var displayData = delRecord.Result;
            if (displayData.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Index");
        }
    }
}