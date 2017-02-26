###Задача по паттернам проектировани€/ќќѕ/чистоте кода###
Реализация: Visual Studio 2013, C#
------

ѕусть у нас имеетс€ база книжных магазинов заданна€ массивом ссылок:

IObjectReference[] bookStores

Ќеобходимо продумать API (набор классов, методов) дл€ выполнени€ стандартных запросов к этой "базе данных" - например, поиск книг с заданным названием или автором, поиск книг, толщина которых находитс€ в требуемом диапазоне; вычисление общего количества страниц, написанных одним автором и т. п. ќсновное внимание необходимо уделить вопросам удобства и безопасности использовани€ предложенного решени€ клиентским кодом. 




API Reference

	public interface IObjectReference
	{
		IObjectReference Parent { get; } // will return null if there is no parent object in hierarchy
		
		// will return null if property is not found or if property value is an inferior object(s) of type IObjectReference
		object GetValueOfProperty(
			string propertyName); // will throw if null or empty
	}
	
	public static class BookStoreUtils
	{
		// will return null if property is not found
		public static IObjectReference[] GetInferiorObjects(
			IObjectReference propertyOwner, // will throw if null
			string propertyName) // will throw if null or empty
	}


Objects Hierarchy:

	BookStore : IObjectReference
	
		Has access to collections of Books objects
		
		Properties:
		- IObjectReference[] Books
	
	
	
	Book : IObjectReference
	
		Represents particular book. Returns BookStore reference as it's parent.
		
		Properties:
		- string Author
		- string Title
		- int NumberOfPages


UsageExamples:

	// given IObjectReference to book object (book)
	string author = (string)book.GetValueOfProperty("Author");
	
	// given IObjectReference to book store object (store)
	IObjectReference firstBook = BookStoreUtils.GetInferiorObjects(store, "Books")[0];
	int firstBookPagesCount = (int)firstBook.GetValueOfProperty("NumberOfPages");