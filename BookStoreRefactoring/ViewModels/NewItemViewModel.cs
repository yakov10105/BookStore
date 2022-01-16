using BookStore.BL.Services.LibraryManageService;
using BookStore.BL.Services.LogService;
using BookStore.Shared;
using BookStore.Shared.Enums;
using BookStore.Shared.Interfaces;
using BookStore.Shared.Models;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Controls;

namespace BookStoreRefactoring.ViewModels
{
    public class NewItemViewModel : BaseViewModel.BaseViewModel,IProductsCollectionUpdate
    {
        private readonly ILibraryManageService _managerService;
        private readonly ILogger _logger;
        public Action UpdateCollection { get; set ; }

        #region View Models References
        public MainViewModel? MainViewModel { get; set; }
        public IProductsCollectionUpdate? ReportViewModelProductsUpdate { get; set; }
        #endregion
        public RelayCommand AddCommand { get => DI.GetRelayCommand(AddNewItem); }

        #region Binding Properties


        private string[] _genre;
        public string[] GenreItems
        {
            get { return _genre; }
            set
            {
                _genre = value;
                OnNotifyPropertyChanged(nameof(GenreItems));
            }
        }

        private Visibility _bookVis;
        public Visibility BookVisibility
        {
            get { return _bookVis; }
            set
            {
                _bookVis = value;
                OnNotifyPropertyChanged(nameof(BookVisibility));
            }
        }

        private Visibility _journalVis;
        public Visibility JournalVisibility
        {
            get { return _journalVis; }
            set
            {
                _journalVis = value;
                OnNotifyPropertyChanged(nameof(JournalVisibility));
            }
        }

        private ComboBoxItem _selectedType;
        public ComboBoxItem SelectedType
        {
            get { return _selectedType; }
            set
            {
                _selectedType = value;
                RenderFields(value);
                OnNotifyPropertyChanged(nameof(SelectedType));
            }
        }

        #endregion

        #region Item Properties
        private string _price;
        public string Price { get => _price; set => Set(ref _price, value); }

        private string _quantity;
        public string Quantity { get => _quantity; set => Set(ref _quantity, value); }

        private string _selectedGenre;
        public string SelectedGenre { get => _selectedGenre; set => Set(ref _selectedGenre, value); }

        private string _writer;
        public string Writer { get => _writer; set => Set(ref _writer, value); }

        private DateTime _date;
        public DateTime Date { get => _date; set => Set(ref _date, value); }

        private string _discount;
        public string Discount { get => _discount; set => Set(ref _discount, value); }

        private string _edition;
        public string EditionNumber { get => _edition; set => Set(ref _edition, value); }

        private string _name;
        public string Name { get => _name; set => Set(ref _name, value); }


        #endregion


        public NewItemViewModel(ILibraryManageService manageService, ILogger logger)
        {
            _managerService = manageService;
            _logger = logger;
            MainViewModel = App.ServiceProvider?.GetRequiredService<MainViewModel>();
            ReportViewModelProductsUpdate = App.ServiceProvider?.GetRequiredService<ReportViewModel>();
            UpdateCollection += ReportViewModelProductsUpdate?.UpdateCollection;
        }
        private void RenderFields(ComboBoxItem value)
        {
            switch (value?.Content)
            {
                case "Book":
                    GenreItems = Enum.GetNames(typeof(BookGenre));
                    BookVisibility = Visibility.Visible;
                    JournalVisibility = Visibility.Hidden;
                    break;
                case "Journal":
                    GenreItems = Enum.GetNames(typeof(JournalGenre));
                    JournalVisibility = Visibility.Visible;
                    BookVisibility = Visibility.Hidden;
                    break;
                default:
                    GenreItems = Array.Empty<string>();
                    JournalVisibility = Visibility.Hidden;
                    BookVisibility = Visibility.Hidden;
                    break;
            }
        }
        private void AddNewItem()
        {
            LibraryItem? newItem = _selectedType?.Content switch
            {
                "Book" => new Book(double.Parse(_price), _name, _date, int.Parse(_quantity), int.Parse(_discount), _writer, (BookGenre)Enum.Parse(typeof(BookGenre), _selectedGenre), int.Parse(_edition)),
                "Journal" => new Journal(double.Parse(_price), _name, _date, int.Parse(_quantity), int.Parse(_discount), _writer, (JournalGenre)Enum.Parse(typeof(JournalGenre), _selectedGenre)),
                _ => null
            };
            if (newItem != null)
            {
                try
                {
                    _managerService.AddNewItem(newItem);
                    UpdateCollection.Invoke();
                    MainViewModel?.NewItemWindow?.Close();
                }
                catch (Exception ex)
                {
                    _logger.Log(ex.Message);
                }
            }
        }
    }
}
