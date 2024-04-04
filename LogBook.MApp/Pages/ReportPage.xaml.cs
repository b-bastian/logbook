using LogBook.Core.ViewModels;
using Microsoft.Maui.Controls;

namespace LogBook.MApp.Pages;

public partial class ReportPage : ContentPage
{
	public ReportPage(ReportViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}
