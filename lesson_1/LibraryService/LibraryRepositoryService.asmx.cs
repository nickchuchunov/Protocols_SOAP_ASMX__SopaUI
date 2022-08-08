using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using LibraryService.Services;
using LibraryService.Services.impl;
using LibraryService.Models;

namespace LibraryService
{
    /// <summary>
    /// Сводное описание для LibraryRepositoryService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Чтобы разрешить вызывать веб-службу из скрипта с помощью ASP.NET AJAX, раскомментируйте следующую строку. 
     [System.Web.Script.Services.ScriptService]
    public class LibraryRepositoryService : System.Web.Services.WebService
    {


        private readonly ILibraryRepositoryService _libraryRepositoryService;



        public LibraryRepositoryService()
        {
            _libraryRepositoryService = new LibraryRepository(new LibraryDatabaseContext());

        }
        [WebMethod]
        public List<Book> GetBooksByTitle(string title)
        {
            return _libraryRepositoryService.GetByTitle(title).ToList();
        }



        [WebMethod]
        public List<Book> GetBooksByAuthor(string authorName)
        {
            return _libraryRepositoryService.GetByAuthors(authorName).ToList();
        }

        [WebMethod]
        public List<Book> GetBooksCategory(string category)
        {
            return _libraryRepositoryService.GetByCategory(category).ToList();
        }










        [WebMethod]
        public string HelloWorld()
        {
            return "Привет всем!";
        }
    }
}
