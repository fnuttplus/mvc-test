using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class YoutubeController : Controller
    {
        //GET: Youtube/NewTubes
        public async Task<ActionResult> NewTubes()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("OAuth", Request.Cookies["googleAccess"].Value);
            var json = await client.GetStringAsync("https://www.googleapis.com/youtube/v3/activities?part=snippet&home=true&maxResults=50&key="
                + System.Configuration.ConfigurationManager.AppSettings["googleKey"]);
            activityListResponse response = JsonConvert.DeserializeObject<activityListResponse>(json);
//            ViewBag.streamList = response.items;
            return View(response.items);
        }

        // GET: Youtube
        public ActionResult Index()
        {
            return View();
        }

        // GET: Youtube/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Youtube/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Youtube/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Youtube/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Youtube/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Youtube/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Youtube/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
