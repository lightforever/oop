using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace BookShopRepository.Bases
{
    public sealed class BookStore : IObjectReference
    {
        private IObjectReference[] _books;

        private bool _locked=false;

        public IObjectReference Parent
        {
            get { return null; }
        }

        public object GetValueOfProperty(string propertyName)
        {
            if(string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException("propertyName can't be null or empty");
            }

            switch (propertyName)
            {
                case "Books":
                    {
                        var booksAsEnumerable = _books as IEnumerable<IObjectReference>;
                        return booksAsEnumerable;
                    }
                default:
                    return null;
            }
        }

        public void SetBooks(IObjectReference[] books)
        {
            if(_locked)
            {
                throw new SecurityException("Object is locked");
            }

            if (ReferenceEquals(books, null))
            {
                throw new ArgumentNullException("books");
            }

            for (int i = 0; i < books.Length; i++)
            {
                var book = books[i];
                if (ReferenceEquals(book, null))
                {
                    throw new ArgumentException(string.Format("Book at {0} index is null", i));
                }
                if (!(book is Book))
                {
                    throw new ArgumentException(string.Format("Book at {0} index is not a Book type", i));
                }
            }

            _books = books;
        }

        /// <summary>
        /// Set object state as Locked
        /// </summary>
        public void Lock()
        {
            _locked = true;
        }
    }
}