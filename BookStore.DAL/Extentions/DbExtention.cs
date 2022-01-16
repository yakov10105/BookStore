using BookStore.Shared.Interfaces;
using BookStore.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Extentions
{
    public static class DbExtention
    {
        private static List<Journal> books = new List<Journal>()
        {
            new Journal(15.95,"DSADASD",DateTime.Now,40,0, "Yakov", Shared.Enums.JournalGenre.Cars)
        };
        private static List<Book> journals = new List<Book>()
        {
            new Book(15.95,"2ddddd",DateTime.Now,40,0, "Yakov", Shared.Enums.BookGenre.Adventure,5)
        };
        public static void SeedData(this ModelBuilder builder)
        {
            builder.Entity<Journal>().HasData(books);
            builder.Entity<Book>().HasData(journals);
        }
    }
}
