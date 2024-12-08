using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IgnatDariaLab2.Data;
using IgnatDariaLab2.Models;
using IgnatDariaLab2.Models.ViewModels;

namespace IgnatDariaLab2.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IgnatDariaLab2.Data.IgnatDariaLab2Context _context;

        public IndexModel(IgnatDariaLab2.Data.IgnatDariaLab2Context context)
        {
            _context = context;
        }

        public CategoryIndexData CategoryData { get; set; }
        public int CategoryID { get; set; }

        public async Task OnGetAsync(int? id)
        {
            
            CategoryData = new CategoryIndexData
            {
                Categories = await _context.Category
                    .Include(c => c.BookCategories)
                    .ThenInclude(bc => bc.Book)
                    .ThenInclude(b => b.Author)
                    .OrderBy(c => c.CategoryName)
                    .ToListAsync()
            };

           
            if (id != null)
            {
                CategoryID = id.Value;
                var category = CategoryData.Categories
                    .SingleOrDefault(c => c.ID == id.Value);

                if (category != null)
                {
                    CategoryData.Books = category.BookCategories.Select(bc => bc.Book);
                }
            }
        }
    }
}