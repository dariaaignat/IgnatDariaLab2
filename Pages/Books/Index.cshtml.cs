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
            {
                _context = context;
            }
        }

        public IList<Book> Book { get; set; } = default!;
        public BookData BookD { get; set; }
        public int BookID { get; set; }
        public int CategoryID { get; set; }
        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }

        public string CurrentFilter { get; set; }
        public async Task OnGetAsync(int? id, int? categoryID, string sortOrder, string searchString)
        {
         
            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            AuthorSort = sortOrder == "author" ? "author_desc" : "author";

            CurrentFilter = searchString;

        
            var booksQuery = _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
                .AsQueryable();

       
            if (!String.IsNullOrEmpty(searchString))
            {
                booksQuery = booksQuery.Where(b =>
                    b.Title.Contains(searchString) ||
                    (b.Author.FirstName + " " + b.Author.LastName).Contains(searchString));
            }

      
            booksQuery = sortOrder switch
            {
                "title_desc" => booksQuery.OrderByDescending(b => b.Title),
                "author" => booksQuery.OrderBy(b => b.Author.LastName).ThenBy(b => b.Author.FirstName),
                "author_desc" => booksQuery.OrderByDescending(b => b.Author.LastName).ThenByDescending(b => b.Author.FirstName),
                _ => booksQuery.OrderBy(b => b.Title),
            };

          
            BookD = new BookData
            {
                Books = await booksQuery.AsNoTracking().ToListAsync()
            };

         
            if (id != null)
            {
                BookID = id.Value;
                var book = BookD.Books.FirstOrDefault(b => b.ID == id.Value);
                if (book != null)
                {
                    BookD.Categories = book.BookCategories.Select(bc => bc.Category);
                }
            }
        }

    }
}

