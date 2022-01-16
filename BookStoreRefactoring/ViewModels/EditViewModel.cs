using BookStore.BL.Services.LibraryManageService;
using BookStore.BL.Services.LogService;
using BookStore.DAL;
using BookStore.Shared;
using BookStore.Shared.Enums;
using BookStore.Shared.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRefactoring.ViewModels
{
    public class EditViewModel : BaseViewModel.BaseViewModel
    {
        private readonly ILibraryManageService _managerService;
        private readonly ILogger _logger;

        #region ItemFields
        public string Name { get; set; }
        public int Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Quantity { get; set; }
        #endregion
        public ReportViewModel ReportViewModel { get; set; }
        public LibraryItem EditedItem { get; set; }
        public LibraryItem NewItem { get; set; }
        public RelayCommand UpdateItem{ get; set; }
        public EditViewModel(ILogger logger,ILibraryManageService libraryManage)
        {
            _managerService = libraryManage;
            _logger = logger;
            ReportViewModel = App.ServiceProvider.GetRequiredService<ReportViewModel>();
            EditedItem = ReportViewModel.SelectedItem;
            UpdateItem = DI.GetRelayCommand(UpdateCommand);
        }

        private void UpdateCommand()
        {
            try
            {
                UpdateProperties();
                _managerService.UpdateItem(EditedItem);
                 ReportViewModel.EditWindow.Close();
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message);
            }
        }

        private void UpdateProperties()
        {
            EditedItem.Name = Name;
            EditedItem.Price = Price;
            EditedItem.Quantity = Quantity;
            EditedItem.ReleaseDate = ReleaseDate;
        }
    }
}
