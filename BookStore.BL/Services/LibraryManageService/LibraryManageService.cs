using BookStore.BL.Services.LogService;
using BookStore.DAL;
using BookStore.Shared.Interfaces;
using BookStore.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BL.Services.LibraryManageService
{
    public class LibraryManageService : ILibraryManageService
    {
        private readonly IRepository<LibraryItem> _repository;
        private readonly ILogger _logger;
        public LibraryManageService(IRepository<LibraryItem> repo,ILogger logger)
        {
            _repository = repo;
            _logger = logger;
        }

        public void AddNewItem(LibraryItem item)
        {
            var existItem = _repository.GetAll().FirstOrDefault((li) => li == item);
            if (existItem != null)
            {
                AddRangeQuantity(existItem, item.Quantity);
                return;
            }
            try
            {
                _repository.Add(item);
            }
            catch (Exception ex)
            {

                _logger.Log(ex.Message);
            }
        }

        public void AddRangeQuantity(LibraryItem item, int quantity)
        {
            try
            {
                item.Quantity += quantity;
                UpdateItem(item);
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message);
            }
        }

        public IEnumerable<LibraryItem> GetAllItems()
        {
            return _repository.GetAll();
        }

        public IEnumerable<LibraryItem> GetBy(Func<LibraryItem, bool> func)
        {
            return _repository.GetBy(func);
        }

        public void RemoveFromStock(LibraryItem item)
        {
            try
            {
                _repository.Remove(item);
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message);
            }
        }

        public void RemoveRangeQuantity(LibraryItem item, int quantity)
        {
            if (item.Quantity == quantity)
                RemoveFromStock(item);
            else if (item.Quantity >= quantity)
            {
                item.Quantity -= quantity;
                UpdateItem(item);
            }
            else throw new ArgumentOutOfRangeException("Wanted Quantity Bigger Than in stock.");
        }

        public void SetDiscount(LibraryItem item,int discountValue)
        {
            try
            {
                item.Discount = discountValue;
                UpdateItem(item);
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message);
            }
        }

        public void UpdateItem(LibraryItem item)
        {
            try
            {
                _repository.Update(item);
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message);
            }
        }
    }
}
