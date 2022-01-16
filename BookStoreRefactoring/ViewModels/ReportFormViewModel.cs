using BookStore.BL.Services.FilteringService;
using BookStore.BL.Services.LogService;
using BookStore.Shared;
using BookStore.Shared.Enums;
using BookStore.Shared.Interfaces;
using BookStoreRefactoring.Windows;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Controls;

namespace BookStoreRefactoring.ViewModels
{
    public class ReportFormViewModel : BaseViewModel.BaseViewModel, IProductsCollectionUpdate,ICanDisabled
    {
        private readonly ILogger _logger;
        private readonly IFilterService _filterService;
        public ReportWindow? ReportWindowRef{ get; set; }
        public ReportViewModel? ReportViewModel { get; set; }
        public Action? UpdateCollection { get; set; }
        private bool _isEnabled=true;
        public bool IsEnabled
        { 
            get{return _isEnabled; }
            set 
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    OnNotifyPropertyChanged(nameof(IsEnabled));
                }
            }
        }

        public RelayCommand SearchCommand{ get => DI.GetRelayCommand(SearchItems); }


        #region Filter Properties
        private ComboBoxItem _productType;
        public ComboBoxItem ProductType
        {
            get { return _productType; }
            set 
            {
               _productType=value ;
                SetGenres(value);
                OnNotifyPropertyChanged(nameof(ProductType));
            } 
        }

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

        private string _selectedGenre;
        public string SelectedGenre { get => _selectedGenre; set => Set(ref _selectedGenre, value); }

        private string _priceFrom;
        public string PriceFrom { get => _priceFrom; set => Set(ref _priceFrom, value); }

        private string _priceTo;
        public string PriceTo { get => _priceTo; set => Set(ref _priceTo, value); }

        private DateTime _dateFrom;
        public DateTime DateFrom { get => _dateFrom; set => Set(ref _dateFrom, value); }

        private DateTime _dateTo;
        public DateTime DateTo { get => _dateTo; set => Set(ref _dateTo, value); }
        #endregion

        public ReportFormViewModel(ILogger logger,IFilterService filterService)
        {
            _logger = logger;
            _filterService = filterService;
            ReportViewModel = App.ServiceProvider?.GetRequiredService<ReportViewModel>();
            ReportWindowRef = App.ServiceProvider?.GetRequiredService<ReportWindow>();
            
        }
        private void SetGenres(ComboBoxItem value)
        {
            if (value!=null)
            {
                GenreItems = value.Content switch
                {
                    "Book" => Enum.GetNames(typeof(BookGenre)),
                    "Journal" => Enum.GetNames(typeof(JournalGenre)),
                    _ => Array.Empty<string>()
                }; 
            }
        }
        private void SearchItems()
        {
            try
            {
                var filteredItems = _filterService.SearchForItems(ProductType.Content.ToString(), SelectedGenre, int.Parse(PriceFrom), int.Parse(PriceTo), DateFrom, DateTo);
                UpdateCollection += () => { ReportViewModel.AllItems = DI.GetObservableCollection(filteredItems); };
                UpdateCollection.Invoke();
                IsEnabled = false;
                ReportWindowRef?.Show();
                ReportWindowRef.Closed += (s, a) => { IsEnabled = true; };
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message);
            }
        }
    }
}