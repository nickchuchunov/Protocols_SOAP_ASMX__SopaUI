using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryService.Models;

namespace LibraryService.Services 
{
    public interface ILibraryRepositoryService : IRepository<Book>
    {
        IList<Book> GetByTitle(string title);

        IList<Book> GetByAuthors(string authorName);

        IList<Book> GetByCategory(string category);
    }
}
