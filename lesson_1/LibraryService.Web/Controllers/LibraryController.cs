using LibraryService.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LibraryServiceReference1;

namespace LibraryService.Web.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILogger<LibraryController> _logger;

        public LibraryController(ILogger<LibraryController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy(SearchType searchType, string searchString)
        {
            LibraryRepositoryServiceSoapClient libraryRepositoryServiceSoapClient =
                new LibraryRepositoryServiceSoapClient(LibraryRepositoryServiceSoapClient.EndpointConfiguration.LibraryRepositoryServiceSoap12);

            var bookCategoryViewModel = new BookCategoryViewModel()
            {
                

            };

            bookCategoryViewModel.Books = new Book[1000];

            for (int i =0; i<0; i++) { bookCategoryViewModel.Books[i] = new Book(); }



            if (!string.IsNullOrEmpty(searchString)&&searchString.Length>=3 )
            {
                switch (searchType) 
                {
                    case SearchType.Title:
                        bookCategoryViewModel.Books = libraryRepositoryServiceSoapClient.GetBooksByTitle(searchString);
                        break;

                    case SearchType.Category:
                        bookCategoryViewModel.Books = libraryRepositoryServiceSoapClient.GetBooksCategory(searchString);
                        break;
                    case SearchType.Author:
                        bookCategoryViewModel.Books = libraryRepositoryServiceSoapClient.GetBooksByAuthor(searchString);
                        break;
                }
            
            }



            return View(bookCategoryViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
