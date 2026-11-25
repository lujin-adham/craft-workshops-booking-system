using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CraftWorkshopSystem.Models
{
    public class BusinessLayer
    {
        ApplicationDbContext dbcon = new ApplicationDbContext();


        public void SeedingDB()
        {
            if (!dbcon.Workshops.Any())
            {
                var i1 = new Instructor { Name = "Sarah Ahmed", Bio = "Professional Artist & Painter", Email = "sarah@art.com" };
                var i2 = new Instructor { Name = "Karim Mohamed", Bio = "Handmade Crafts Expert", Email = "karim@crafts.com" };

                dbcon.Instructors.AddRange(i1, i2);
                dbcon.SaveChanges();

                Workshop w1 = new Workshop 
                { 
                    Title = "Painting & Canvas Art", 
                    Description = "Learn acrylic painting techniques and color mixing.", 
                    Price = 450, 
                    Date = DateTime.Now.AddDays(4), 
                    ImageUrl = "/images/painting.png",
                    Capacity = 20,
                    Location = "Art Cafe, Maadi",
                    InstructorId = i1.Id
                };

                Workshop w2 = new Workshop 
                { 
                    Title = "Handmade Jewelry Design", 
                    Description = "Create unique accessories using beads and wire.", 
                    Price = 650, 
                    Date = DateTime.Now.AddDays(8), 
                    ImageUrl = "/images/jewelry.jpg",
                    Capacity = 10,
                    Location = "Beit Barsoum, Old Cairo",
                    InstructorId = i2.Id

                };

                dbcon.Workshops.Add(w1);
                dbcon.Workshops.Add(w2);
                dbcon.SaveChanges();
            }
        }

        public List<Workshop> GetAllWorkshops()
        {
            return dbcon.Workshops
            .Include(w => w.Instructor)
            .Include(w => w.Bookings)
            .ToList();
        }

        public Workshop GetWorkshopById(int id)
        {
            return dbcon.Workshops.Find(id);
        }

        public void AddWorkshop(Workshop workshop)
        {
            dbcon.Workshops.Add(workshop);
            dbcon.SaveChanges();
        }

        public void DeleteWorkshop(int id)
        {
            var workshop = dbcon.Workshops.Find(id);
            if (workshop != null)
            {
                dbcon.Workshops.Remove(workshop);
                dbcon.SaveChanges();
            }
        }

        public bool IsWorkshopFull(int workshopId)
        {
            var workshop = dbcon.Workshops.Find(workshopId);
            if (workshop == null) return true;

            int currentBookings = dbcon.Bookings.Count(b => b.WorkshopId == workshopId);
            return currentBookings >= workshop.Capacity;
        }

        public bool AddBooking(Booking booking)
        {
            booking.Id = 0;
            if (IsWorkshopFull(booking.WorkshopId)) return false;

            dbcon.Bookings.Add(booking);
            dbcon.SaveChanges();
            return true;
        }

        public void UpdateWorkshop(Workshop workshop)
        {
            var existingWorkshop = dbcon.Workshops.Find(workshop.Id);
            if (existingWorkshop != null)
            {
                existingWorkshop.Title = workshop.Title;
                existingWorkshop.Price = workshop.Price;
                existingWorkshop.Date = workshop.Date;
                existingWorkshop.Capacity = workshop.Capacity;
        
                dbcon.SaveChanges();
            }
        }


        public Workshop GetWorkshopDetails(int id)
        {
            return dbcon.Workshops
            .Include(w => w.Bookings)
            .Include(w => w.Instructor)
            .FirstOrDefault(w => w.Id == id);
        }

        public List<Instructor> GetAllInstructors()
        {
            return dbcon.Instructors.ToList();
        }

       public Instructor GetInstructorById(int id)
       {
            return dbcon.Instructors.FirstOrDefault(i => i.Id == id);
       }

       public void AddInstructor(Instructor instructor)
       {
            dbcon.Instructors.Add(instructor);
            dbcon.SaveChanges();
       }

        public void UpdateInstructor(Instructor instructor)
       {
           dbcon.Instructors.Update(instructor);
           dbcon.SaveChanges();
       }

       public void DeleteInstructor(int id)
       {
           var instructor = GetInstructorById(id);
           if (instructor != null)
           {
                dbcon.Instructors.Remove(instructor);
                dbcon.SaveChanges();
           }
       }
    }    
}