using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using LogBook.Lib.Interfaces;
using System.Collections.ObjectModel;
using Entry = LogBook.Lib.Models.Entry;
using DateTime = System.DateTime;
using LogBook.Core.Services;
using CommunityToolkit.Mvvm.Messaging;
using LogBook.Core.Messages;

namespace LogBook.Core.ViewModels;
// primärer Konstruktor
public partial class MainViewModel /*(IRepository repositroy, IAlertService alertService)*/ : ObservableObject
{
    public string Header => "Fahrtenbuch";

    // primärer Konstruktor
	// IRepository _repository = repository;
	// IAlertServce _alertService = alertService;

	IRepository _repository;
	IAlertService _alertService;

    private bool _isLoaded = false;

	[ObservableProperty]
    ObservableCollection<Entry> _entries = new();

    [ObservableProperty]
    Entry _selectedEntry = null;

    #region Properties

    [ObservableProperty]
    DateTime _start = DateTime.Now;

	[ObservableProperty]
	DateTime _ende = DateTime.Now;

	[ObservableProperty]
	[NotifyCanExecuteChangedFor(nameof(AddCommand))]
	string _description = string.Empty;

	[ObservableProperty]
	string _numberplate = string.Empty;

    [ObservableProperty]
    int _startkm = 0;

    [ObservableProperty]
    int _endkm = 0;

	[ObservableProperty]
	string _from = string.Empty;

	[ObservableProperty]
	string _to = string.Empty;

	#endregion

	[RelayCommand]
	void ToggleFavorite(Entry entry)
	{
		entry.Favorite = !entry.Favorite;

		var result = this._repository.Update(entry);
		
		if(result) {
			int pos = this.Entries.IndexOf(entry);

			if(pos != -1) {
				this.Entries[pos] = entry;

				this._alertService.ShowAlert("Erfolg", "Der Status wurde geändert!");
			} else {
				this._alertService.ShowAlert("Fehler", "Der Status konnte nicht geändert werden!");
			}
		}
	}

	public MainViewModel(IRepository repository, IAlertService alertService)
    {
        this._repository = repository;
        this._alertService = alertService;
    }

    [RelayCommand]
    void LoadData()
    {
        // Performance leidet darunter
        // this.Entries.Clear();

        if(!this._isLoaded) {
			var entries = this._repository.GetAll();

			foreach (var entry in entries) {
				Entries.Add(entry);
			}

			this._isLoaded = true;
		}
    }

    private bool CanAdd => this.Description.Length > 0;

    [RelayCommand(CanExecute = nameof(CanAdd))]
    void Add()  
    {
        Entry entry = new Entry(this.Start, this.Ende, this.Startkm, this.Endkm, this.Numberplate, this.From, this.To, false);

        if(this.Description.Trim() != string.Empty &&
            this.Description.Length > 0) {
            entry.Description = this.Description;
        }
      
        var result = this._repository.Add(entry);

        if (result) {
			this.Entries.Add(entry);

            this.Description = string.Empty;
            this.From = string.Empty;
            this.To = string.Empty;
            this.Startkm = Endkm;
            this.Endkm = 0;

			WeakReferenceMessenger.Default.Send(new AddMessage(entry));
		}
	}

	[RelayCommand]
	void Delete(Entry entry)
	{
		Entry entryToDelete = this._repository.Find(entry.Id);

		if (entryToDelete != null) {
			var res = this._repository.Delete(entryToDelete);

			if (res) {
				this.SelectedEntry = null;
				this.Entries.Remove(entry);

				this._alertService.ShowAlert("Erfolgreich", "Der Eintrag wurde gelöscht.");
			} else {
                // alert not possible to delete from
                this._alertService.ShowAlert("Fehler", "Der Eintrag konnte nicht gelöscht werden.");
			}
		} else {
			// alert entry not found
			this._alertService.ShowAlert("Fehler", "Der Eintrag konnte nicht gefunden werden.");

		}
	}
}

