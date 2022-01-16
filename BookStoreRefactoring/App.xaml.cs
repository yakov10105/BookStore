using BookStore.BL.Services.AuthService;
using BookStore.BL.Services.FilteringService;
using BookStore.BL.Services.LibraryManageService;
using BookStore.BL.Services.LogService;
using BookStore.DAL;
using BookStore.DAL.Repositories;
using BookStore.Shared;
using BookStore.Shared.Interfaces;
using BookStore.Shared.Models;
using BookStoreRefactoring.ViewModels;
using BookStoreRefactoring.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace BookStoreRefactoring
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider? ServiceProvider { get; set; }
        public App()
        {
            ServiceCollection services = DI.GetServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
        }


        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<BookStoreDbContext>(opt =>
            {
                opt.UseSqlServer("Server=(localdb)\\BookStoreDB;Database=BookStore;Trusted_Connection=True;");
            });

            //registering all the view models
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<ReportFormViewModel>();
            services.AddSingleton<ReportViewModel>();
            services.AddSingleton<EditViewModel>();
            services.AddSingleton<NewItemViewModel>();

            //registering all the windows
            services.AddTransient<MainWindow>();
            services.AddTransient<EditWindow>();
            services.AddTransient<ReportFormWindow>();
            services.AddTransient<NewItemWindow>();
            services.AddTransient<ReportWindow>();

            //registering all the services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IFilterService, FilterService>();
            services.AddSingleton<ILogger,Logger>();
            services.AddSingleton<ILibraryManageService, LibraryManageService>();

            //registering all the repositories
            services.AddSingleton<IRepository<LibraryItem>,LibraryRepository>();
            services.AddSingleton<IRepository<User>, UserRepository>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            MainWindow? mainWindow = ServiceProvider?.GetService<MainWindow>();
            mainWindow?.Show();
        }
        
    }
}
