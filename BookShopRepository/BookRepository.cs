using BookShopRepository.Bases;

namespace BookShopRepository
{
    public static class BookRepository
    {
        private static readonly Book[] _books = new Book[]
                                             {
                                                new Book("Bringer","White and black",100),
                                                new Book("Dostoevsky","Gore ot uma",400),
                                                new Book("Berklin","From stars",300), 
                                                new Book("Rasket","Animals",300), 
                                                new Book("Bringer","Rocket star",200),
                                             };

        public static Book Get(int index)
        {
            return (Book)_books[index].Clone();
        }
    }
}