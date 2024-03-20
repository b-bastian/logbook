using LogBook.Lib.Interfaces;
using LogBook.Lib.Services;
using LogBook.Lib.Models;
using System;
using Entry = LogBook.Lib.Models.Entry;
using System.Collections.Generic;

Console.WriteLine("Willkommen im Fahrtenbuch!");

string path = "logbook.xml";

IRepository repository = new XmlRepository(path);

repository.Add(new Entry(DateTime.Now, DateTime.Now.AddHours(2), 25000, 25180, "ZE-XY123", "Zell am See", "München", false));

Entry entrySaalfelden = new Entry(DateTime.Now.AddDays(3), DateTime.Now.AddDays(3).AddMinutes(20), 25500, 25514, "ZE-XY123", "Zell am See", "Saalfelden", false)
{
	Description = "Fahrt nach Saalfelden"
};

repository.Add(entrySaalfelden);

List<Entry> entries = repository.GetAll();

foreach (var item in entries)
{
	Console.WriteLine(item);
	Console.WriteLine(item.Description + "\n");
}