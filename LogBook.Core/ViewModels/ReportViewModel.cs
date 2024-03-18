using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LogBook.Core.Messages;
using LogBook.Lib.Interfaces;
using LogBook.Lib.Models;

namespace LogBook.Core.ViewModels
{
	public partial class ReportViewModel : ObservableObject
	{
		IRepository _repository;

		[ObservableProperty]
		ObservableCollection<Entry> _entries = new();

		private bool _isLoaded = false;

		public ReportViewModel(IRepository repository)
		{
			this._repository = repository;

			WeakReferenceMessenger.Default.Register<AddMessage>(this, (r, m) => {
				// m.Value = unser Entry-Objekt
				Debug.WriteLine(m.Value);

				// add to list
				this.Entries.Add(m.Value);
			});
		}

		[RelayCommand]
		void LoadData()
		{
			// Performance leidet darunter
			// this.Entries.Clear();

			if (!this._isLoaded) {
				var entries = this._repository.GetAll();

				foreach (var entry in entries) {
					Entries.Add(entry);
				}

				this._isLoaded = true;
			}
		}
	}
}

