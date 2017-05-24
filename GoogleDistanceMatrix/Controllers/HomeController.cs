using System.Web.Mvc;
using System.Net.Http;
using GoogleDistanceMatrix.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GoogleDistanceMatrix.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private static string requestApiKey = "AIzaSyAVTS-hRRBnBm46Ee0r0lo70uTiZqVt5PI";
        private static string baseUrl = "https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial";        

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Welcome";
            var locationModel = new LocationModel();
            locationModel.History = (List<LocationModel>)Session["history"];
            return View(locationModel);
        }
        
        [HttpPost]
        public ActionResult Index(LocationModel locationModel)
        {
            var response = "";
            using (var client = new HttpClient())
            {                               
                response = client.GetStringAsync($"{baseUrl}&origins={locationModel.Origin}&destinations={locationModel.Destination}&mode={locationModel.Mode}&key={requestApiKey}").Result;
            }
            JSONModel result = JsonConvert.DeserializeObject<JSONModel>(response);
            if (result.rows[0].elements[0].status != "OK")
            {
                //die
                ModelState.AddModelError("", "Error- Google Maps didnt like something");
                return View(locationModel);
            }
            locationModel.Distance = result.rows[0].elements[0].distance.text.ToString();
            locationModel.Duration= result.rows[0].elements[0].duration.text.ToString();
            StoreHistory(locationModel);
            return View(locationModel);
        }

        public void StoreHistory(LocationModel locationModel)
        {
            List<LocationModel> history = new List<LocationModel>();
            if (Session["history"] != null)
            {
                history = (List<LocationModel>)Session["history"];
            }

            LocationModel historyItem = new LocationModel()
            {
                Origin = locationModel.Origin,
                Destination = locationModel.Destination,
                Mode = locationModel.Mode,
                Duration = locationModel.Duration,
                Distance = locationModel.Distance
            };

            history.Add(historyItem);            
            Session["history"] = history;
        }
    }
}
