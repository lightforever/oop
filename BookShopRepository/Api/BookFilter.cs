using System;
using BookShopRepository.Bases;

namespace BookShopRepository.Api
{
    /// <summary>
    /// Contains information to filter valid books quick and safe
    /// </summary>
    public sealed class BookFilter
    {
        /// <summary>
        /// Author's name of book. AuthorNameSearchFlags will be applied
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Flags for AuthorName
        /// </summary>
        public SearchStringFlags AuthorNameSearchFlags { get; set; }

        /// <summary>
        /// Title of book. TitleSearchingFlags will be applied
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Flags for Title
        /// </summary>
        public SearchStringFlags TitleSearchFlags { get; set; }

        /// <summary>
        /// Min number of pages. If it's null - it will not be used
        /// </summary>
        public int? MinNumberOfPages { get; set; }

        /// <summary>
        /// Max number of pages. If it's null - it will not be used
        /// </summary>
        public int? MaxNumberOfPages { get; set; }

        /// <summary>
        /// If there was one book valids by this filter - don't add to result set
        /// </summary>
        public bool Distinct { get; set; }

        /// <summary>
        /// Like SQL Count(Rows)
        /// </summary>
        public bool SelectCount { get; set; }

        /// <summary>
        /// Like SQL Sum - for page count summing
        /// </summary>
        public string SumPropertyName { get; set; }

        //Like SQL Select ColumnName
        public string SelectPropertyName { get; set; }

        /// <summary>
        /// Method to validate book for current filter
        /// </summary>
        /// <param name="book">Book to validate</param>
        /// <returns>Book is valid for current filter? If so- returns True</returns>
        public bool IsValidFor(Book book)
        {
            if(!string.IsNullOrWhiteSpace(AuthorName))
            {
                return IsValidString((string)book.GetValueOfProperty("Author"),AuthorName,AuthorNameSearchFlags);
            }

            if(!string.IsNullOrWhiteSpace(Title))
            {
                return IsValidString((string)book.GetValueOfProperty("Title") as string,Title,AuthorNameSearchFlags);
            }

            int numberOfPages = (int) book.GetValueOfProperty("NumberOfPages");
            if(MinNumberOfPages.HasValue)
            {
                if(numberOfPages<MinNumberOfPages.Value)
                {
                    return false;
                }
            }

            if(MaxNumberOfPages.HasValue)
            {
                if(numberOfPages>MaxNumberOfPages.Value)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataString">String in wicth </param>
        /// <param name="searchingString">Searching string in dataString</param>
        /// <param name="flags">Options</param>
        /// <returns></returns>
        private bool IsValidString(string dataString,string searchingString,SearchStringFlags flags)
        {
            if(string.IsNullOrWhiteSpace(dataString))
            {
                throw new ArgumentNullException("dataString");
            }

            string currentDataString = dataString;
            string currentSearchingString = searchingString;

            if(flags.HasFlag(SearchStringFlags.IgnoreCase))
            {
                currentDataString = dataString.ToLower();
                currentSearchingString = searchingString.ToLower();
            }

            if(flags.HasFlag(SearchStringFlags.ContainsMode))
            {
                return currentDataString.Contains(currentSearchingString);
            }

            return currentDataString == currentSearchingString;
        }
    }
}