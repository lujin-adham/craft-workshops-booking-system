using Microsoft.AspNetCore.Mvc;
using CraftWorkshopSystem.Models;
using System.Linq;

namespace CraftWorkshopSystem.Controllers
{
    public class InstructorController : Controller
    {
        BusinessLayer bl = new BusinessLayer();

        public IActionResult Index()
        {
            List<Instructor> instructors = bl.GetAllInstructors();
            return View(instructors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                bl.AddInstructor(instructor);
                TempData["SuccessMessage"] = "Instructor is added successfully!";
                return RedirectToAction("Index");
            }
            return View(instructor);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Instructor instructor = bl.GetInstructorById(id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }

        [HttpPost]
        public IActionResult Edit(int id, Instructor instructor)
        {
            if (id != instructor.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                bl.UpdateInstructor(instructor);
                TempData["SuccessMessage"] = "Changes saved successfully!";
                return RedirectToAction("Index");
            }
            return View(instructor);
        }

        public IActionResult Delete(int id)
        {
            bl.DeleteInstructor(id);
            TempData["SuccessMessage"] = "Instructor deleted successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id, string returnUrl)
        {
            var instructor = bl.GetInstructorById(id);

            if (instructor == null)
            {
                 return NotFound();
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(instructor);
        }
    }
}