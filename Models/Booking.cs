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
    public class Booking
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter your name")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Please enter your phone number")]
        public string CustomerPhone { get; set; }
        public int WorkshopId { get; set; }
        public Workshop Workshop { get; set; }
    }
}