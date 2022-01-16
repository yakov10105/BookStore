using BookStore.Shared.Interfaces;
using BookStore.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Repositories
{
    public class LibraryRepository : IRepository<LibraryItem>
    {
        private readonly BookStoreDbContext _dbContext;
        public LibraryRepository(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(LibraryItem item)
        {
            CheckNullItem(item,nameof(Add));
            try
            {
                _dbContext.Add(item);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                throw new DbUpdateException($"Issue while saving to DB: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(item)} could not be saved: {ex.Message}");
            }
        }

        public IEnumerable<LibraryItem> GetAll()
        {
            try
            {
                IEnumerable<LibraryItem> books = _dbContext.Books.AsEnumerable();
                IEnumerable<LibraryItem> journals = _dbContext.Journals.AsEnumerable();
                var all = books.Concat(journals);
                return all;
            }
            catch (Exception ex)
            {

                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public IEnumerable<LibraryItem> GetBy(Func<LibraryItem, bool> func)
        {
            try
            {
                return GetAll().Where(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public void Remove(LibraryItem item)
        {
            CheckNullItem(item,nameof(Remove));
            try
            {
                _dbContext.Remove(item);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                throw new DbUpdateException($"Issue while saving to DB: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(item)} could not be updated: {ex.Message}");
            }
        }

        public void Update(LibraryItem item)
        {

            CheckNullItem(item,nameof(Update));
            try
            {
                _dbContext.Update(item);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                throw new DbUpdateException($"Issue while saving to DB: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(item)} could not be updated: {ex.Message}");
            }
        }

        public static void CheckNullItem(LibraryItem item,string name)
        {
            if (item == null)
                throw new ArgumentNullException($"{name} entity must not be null");
        }
    }
}
