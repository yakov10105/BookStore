using BookStore.Shared.Enums;
using BookStore.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Shared.Models
{
    public class Book : LibraryItem, IBook
    {
        public string AuthorName { get; set; }
        public BookGenre Genre { get; set; }
        public int EditionNumber { get; set; }

        public Book(double price, string name, DateTime releaseDate,int quantity,int discount,  string authorName, BookGenre genre,int editionNumber) : base(price, name, releaseDate,quantity,discount)
        {
            AuthorName = authorName;
            Genre = genre;
            EditionNumber = editionNumber;
        }
    }
}
