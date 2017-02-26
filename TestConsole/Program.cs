using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShopRepository;
using BookShopRepository.Api;
using BookShopRepository.Bases;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var bookStoreApi = BookStoreApiProvider.Current;

            //Writing all bringer Books Titles
            var bringerBooksNames = bookStoreApi.Select(new BookFilter() { AuthorName = "Bringer",SelectPropertyName = "Title",Distinct = true}) as object[];
            Console.WriteLine("Bringer's book titles:");
            foreach (var bringerBooksName in bringerBooksNames)
            {
                Console.WriteLine(bringerBooksName);
            }

            Console.WriteLine();

            Console.WriteLine("Bringer's books count: {0}", bookStoreApi.Select(new BookFilter() { AuthorName = "Bringer" ,SelectCount = true}));
            Console.WriteLine("Book stores count where one or more Bringers book: {0}", bookStoreApi.Select(new BookFilter() { AuthorName = "Bringer", SelectCount = true ,Distinct = true}));
            
            Console.WriteLine("Bringer's total number of pages:{0}",bookStoreApi.Select(new BookFilter(){AuthorName = "Bringer",Distinct=true,SumPropertyName = "NumberOfPages"}));

            Console.WriteLine();

            Console.WriteLine("Books with 'Animals' title and 200<=pageCount<=400: {0}", bookStoreApi.Select(new BookFilter() { Title = "Animals", MinNumberOfPages = 200, MaxNumberOfPages = 400, SelectCount = true }));

            Console.ReadLine();
        }
    }
}
