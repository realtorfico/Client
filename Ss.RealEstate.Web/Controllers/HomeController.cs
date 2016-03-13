using System.Web.Mvc;
using Ss.RealEstate.Library2;
using Ss.RealEstate.Model;
using System.Collections.Generic;
using System.Linq;
using Ss.RealEstate.Web.Models;
using System.Configuration;

namespace Ss.RealEstate.Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var combinedList = new List<IOrderedEnumerable<PropertyInfo>>();
            ViewBag.Message = "";
            ViewBag.Collection = combinedList;

            var propVM = new PropertyFormRequestViewModel();
            propVM.City = string.Empty;

            return View(propVM); 
        }

        [HttpPost]
        public ActionResult Index(PropertyFormRequestViewModel homeVM)
        {
            if (!ModelState.IsValid) return View(); 

            var basicScorer = new BasicScorer();
            bool isPaginated = false;
            bool usePropertyIdForUrl = (ConfigurationManager.AppSettings["UseJsonForGettingAddresses"].ToLower() == "true");

            var addressList = (usePropertyIdForUrl) ?
                CrawlerForProperty.GetAddressList(homeVM.City, homeVM.MinPrice, homeVM.MaxPrice, homeVM.MinYear, homeVM.MaxYear, out isPaginated) :
                CrawlerForProperty.GetAddresses(homeVM.City, homeVM.MinPrice, homeVM.MaxPrice, homeVM.MinYear, homeVM.MaxYear, out isPaginated); 

            var propInfoList = new List<PropertyInfo>();
            var propInfoZeroScoreList = new List<PropertyInfo>();
            var combinedList = new List<IOrderedEnumerable<PropertyInfo>>(); 

            foreach (var address in addressList)
            {
                var propInfo = basicScorer.GetBasicScore(address, usePropertyIdForUrl); 
                if (propInfo.DesirabilityScore > 0) propInfoList.Add(propInfo); else propInfoZeroScoreList.Add(propInfo); 
            }
            combinedList.Add(propInfoList.OrderByDescending(a => a.DesirabilityScore));
            combinedList.Add(propInfoZeroScoreList.OrderByDescending(a => a.DesirabilityScore)); 

            ViewBag.Message = "";
            ViewBag.Collection = combinedList;
            ViewData["ZipPps"] = basicScorer.GetZipPps();
            ViewData["isPaginated"] = isPaginated;

            return View("Index");
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;

            // Redirect on error:
            filterContext.Result = RedirectToAction("Index", "Error");

            // OR set the result without redirection:
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Error/Index.cshtml"
            };
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}