using System;
using CsvHelper;
using System.Globalization;
using LogBook.Lib.Interfaces;
using LogBook.Lib.Models;
using System.Reflection.Metadata.Ecma335;
using System.Diagnostics;
using System.IO;

namespace LogBook.Lib.Services
{
	public class CsvRepository : IRepository
	{
		/* title, money
		 * Test, 23.22
		 * Probe 14.11
		*/
		
		private string _path = string.Empty;
		readonly List<Entry> list = new();

		public CsvRepository(string path)
		{
			this._path = path;

			if (File.Exists(this._path)) {
				using (var reader = new StreamReader(path))
				using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) {
					csv.Context.RegisterClassMap<EntryMap>();
					csv.Read();
					csv.ReadHeader();

					var records = csv.GetRecords<Entry>();
					this.list = records.ToList();
				}
			}
		}

		public bool Add(Entry entry)
		{
			this.list.Add(entry);
			return this.Save();
		}

		public bool Delete(Entry entry)
		{
			var item = this.list.FirstOrDefault((item) => item.Id == entry.Id);

			if(item != null) {
				this.list.Remove(item);
			}

			return this.Save();
		}

		public Entry? Find(string id)
		{
			return this.list.FirstOrDefault((item) => item.Id == id);
		}

		public List<Entry> GetAll()
		{
			return this.list;
		}

		public bool Save()
		{
			try {
				using (var writer = new StreamWriter(this._path))
				using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture)) {
					csv.Context.RegisterClassMap<EntryMap>();

					csv.WriteHeader<Entry>();
					csv.NextRecord();

					foreach (var record in this.list) {
						csv.WriteRecord(record);
						csv.NextRecord();
					}
				}

				return true;
			} catch (Exception ex) {
				Debug.WriteLine(ex.Message);
				return false;
			}
		}

		public bool Update(Entry entry)
		{
			var item = (from search in this.list
						where search.Id == entry.Id
						select search).First();

			if(item != null) {
				item = entry;
			}

			return this.Save();
		}
	}
}

