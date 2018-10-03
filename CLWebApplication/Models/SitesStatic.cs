using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLWebApp.Models
{
    public class SitesFixed
    {
        public List<SelectListItem> Sites = new List<SelectListItem>
            {
                new SelectListItem{ Value="2", Text="CL" },
                new SelectListItem{ Value="8", Text="Moko" }
            };
    }
}
