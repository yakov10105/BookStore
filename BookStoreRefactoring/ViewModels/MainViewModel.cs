using BookStore.Shared;
using BookStore.Shared.Interfaces;
using BookStoreRefactoring.Windows;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.DependencyInjection;

namespace BookStoreRefactoring.ViewModels
{
    public class MainViewModel : BaseViewModel.BaseViewModel,ICanDisabled
    {
        public ReportWindow? ReportWindow { get; set; }
        public ReportFormWindow? ReportFormWindow { get; set; }
        public NewItemWindow? NewItemWindow { get; set; }

        public RelayCommand AddNewItemCommand { get; set; }
        public RelayCommand ReportCommand { get; set; }
        public RelayCommand ReportFormCommand { get; set; }

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

        public MainViewModel()
        {
            _isWindowEnabled = true;
            ReportCommand = DI.GetRelayCommand(InvokeReportCommand);
            ReportFormCommand = DI.GetRelayCommand(InvokeReportFormCommand);
            AddNewItemCommand = DI.GetRelayCommand(InvokeAddNewItem);
        }

        private void InvokeReportCommand()
        {
            ReportWindow = App.ServiceProvider?.GetRequiredService<ReportWindow>();
            IsEnabled = false;
            ReportWindow.Show();
            ReportWindow.Closed += (e, args) => { IsEnabled = true; };
        }
        private void InvokeReportFormCommand()
        {
            ReportFormWindow = App.ServiceProvider?.GetRequiredService<ReportFormWindow>();
            IsEnabled = false;
            ReportFormWindow.Show();
            ReportFormWindow.Closed += (e, args) => { IsEnabled = true; };
        }
        private void InvokeAddNewItem()
        {
            NewItemWindow = App.ServiceProvider?.GetRequiredService<NewItemWindow>();
            IsEnabled = false;
            NewItemWindow.Show();
            NewItemWindow.Closed += (e, args) => { IsEnabled = true; };
        }
    }
}
