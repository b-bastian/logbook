﻿using System;
using System.Collections.Generic;
using LogBook.Lib.Interfaces;
using LogBook.Lib.Models;

namespace LogBook.Lib.Services
{
	public class MemoryRepository : IRepository
	{
		List<Entry> entries = new List<Entry>();

		public bool Add(Entry entry)
		{
			this.entries.Add(entry);
			return true;
		}

		public bool Delete(Entry entry)
		{
			return this.entries.Remove(entry);
		}

		public List<Entry> GetAll()
		{
			return this.entries;
		}

		public bool Save()
		{
			return true;
		}

		public bool Update(Entry entry)
		{
			var item = (from search in this.entries
					   where search.Id == search.Id
					   select search).FirstOrDefault();

			if (item != null) {
				item = entry;
				return true;
			}

			return false;
		}
	}
}

