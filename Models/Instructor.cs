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
    public class Instructor
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string Bio { get; set; }
        
        public string Email { get; set; }
        
        public List<Workshop> Workshops { get; set; } = new List<Workshop>();
    }
}