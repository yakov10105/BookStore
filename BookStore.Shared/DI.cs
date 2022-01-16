using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Shared
{
    public static class DI
    {
        public static ServiceCollection GetServiceCollection()=>new ServiceCollection();
        public static StreamWriter GetStreamWriter(string path)=> new StreamWriter(path,true);
        public static FileStream GetFileStream(string filePath,FileMode mode, FileAccess access) => new FileStream(filePath,mode,access);
        public static ObservableCollection<T> GetObservableCollection<T>(IEnumerable<T> initialList) => new ObservableCollection<T>(initialList);
        public static RelayCommand GetRelayCommand(Action action) => new RelayCommand(action);
    }
}
