using BookStoreRefactoring.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRefactoring
{
    public class ViewModelLocator
    {
        public MainViewModel? MainViewModel => App.ServiceProvider?.GetRequiredService<MainViewModel>();
        public ReportViewModel? ReportViewModel => App.ServiceProvider?.GetRequiredService<ReportViewModel>();
        public ReportFormViewModel? ReportFormViewModel => App.ServiceProvider?.GetRequiredService<ReportFormViewModel>();
        public EditViewModel? EditViewModel => App.ServiceProvider?.GetRequiredService<EditViewModel>();
        public NewItemViewModel? NewItemViewModel => App.ServiceProvider?.GetRequiredService<NewItemViewModel>();
    }
}
