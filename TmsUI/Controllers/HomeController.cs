using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TmsUI.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace TmsUI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public async Task<ActionResult> Index()
        {
            string Baseurl = "http://localhost:5000";
            List<Load> LoadInfo = new List<Load>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Loads/");

                if (Res.IsSuccessStatusCode)
                {
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;

                    LoadInfo = JsonConvert.DeserializeObject<List<Load>>(PrResponse);
                }
            }

            return View(LoadInfo);
        }

        //GET: Load/Edit
        public async Task<ActionResult> Edit(int id)
        {
            string Baseurl = "http://localhost:5000";
            Load load1 = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                HttpResponseMessage Res = await client.GetAsync("api/Loads/" + id);

                if (Res.IsSuccessStatusCode)
                {
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;

                    load1 = JsonConvert.DeserializeObject<Load>(PrResponse);

                }
                else
                    ModelState.AddModelError(string.Empty, "Server Error.Please contact administrator");
            }
            return View(load1);
        }

        //POSt: Loads/Edit/
        [HttpPost]
        public async Task<ActionResult>Edit(int id,Load load)
        {
            try
            {
                string Baseurl = "http://localhost:5000";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    HttpResponseMessage Res = await client.GetAsync("api/Loads/" + id);
                    Load load1 = null;

                    if (Res.IsSuccessStatusCode)
                    {
                        var PrResponse = Res.Content.ReadAsStringAsync().Result;

                        load1 = JsonConvert.DeserializeObject<Load>(PrResponse);
                    }
                    load.LoadNumber = load1.LoadNumber;
                    //HTTP POST
                    var postTask = client.PutAsJsonAsync<Load>("api/Loads/" + load.Id, load);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }

                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }
    }
}