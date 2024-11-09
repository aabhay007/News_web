using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsWeb.Models
{
    public class News
    {
        public int Id { get; set; }
        [Required]
        public string HeadLine { get; set; }
        [Required]
        public string Description { get; set; }

        [Display(Name = "Img Url")]
        public string ImageUrl { get; set; }
        [Display(Name = "Place")]
        public int PlaceId { get; set; }
        public Place Place { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string External_Url { get; set; }
    }
}
