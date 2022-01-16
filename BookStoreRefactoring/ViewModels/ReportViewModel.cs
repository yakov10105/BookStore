using BookStore.BL.Services.LibraryManageService;
using BookStore.BL.Services.LogService;
using BookStore.Shared;
using BookStore.Shared.Interfaces;
using BookStore.Shared.Models;
using BookStoreRefactoring.Windows;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace BookStoreRefactoring.ViewModels
{
    public class ReportViewModel : BaseViewModel.BaseViewModel,ICanDisabled,IProductsCollectionUpdate
    {
        private readonly ILibraryManageService _managerService;
        private readonly ILogger _logger;

        private bool _isWindowEnabled;
        public bool IsEnabled
        {
            get { return _isWindowEnabled; }
            set
            {
                if (_isWindowEnabled != value)
                {
                    _isWindowEnabled = value;
                    OnNotifyPropertyChanged(nameof(IsEnabled));
                }
            }
        }


        private ObservableCollection<LibraryItem> _allItems;
        public ObservableCollection<LibraryItem> AllItems
        {
            get {return _allItems ; }
            set 
            {
               _allItems=value ;
                OnNotifyPropertyChanged(nameof(AllItems));
            }
        }


        public RelayCommand DeleteItem { get => DI.GetRelayCommand(DeleteItemCommand); }
        public RelayCommand DeleteAll { get => DI.GetRelayCommand(DeleteAllStock); }
        public RelayCommand EditItem { get => DI.GetRelayCommand(EditItemCommand); }


        public EditWindow? EditWindow { get; set; }


        private LibraryItem _selectedItem;
        public LibraryItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem == value)
                    return;
                _selectedItem = value;
                OnNotifyPropertyChanged(nameof(SelectedItem));
            }
        }

        public Action UpdateCollection { get; set; }

        public ReportViewModel(ILogger logger,ILibraryManageService manageService)
        {
            _isWindowEnabled = true;
            _managerService = manageService;
            _logger = logger;
            UpdateCollection += () => { AllItems = DI.GetObservableCollection<LibraryItem>(_managerService.GetAllItems()); };
            UpdateCollection.Invoke();
        }

        private void EditItemCommand()
        {
            if (CheckSelected(nameof(EditItemCommand)))
            {
                EditWindow = App.ServiceProvider?.GetRequiredService<EditWindow>();
                EditWindow.Closed += (s, e) => 
                {
                    _isWindowEnabled = true;
                    UpdateCollection.Invoke();
                };
                _isWindowEnabled = false;
                EditWindow.Show();
            }
            else return;
        }
        private void DeleteItemCommand()
        {
            if(!CheckSelected(nameof(DeleteItemCommand)))return;
            try
            {
                _managerService.RemoveRangeQuantity(SelectedItem, 1);
                UpdateCollection.Invoke();
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message);
            }
        }
        private void DeleteAllStock()
        {
            if(!CheckSelected(nameof(DeleteAllStock)))return;
            try
            {
                _managerService.RemoveFromStock(SelectedItem);
                UpdateCollection.Invoke();
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message);
            }
        }
        private bool CheckSelected(string operation)
        {
            if (SelectedItem == null)
            {
                MessageBox.Show($"For {operation} Operation you must select item.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

    }
}
