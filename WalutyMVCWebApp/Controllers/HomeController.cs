using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using WalutyBusinessLogic.DatabaseLoading;
using WalutyBusinessLogic.LoadingFromFile;
using WalutyMVCWebApp.Models;
using X.PagedList;
using System.Net.Http;
using System.Threading.Tasks;

namespace WalutyMVCWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoader _loader;
        private readonly ICurrencyRepository _repository;
        private int _pageSize = 5;
        private const string ReportModuleUrl = "http://localhost:5006";

        public HomeController(ILoader loader, ICurrencyRepository repository)
        {
            _loader = loader;
            _repository = repository;
        }
        public async Task<IActionResult> Index(int? page, string searchString)
        {

            using (var client = new HttpClient())
            {
                var reports = await client.GetAsync($"{ReportModuleUrl}/api/reports");
                var message = await reports.Content.ReadAsStreamAsync();
            }

            var report = new ReportDto()
            {
                 = "Przemek"
            };
        

        int pageNumber = page ?? 1;
            IPagedList<CurrencyInfo> listOfResults = null;

            if (!String.IsNullOrWhiteSpace(searchString))
            {
                ViewBag.searchFilter = searchString;
            }

            if (ViewBag.searchFilter != null)
            {
                listOfResults = _repository.GetAllCurrencyInfo(_pageSize, pageNumber, ViewBag.searchFilter).Result;
                //listOfResults = _loader.LoadCurrencyInformation().Where(x => x.Code.Contains(ViewBag.searchFilter)).ToPagedList(pageNumber, _pageSize);
            }
            else
            {
                listOfResults = _repository.GetAllCurrencyInfo(_pageSize, pageNumber).Result;
                //listOfResults = _loader.LoadCurrencyInformation().ToPagedList(pageNumber, _pageSize);
            }

            ViewBag.SinglePageOfCurrencyInfo = listOfResults;

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
