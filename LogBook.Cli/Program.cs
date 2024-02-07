using LogBook.Lib.Interfaces;
using LogBook.Lib.Services;
using LogBook.Lib.Models;

Console.WriteLine("Willkommen im Fahrtenbuch!");

string path = "logbook.xml";

IRepository repository = new XmlRepository(path);
List<Entry> entries = repository.GetAll();

repository.Add(new Entry(DateTime.Now, DateTime.Now.AddHours(2), 25000, 25180, "ZE-XY123", "Zell am See", "München"));

Entry entrySaalfelden = new Entry(DateTime.Now.AddDays(3), DateTime.Now.AddDays(3).AddMinutes(20), 25500, 25514, "ZE-XY123", "Zell am See", "Saalfelden");

repository.Add(entrySaalfelden);

foreach (var item in entries)
{
	Console.WriteLine(item);
}