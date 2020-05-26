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
        // GET: Credit
        public ActionResult Index()
        {
            if (Session["personelId"] == null || Session["yetkiId"] == null)
            {
                return RedirectToActionPermanent("Index", "Login");
            }


            int perId = (int)Session["personelId"];
            int authId = (int)Session["yetkiId"];

            //var listCont = new ListsCreditController();
            //var result = listCont.getCredit(Convert.ToInt32(perId));
            //return View();
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

            //IEnumerable<Credit> credObj = null;
            //HttpClient hc = new HttpClient();
            //hc.BaseAddress = new Uri("https://localhost:44300/api/");

            //var apiCredController = hc.GetAsync("ListsCredit");
            //apiCredController.Wait();

            //var resultDisplay = apiCredController.Result;
            //if (resultDisplay.IsSuccessStatusCode)
            //{
            //    var readCredTable = resultDisplay.Content.ReadAsAsync<IList<Credit>>();
            //    readCredTable.Wait();
            //    credObj = readCredTable.Result;
            //}
            //else
            //{
            //    credObj = Enumerable.Empty<Credit>();
            //    ModelState.AddModelError(string.Empty, "No Records Found.. ");
            //}
            //return View(credObj);
        }
    }
}