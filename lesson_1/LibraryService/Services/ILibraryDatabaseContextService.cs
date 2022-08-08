using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryService.Models;

namespace LibraryService.Services
{
    public interface ILibraryDatabaseContextService
    {
        IList<Book> Books { get; }
    }
}
