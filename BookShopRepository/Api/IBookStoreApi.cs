using System.Collections.Generic;
using BookShopRepository.Bases;

namespace BookShopRepository.Api
{
    /// <summary>
    /// Book store api to get books by: author name, number pages e.t.c....
    /// </summary>
    public interface IBookStoreApi
    {
        object Select(BookFilter filter);
    }
}