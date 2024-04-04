using LogBook.Core.ViewModels;
using Microsoft.Maui.Controls;

namespace LogBook.MApp;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}


