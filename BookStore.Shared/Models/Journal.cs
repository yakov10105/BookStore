using BookStore.Shared.Enums;
using BookStore.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Shared.Models
{
    public class Journal : LibraryItem, IJournal
    {
        public string MainEditorName { get; set; }
        public JournalGenre Genre { get; set; }

        public Journal(double price, string name, DateTime releaseDate,int quantity,int discount, string mainEditorName, JournalGenre genre) : base(price, name, releaseDate,quantity, discount)
        {
            MainEditorName = mainEditorName;
            Genre = genre;
        }
    }
}
