using BookStore.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Shared.Interfaces
{
    public interface IJournal
    {
        string MainEditorName { get; set; }
        JournalGenre Genre { get; set; }
    }
}
