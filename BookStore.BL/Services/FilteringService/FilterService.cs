using BookStore.BL.Services.LibraryManageService;
using BookStore.BL.Services.LogService;
using BookStore.Shared.Enums;
using BookStore.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BL.Services.FilteringService
{
    public class FilterService : IFilterService
    {
        private readonly ILibraryManageService _manageService;
        private readonly ILogger _logger;

        public FilterService(ILogger logger ,ILibraryManageService libraryManage)
        {
            _manageService = libraryManage;
            _logger = logger;
        }
        public IEnumerable<LibraryItem> SearchForItems(string productType, string genre, int priceFrom, int priceTo, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                IEnumerable<LibraryItem> retValue = productType switch
                {
                    "Book" => _manageService.GetAllItems().OfType<Book>().Where(b =>
                          b.Genre.Equals(Enum.Parse(typeof(BookGenre), genre)) &&
                          b.Price > priceFrom && b.Price < priceTo &&
                          b.ReleaseDate > dateFrom && b.ReleaseDate < dateTo).AsEnumerable<LibraryItem>(),
                    "Journal" => _manageService.GetAllItems().OfType<Journal>().Where(j =>
                         j.Genre.Equals(Enum.Parse(typeof(JournalGenre), genre)) &&
                         j.Price > priceFrom && j.Price < priceTo &&
                         j.ReleaseDate > dateFrom && j.ReleaseDate < dateTo).AsEnumerable<LibraryItem>(),
                    _ => _manageService.GetBy(item =>
                       item.Price > priceFrom && item.Price < priceTo &&
                       item.ReleaseDate > dateFrom && item.ReleaseDate < dateTo)
                };
                return retValue;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while filtering data.");
            }
        }
    }
}
