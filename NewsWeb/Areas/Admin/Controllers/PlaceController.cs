using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsWeb.DataAccess.Repository.IRepository;
using NewsWeb.Models;
using NewsWeb.Utility;
using System.Data;

namespace NewsWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class PlaceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PlaceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new {data=_unitOfWork.Place.GetAll()});
        }
        [HttpDelete] 
        public IActionResult Delete(int id)
        {
            var placeInDb = _unitOfWork.Place.Get(id);
            if (placeInDb == null)
                return Json(new { success = false, message = "Something Went Wrong" });
            _unitOfWork.Place.Remove(placeInDb);
            _unitOfWork.save();
            return Json(new { success = true, message = "Data deleted successfully" });
        }
        #endregion
        public IActionResult Upsert(int? id)
        {
            Place place = new Place();
            if (id == null) return View(place);//create
            //edit
            place = _unitOfWork.Place.Get(id.GetValueOrDefault());
            if (place == null) return NotFound();
            return View(place);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Place place)
        {
            if (place == null) return NotFound();
            if (!ModelState.IsValid) return View(place);
            if (place.Id == 0)
                _unitOfWork.Place.Add(place);
            else
                _unitOfWork.Place.Update(place);
            _unitOfWork.save();
            return RedirectToAction("Index");
        }
    }
}
