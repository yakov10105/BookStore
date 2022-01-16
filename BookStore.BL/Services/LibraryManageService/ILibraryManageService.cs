using BookStore.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BL.Services.LibraryManageService
{
    public interface ILibraryManageService
    {
        void AddNewItem(LibraryItem item);
        void AddRangeQuantity(LibraryItem item, int quantity);
        void UpdateItem(LibraryItem item);
        void RemoveFromStock(LibraryItem item);
        void RemoveRangeQuantity(LibraryItem item, int quantity);
        void SetDiscount(LibraryItem item,int discountValue);
        IEnumerable<LibraryItem> GetBy(Func<LibraryItem, bool> func);
        IEnumerable<LibraryItem> GetAllItems();
    }
}
