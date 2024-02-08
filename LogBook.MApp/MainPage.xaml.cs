using LogBook.MApp.ViewModels;

namespace LogBook.MApp;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}


