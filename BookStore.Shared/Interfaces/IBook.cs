using BookStore.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Shared.Interfaces
{
    public interface IBook
    {
        int EditionNumber { get; set; }
        string AuthorName { get; set; }
        BookGenre Genre { get; set; }
    }
}
