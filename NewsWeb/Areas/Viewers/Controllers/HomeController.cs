using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Operations;
using NewsWeb.DataAccess.Repository.IRepository;
using NewsWeb.Models;
using NewsWeb.Models.ViewModels;
using System.Diagnostics;

namespace NewsWeb.Areas.Viewers.Controllers
{
    [Area("Viewers")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        //public IActionResult Index(string searchString, string searchCriteria)
        //{
        //    ViewBag.SearchCriteria = new List<SelectListItem>
        //    {
        //        new SelectListItem {Text="HeadLine",Value="headLine"},
        //        new SelectListItem {Text="Description",Value ="description"}
        //    };
        //    var newsList = _unitOfWork.News.GetAll(includeProperties: "Place").Where(p => string.IsNullOrEmpty(searchString)
        //      || (searchCriteria == "headLine" && p.HeadLine.Contains(searchString, StringComparison.OrdinalIgnoreCase)) ||
        //      (searchCriteria == "description" && p.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase))).ToList();
        //    ViewBag.SearchString = searchString;
        //    ViewBag.SelectedSearchCriteria = searchCriteria;
        //    return View(newsList);
        //}
        //public IActionResult Index(string searchText)
        //{
        //    var productList = _unitOfWork.News.GetAll(includeProperties: "Place,Category");

        //    if (searchText != null)
        //    {
        //        productList = _unitOfWork.News.GetAll(includeProperties: "Place,Category").Where(p => p.HeadLine.Contains(searchText, StringComparison.OrdinalIgnoreCase)
        //        || p.Description.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();
        //    }
        //    else
        //        productList = _unitOfWork.News.GetAll(includeProperties: "Place,Category");
        //    return View(productList);
        //}
        public IActionResult Index(string searchText, int? categoryId, int? placeId)
        {
            // Retrieve all categories and places for dropdowns
            ViewBag.Categories = new SelectList(_unitOfWork.Category.GetAll(), "Id", "Name");
            ViewBag.Places = new SelectList(_unitOfWork.Place.GetAll(), "Id", "Name");

            var newsList = _unitOfWork.News.GetAll(includeProperties: "Place");

            // Filter by search text
            if (!string.IsNullOrEmpty(searchText))
            {
                newsList = newsList.Where(p => p.HeadLine.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                    || p.Description.Contains(searchText, StringComparison.OrdinalIgnoreCase));
            }

            // Filter by selected category
            if (categoryId != null)
            {
                newsList = newsList.Where(p => p.CategoryId == categoryId);
            }

            // Filter by selected place
            if (placeId != null)
            {
                newsList = newsList.Where(p => p.PlaceId == placeId);
            }

            return View(newsList.ToList());
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
        [HttpGet]
        public IActionResult Details(int id)
        {
            var newsIndb = _unitOfWork.News.FirstOrDefault(p => p.Id == id, includeProperties: "Place,Category");
            if (newsIndb == null) NotFound();
            return View(newsIndb);
        }
        //[HttpPost]
        //public IActionResult Details(News NewsList)
        //{

        //}
    }
}