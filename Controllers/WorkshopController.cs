using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CraftWorkshopSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CraftWorkshopSystem.Controllers
{
    public class WorkshopController : Controller
    {
        BusinessLayer bl = new BusinessLayer();

        public IActionResult Index()
        {
            List<Workshop> workshops = bl.GetAllWorkshops();
            return View(workshops);
        }

        [HttpGet]
        public IActionResult Book(int id)
        {
            if (bl.IsWorkshopFull(id))
            {
                return RedirectToAction("Index");
            }

            Workshop workshop = bl.GetWorkshopById(id);
            if (workshop == null)
            {
                return NotFound();
            }

            Booking booking = new Booking
            {
                WorkshopId = workshop.Id,
                Workshop = workshop
            };

            return View(booking);
        }

        [HttpPost]
        public IActionResult Book(Booking booking)
        {
                bool success = bl.AddBooking(booking);
                if (success)
                {
                    return RedirectToAction("Index","Workshop");
                }

            booking.Workshop = bl.GetWorkshopById(booking.WorkshopId);
            return View(booking);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Instructors"] = new SelectList(bl.GetAllInstructors(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Workshop workshop)
        {
            ModelState.Remove("Instructor");
            if (ModelState.IsValid)
            {
                bl.AddWorkshop(workshop);
                TempData["SuccessMessage"] = "Workshop is added successfully!";
                return RedirectToAction("Privacy", "Workshop");
            }
            ViewData["Instructors"] = new SelectList(bl.GetAllInstructors(), "Id", "Name", workshop.InstructorId);
            return View(workshop);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Workshop workshop = bl.GetWorkshopById(id);
            if (workshop == null)
            {
                return NotFound();
            }
            ViewData["Instructors"] = new SelectList(bl.GetAllInstructors(), "Id", "Name", workshop.InstructorId);
            return View(workshop);
        }

        [HttpPost]
        public IActionResult Edit(int id ,Workshop workshop)
        {
            if (id != workshop.Id)
            {
                return NotFound();
            }
            
                bl.UpdateWorkshop(workshop);
                TempData["SuccessMessage"] = "Changes saved successfully!";
                return RedirectToAction("Privacy", "Workshop");

            ViewData["Instructors"] = new SelectList(bl.GetAllInstructors(), "Id", "Name", workshop.InstructorId);
            return View(workshop);
        }

        public IActionResult Delete(int id)
        {
            bl.DeleteWorkshop(id);
            TempData["SuccessMessage"] = "Workshop is deleted successfully!";
            return RedirectToAction("Privacy", "Workshop");
        }

        public IActionResult Privacy()
        {
            List<Workshop> workshops = bl.GetAllWorkshops();
            return View(workshops);
        }

        public IActionResult Details(int id)
        {
            var workshop = bl.GetWorkshopDetails(id);

            if (workshop == null)
            {
                return NotFound();
            }

            ViewData["RemainingSeats"] = workshop.Capacity - workshop.Bookings.Count();

            return View(workshop);
        }

    }
}