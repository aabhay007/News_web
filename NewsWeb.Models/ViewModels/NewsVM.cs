using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsWeb.Models.ViewModels
{
    public class NewsVM
    {
        public News News { get; set; }
        public IEnumerable<SelectListItem> PlaceList { get; set; }
        public IEnumerable<SelectListItem>CategoryList { get; set; }
    }
}
