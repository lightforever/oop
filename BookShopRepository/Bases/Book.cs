using System;
using System.Security;

namespace BookShopRepository.Bases
{
    public class Book : IObjectReference, ICloneable
    {
        #region Equality members

        protected bool Equals(Book other)
        {
            return string.Equals(_author, other._author) && string.Equals(_title, other._title) && _numberOfPages == other._numberOfPages;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Book) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (_author != null ? _author.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (_title != null ? _title.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ _numberOfPages;
                return hashCode;
            }
        }

        #endregion

        private IObjectReference _parent;
        private readonly string _author;
        private readonly string _title;
        private readonly int _numberOfPages;

        private bool _locked;

        /// <summary>
        ///
        /// </summary>
        /// <param name="author">Author's name</param>
        /// <param name="title">Book's title</param>
        /// <param name="numberOfPages">Number of pages in book</param>
        public Book(string author, string title, int numberOfPages)
        {
            if(string.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentException("Author can't be null or empty");
            }

            if(string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title can't be null or empty");
            }

            if(numberOfPages<=0)
            {
                throw new ArgumentException("Number of pages must be great than zero");
            }

            _author = author;
            _title = title;
            _numberOfPages = numberOfPages;
        }

        /// <summary>
        /// It must be called before book usage
        /// </summary>
        /// <param name="parent">BookStore - host of this book</param>
        public void SetParent(IObjectReference parent)
        {
            if(_locked)
            {
                throw new SecurityException("Object is locked");
            }

            if (ReferenceEquals(parent, null))
            {
                throw new ArgumentNullException("parent");
            }

            if (!(parent is BookStore))
            {
                throw new ArgumentException();
            }

            _parent = parent;
        }

        public void Lock()
        {
            _locked = true;
        }

        public IObjectReference Parent
        {
            get
            {
                if(ReferenceEquals(_parent,null))
                {
                    throw new Exception("SetParent must be called before usage");
                }
                return _parent;
            }
        }

     
        public object GetValueOfProperty(string propertyName)
        {
            if(string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException("Invalid property name. It can't be empty or null");
            }

            switch (propertyName)
            {
                case "Author":
                    return _author;
                case "Title":
                    return _title;
                case "NumberOfPages":
                    return _numberOfPages;
                default:
                    return null;
            }
        }

        public object Clone()
        {
            var result = new Book(_author, _title, _numberOfPages);
            if(!ReferenceEquals(_parent,null))
            {
                result.SetParent(_parent);
            }
            return result;
        }
    }
}