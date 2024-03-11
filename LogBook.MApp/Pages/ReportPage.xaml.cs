using LogBook.Core.ViewModels;

namespace LogBook.MApp.Pages;

public partial class ReportPage : ContentPage
{
	public ReportPage(ReportViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}
