using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IgnatDariaLab2.Data;
using IgnatDariaLab2.Models;

namespace IgnatDariaLab2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly IgnatDariaLab2.Data.IgnatDariaLab2Context _context;

        public IndexModel(IgnatDariaLab2.Data.IgnatDariaLab2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Book = await _context.Book
                 .Include(b => b.Author)
                 .Include(b => b.Publisher)
                .ToListAsync();
        }
    }
}
