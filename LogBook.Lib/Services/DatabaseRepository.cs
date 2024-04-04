using System;
using System.Diagnostics;
using System.Net.Mime;
using LogBook.Lib.Interfaces;
using LogBook.Lib.Models;
using Microsoft.EntityFrameworkCore;

namespace LogBook.Lib.Services;

public class DatabaseRepository : IRepository
{
	string _path = string.Empty;

	public DatabaseRepository(string path)
	{
		this._path = path;
	}

	public bool Add(Entry entry)
	{
		try {
			using (var context = new EntriesContext(this._path)) {
				context.Add(entry);
				context.SaveChanges();
			}

			return true;
		} catch (Exception ex) {
			Debug.WriteLine(ex.Message);

			return false;
		}
	}

	public bool Delete(Entry entry)
	{
		try {
			using (var context = new EntriesContext(this._path)) {
				context.Database.ExecuteSqlRaw("DELETE FROM Entries WHERE Id={0}", entry.Id);
			}

			return true;
		} catch (Exception ex) {
			Debug.WriteLine(ex.Message);

			return false;
		}
	}

	public Entry? Find(string id)
	{
		try {
			using (var context = new EntriesContext(this._path)) {
				var find = (from entry in context.Entries
							where entry.Id == id
							select entry).FirstOrDefault();

				return find;
			}
		} catch (Exception ex) {
			Debug.WriteLine(ex.Message);

			return null;
		}
	}

	public List<Entry> GetAll()
	{
		try {
			using (var context = new EntriesContext(this._path)) {
				/*
				var to = "Zell am See";

				var entries = context.Entries.
								FromSql($"SELECT * FROM Entries WHERE `To`={to}").
								ToList();
				*/

				var entries = context.Entries.
				FromSql($"SELECT * FROM Entries").
				ToList();

				return entries;
			}
		} catch (Exception ex) {
			Debug.WriteLine(ex.Message);

			return new List<Entry>();
		}
	}

	/*
	public List<Entry> GetAll()
	{
		try {
			using (var context = new EntriesContext(this._path)) {
				var entries = (from entry in context.Entries
							  select entry).ToList();

				return entries;
			}
		} catch (Exception ex) {
			Debug.WriteLine(ex.Message);
			return new List<Entry>();
		}
	}
	*/

	public bool Save()
	{
		try {
			using (var context = new EntriesContext(this._path)) {
				context.SaveChanges();
			}

			return true;
		} catch (Exception ex) {
			Debug.WriteLine(ex.Message);

			return false;
		}
	}

	public bool Update(Entry entry)
	{
		try {
			using (var context = new EntriesContext(this._path)) {
				context.Entry(entry).State = EntityState.Modified;

				context.SaveChanges();
			}

			return true;
		} catch (Exception ex) {
			Debug.WriteLine(ex.Message);
			return false;
		}
	}
}

