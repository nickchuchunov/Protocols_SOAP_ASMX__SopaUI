using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryService.Models;
using LibraryService.Services;
using System.Linq;
using Newtonsoft.Json;

namespace LibraryService.Services.impl
{
    public class LibraryRepository : ILibraryRepositoryService
    {
        private readonly ILibraryDatabaseContextService _dbContext;


        public LibraryRepository(ILibraryDatabaseContextService dbContext)
        {
            _dbContext = dbContext;
        }




        public int? Add(Book item)
        {
            // присваи  Id уникальный идентификатор.

            string id = Guid.NewGuid().ToString();
            item.Id = id;
            string BookSerialize = JsonConvert.SerializeObject(item);

            File.AppendAllText("Properties.Resources.books", BookSerialize);

            throw new NotImplementedException();
        }

        public int Delete(Book item)
        {








            throw new NotImplementedException();
        }

        public IList<Book> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<Book> GetByAuthors(string authorName)
        {
            return _dbContext.Books.Where(book => book.Authors.Where(autor => autor.Name.ToLower().Contains(authorName.ToLower())).Count() > 0).ToList();
        }

        public IList<Book> GetByCategory(string category)
        {
            return _dbContext.Books.Where(book => book.Category.ToLower().Contains(category.ToLower())).ToList();
        }

        public Book GetById(string id)
        {
            throw new NotImplementedException();
        }

        public IList<Book> GetByTitle(string title)
        {
            return _dbContext.Books.Where(book => book.Title.ToLower().Contains(title.ToLower())).ToList();
        }

        public int Update(Book item)
        {
            throw new NotImplementedException();
        }
    }
}