using LogBook.Lib.Interfaces;
using LogBook.Lib.Models;
using LogBook.Lib.Services;

Console.WriteLine("Willkommen im Fahrtenbuch!");

IRepository repository = new MemoryRepository();
List<Entry> entries = repository.GetAll();

foreach (var item in entries) {
	Console.WriteLine(item.From);
	Console.WriteLine(item.To);
	Console.WriteLine("\n");
}