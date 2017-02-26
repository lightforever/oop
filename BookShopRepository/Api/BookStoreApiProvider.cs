using BookShopRepository.Bases;

namespace BookShopRepository.Api
{
    public static class BookStoreApiProvider
    {
        private static readonly BookStoreApi _current;

        static BookStoreApiProvider()
        {
            var firstBookStore = new BookStore();
            Book[] firstStoreBooks = new Book[]{BookRepository.Get(0),BookRepository.Get(3),BookRepository.Get(2),BookRepository.Get(4)};
            SetBookStoreParent(firstBookStore,firstStoreBooks);

            var secondBookStore = new BookStore();
            Book[] secondStoreBooks = new Book[]{BookRepository.Get(0),BookRepository.Get(0),BookRepository.Get(3),BookRepository.Get(1)};
            SetBookStoreParent(secondBookStore, secondStoreBooks);

            var thirdBookStore = new BookStore();
            Book[] thirdStoreBooks = new Book[]{BookRepository.Get(3),BookRepository.Get(3),BookRepository.Get(2)};
            SetBookStoreParent(thirdBookStore, thirdStoreBooks);

            _current = new BookStoreApi(new BookStore[]{firstBookStore,secondBookStore,thirdBookStore });
        }

        private static void SetBookStoreParent(BookStore store,Book[] books)
        {
            foreach (var book in books)
            {
                book.SetParent(store);
                book.Lock();
            }

            store.SetBooks(books);
            store.Lock();
        }

        public static IBookStoreApi Current
        {
            get { return _current; }
        }
    }
}