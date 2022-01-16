using BookStore.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BL.Services.FilteringService
{
    public interface IFilterService
    {
        IEnumerable<LibraryItem> SearchForItems(string productType, string genre, int priceFrom, int priceTo, DateTime dateFrom, DateTime dateTo);
    }

}
