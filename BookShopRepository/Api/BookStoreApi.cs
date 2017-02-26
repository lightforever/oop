using System;
using System.Collections.Generic;
using System.Linq;
using BookShopRepository.Bases;

namespace BookShopRepository.Api
{
    internal class BookStoreApi:IBookStoreApi
    {
        private readonly BookStore[] _bookStores;

        public BookStoreApi(BookStore[] bookStores)
        {
            _bookStores = bookStores;
        }

        public object Select(BookFilter filter)
        {
            var result = _bookStores.SelectMany(item => item.GetValueOfProperty("Books") as Book[]).Where(filter.IsValidFor);
            if(filter.Distinct)
            {
                result = result.Distinct();
            }

            if(filter.SelectCount)
            {
                return result.Count();
            }

            if(!string.IsNullOrWhiteSpace(filter.SumPropertyName))
            {
                int resultSum = 0;
                foreach (var book in result)
                {
                    var valueOfProperty = book.GetValueOfProperty(filter.SumPropertyName);
                    if(ReferenceEquals(valueOfProperty,null))
                    {
                        throw new Exception(string.Format("Property with name {0} does not exists",filter.SumPropertyName));
                    }
                    if(valueOfProperty is int)
                    {
                        resultSum += (int)valueOfProperty;
                    }
                    else
                    {
                        resultSum += 1;
                    }

                }

                return resultSum;
            }

            if(!string.IsNullOrWhiteSpace(filter.SelectPropertyName))
            {
                var propertyNameResults=result.Select(item=>item.GetValueOfProperty(filter.SelectPropertyName));
                if(filter.Distinct)
                {
                    propertyNameResults = propertyNameResults.Distinct();
                }

                return propertyNameResults.ToArray();

            }

            return result.ToArray();

        }
    }
}