using IronXL;
using Microsoft.AspNetCore.Mvc;
using RouteJsonDataCreate.Models;
using System.Diagnostics;
using System.Xml;

namespace RouteJsonDataCreate.Controllers
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult CreateRouteData()
        {
            ViewBag.Status = "";
            RouteJsonData _routeData = new RouteJsonData();
            return View(_routeData);
        }

        [HttpPost]
        public IActionResult CreateRouteData(RouteJsonData _routeData)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Status = false;
                return View("CreateRouteData");
            }
            
            ReadExcelFile(_routeData);
            ViewBag.Status = true;           
            return View("CreateRouteData", _routeData);
        }

        private void ReadExcelFile(RouteJsonData _routeData)
        {
            string excelFilePath = "D:\\Test-Projects\\RouteJsonDataFiles\\RoutesExcelFiles\\" + _routeData.FileName + ".xlsx";
            RouteJsonModel routeJsonModel = new RouteJsonModel();
            routeJsonModel.Settings = new Settings();
            routeJsonModel.Model = new Model();
            routeJsonModel.Model.Facilities = new List<Facility>();
            routeJsonModel.Model.Routes = new List<Routes>();
            routeJsonModel.Settings.RoadsFilePath = _routeData.RoadsFilePath;
            List<Jobs> _jobs = new List<Jobs>();
            RouteLatLongData _routeLatLongData = new RouteLatLongData();
            _routeLatLongData.Route = _routeData.RouteName;
            _routeLatLongData.Jobs = new List<JobLocation>();

            _routeLatLongData.Jobs.Add(new JobLocation()
            {
                lat = Convert.ToDecimal(_routeData.YardLat),
                lng = Convert.ToDecimal(_routeData.YardLng)
            });

            WorkBook workBook = WorkBook.Load(excelFilePath);
            WorkSheet workSheet = workBook.DefaultWorkSheet;
            
            foreach (var row in workSheet.Rows)
            {
                if (row.RowNumber != 0)
                {
                    _jobs.Add(new Jobs()
                    {
                        UniqueID = row.Columns[0].Value.ToString(),
                        Latitude = Convert.ToDecimal(row.Columns[1].Value),
                        Longitude = Convert.ToDecimal(row.Columns[2].Value),
                        StreetName = row.Columns[3].Value.ToString().Trim(),
                    });

                    _routeLatLongData.Jobs.Add(new JobLocation()
                    {
                        lat = Convert.ToDecimal(row.Columns[1].Value),
                        lng = Convert.ToDecimal(row.Columns[2].Value)
                    });
                }                
            }

            List<Routes> _routeLst = new List<Routes>();
            Routes _route = new Routes();
            _route.RouteID = 205;
            _route.Jobs = _jobs;
            _routeLst.Add(_route);
            routeJsonModel.Model.Routes = _routeLst;

            List<Facility> _facilityLst = new List<Facility>();
            Facility _facility = new Facility();
            _facility.Type = "Dump";
            _facility.Name = "Acme Dump";
            _facility.Latitude = Convert.ToDecimal(workSheet.Rows[workSheet.Rows.Length - 1].Columns[1].Value);
            _facility.Longitude = Convert.ToDecimal(workSheet.Rows[workSheet.Rows.Length - 1].Columns[2].Value);
            _facility.Commodities = ["Waste"];
            _facilityLst.Add(_facility);

            _facility = new Facility();
            _facility.Type = "Depot";
            _facility.Name = "Yard";
            _facility.Latitude = _routeData.YardLat;
            _facility.Longitude = _routeData.YardLng;
            _facility.Commodities = [];
            _facilityLst.Add(_facility);

            routeJsonModel.Model.Facilities = _facilityLst;
            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(routeJsonModel);
            string outputFileName = "D:\\Test-Projects\\RouteJsonDataFiles\\RouteData\\" + _routeData.FileName + ".json";
            System.IO.File.WriteAllText(outputFileName, jsonString);

            jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(_routeLatLongData);
            outputFileName = "D:\\Test-Projects\\RouteJsonDataFiles\\LatLong\\" + _routeData.FileName + ".json";
            System.IO.File.WriteAllText(outputFileName, jsonString);
        }
    }
}
