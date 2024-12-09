using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IgnatDariaLab2.Models;

namespace IgnatDariaLab2.Data
{
    public class IgnatDariaLab2Context : DbContext
    {
        public IgnatDariaLab2Context (DbContextOptions<IgnatDariaLab2Context> options)
            : base(options)
        {
        }

        public DbSet<IgnatDariaLab2.Models.Book> Book { get; set; } = default!;
        public DbSet<IgnatDariaLab2.Models.Publisher>? Publisher { get; set; } = default!;
        public DbSet<IgnatDariaLab2.Models.Author>? Author { get; set; } = default!;
        public DbSet<IgnatDariaLab2.Models.Category>? Category { get; set; } = default!;
        public DbSet<IgnatDariaLab2.Models.Member> Member { get; set; } = default!;
        public DbSet<IgnatDariaLab2.Models.Borrowing> Borrowing { get; set; } = default!;
    }
}
