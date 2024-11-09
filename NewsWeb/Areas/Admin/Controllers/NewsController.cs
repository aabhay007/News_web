using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsWeb.DataAccess.Repository.IRepository;
using NewsWeb.Models;
using NewsWeb.Models.ViewModels;
using NewsWeb.Utility;
using System.Data;

namespace NewsWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class NewsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public NewsController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        #region
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.News.GetAll(includeProperties:"Category,Place") });
        }
        public IActionResult Delete(int id)
        {
            var newsInDb = _unitOfWork.News.Get(id);
            if (newsInDb == null)
                return Json(new { success = false, message = "Something Went Wrong While Deleting Data!" });
            _unitOfWork.News.Remove(newsInDb);
            _unitOfWork.save();
            return Json(new { success = true, message = "SuccessFully Deleted" });
        }
        #endregion
        public IActionResult Upsert(int? id)
        {
            NewsVM newsVM = new NewsVM()
            {
                News = new News(),
                PlaceList = _unitOfWork.Place.GetAll().Select(pl => new SelectListItem()
                {
                    Text = pl.Name,
                    Value = pl.Id.ToString()
                }
                ),
                CategoryList = _unitOfWork.Category.GetAll().Select(ct => new SelectListItem()
                {
                    Text = ct.Name,
                    Value = ct.Id.ToString()
                })
            };
            if (id == null) return View(newsVM);
            newsVM.News = _unitOfWork.News.Get(id.GetValueOrDefault());
            if (newsVM.News == null) return NotFound();
            return View(newsVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(NewsVM newsVM)
        {
            if (ModelState.IsValid)
            {
                var webRootPath = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count() > 0)
                {
                    var fileName = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(files[0].FileName);
                    var uploads = Path.Combine(webRootPath, @"images\news");
                    if (newsVM.News.Id != 0)
                    {
                        var imageExists = _unitOfWork.News.Get(newsVM.News.Id).ImageUrl;
                        newsVM.News.ImageUrl = imageExists;
                    }
                    if (newsVM.News.ImageUrl != null)
                    {
                        var imagePath = Path.Combine(webRootPath, newsVM.News.ImageUrl.Trim('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    newsVM.News.ImageUrl = @"\images\news\" + fileName + extension;
                }
                else
                {
                    if (newsVM.News.Id != 0)
                    {
                        var imageExists = _unitOfWork.News.Get(newsVM.News.Id).ImageUrl;
                        newsVM.News.ImageUrl = imageExists;
                    }
                }
                if (newsVM.News.Id == 0)
                    _unitOfWork.News.Add(newsVM.News);//create
                else
                    _unitOfWork.News.Update(newsVM.News);//edit
                _unitOfWork.save();
                return RedirectToAction("Index");
            }
            else
            {
                newsVM = new NewsVM()
                {
                    News = new News(),
                    PlaceList = _unitOfWork.Place.GetAll().Select(pl => new SelectListItem()
                    {
                        Text = pl.Name,
                        Value = pl.Id.ToString()
                    }
                    ),
                    CategoryList = _unitOfWork.Category.GetAll().Select(ct => new SelectListItem()
                    {
                        Text = ct.Name,
                        Value = ct.Id.ToString()
                    })
                };
                if (newsVM.News.Id == 0)
                {
                    newsVM.News = _unitOfWork.News.Get(newsVM.News.Id);
                }
                return View(newsVM);
            }
        }
    }
}
