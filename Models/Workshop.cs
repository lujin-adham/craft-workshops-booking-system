using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace CraftWorkshopSystem.Models
{
    public class Workshop
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public string ImageUrl { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }
        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}