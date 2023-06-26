using Microsoft.AspNetCore.Mvc;
using NewshoreTest.Web.Models;
using System.Diagnostics;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http.Json;
using System.Net;


namespace NewshoreTest.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        async public Task<ActionResult> Browse(IFormCollection collection)
        {
            var _baseUrl = "http://localhost:5210";

            RequestViewModel FlightOBject = new RequestViewModel
            {
                Destino = collection["Destino"].ToString(),
                Moneda = collection["Moneda"].ToString(),
                Origen = collection["Origen"].ToString()
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                //HttpContent _Content = new JsonContent(_Request, );
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.PostAsJsonAsync("api/GetTravel", FlightOBject);
                if (Res.IsSuccessStatusCode)
                {
                    var UserResponse = Res.Content.ReadAsStringAsync().Result;
                    var _Result = JsonConvert.DeserializeObject<GetTravelResponse>(UserResponse);

                    if (_Result.Status != "OK")
                        throw new Exception("Error retreiving flights");
                }

            }

            return RedirectToAction(nameof(Index));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}