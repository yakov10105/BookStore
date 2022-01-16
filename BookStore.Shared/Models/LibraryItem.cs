using BookStore.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Shared.Models
{
    public abstract class LibraryItem
    {
        public int Discount { get; set; }
        public string Name { get; set; }
        [Key]
        public Guid IBSN { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Price { get; set; }
        public DateTime AddedToStoreDate { get; set; }
        public int Quantity { get; set; }

        public LibraryItem(double price, string name, DateTime releaseDate, int quantity,int discount)
        {
            Name = name;
            ReleaseDate = releaseDate;
            Price = price;
            AddedToStoreDate = DateTime.Now;
            Quantity = quantity;
            IBSN = Guid.NewGuid();
        }
    }
}
