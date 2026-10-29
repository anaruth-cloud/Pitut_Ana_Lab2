using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pitut_Ana_Lab2.Models;

namespace Pitut_Ana_Lab2.Pages.PublisherPages
{
    public class IndexModel : PageModel
    {
        private readonly Pitut_Ana_Lab2Context _context;

        public IndexModel(Pitut_Ana_Lab2Context context)
        {
            _context = context;
        }

        public IList<Publisher> Publisher { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Publisher = await _context.Publisher.ToListAsync();
        }
    }
}
