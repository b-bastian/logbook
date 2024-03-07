using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using LogBook.Lib.Interfaces;
using System.Collections.ObjectModel;
using Entry = LogBook.Lib.Models.Entry;
using DateTime = System.DateTime;

namespace LogBook.Core.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public string Header => "Fahrtenbuch";

    IRepository _repository;

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

	public MainViewModel(IRepository repository)
    {
        this._repository = repository;
    }

    [RelayCommand]
    void LoadData()
    {
        var entries = this._repository.GetAll();

        foreach (var entry in entries) {
            Entries.Add(entry);
        }
    }

    private bool CanAdd => this.Description.Length > 0;

    [RelayCommand(CanExecute = nameof(CanAdd))]
    void Add()  
    {
        Entry entry = new Entry(this.Start, this.Ende, this.Startkm, this.Endkm, this.Numberplate, this.From, this.To);

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
		}
	}
}

