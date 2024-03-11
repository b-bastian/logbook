using Microsoft.Extensions.Logging;
using LogBook.Lib.Services;
using LogBook.Lib.Interfaces;
using Debug = System.Diagnostics.Debug;
using CommunityToolkit.Maui;
using LogBook.Core.ViewModels;
using LogBook.MApp.Pages;

namespace LogBook.MApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// add mainviewmodel & mainpage
		builder.Services.AddSingleton<MainViewModel>();
		builder.Services.AddSingleton<MainPage>();

		// add reportpage & reportviewmodel
		builder.Services.AddSingleton<ReportPage>();
		builder.Services.AddSingleton<ReportViewModel>();

		// add appshell
		builder.Services.AddSingleton<AppShell>();

		// add irepository & path for the xmlrepository
		string path = FileSystem.Current.AppDataDirectory;
		string filename = "data.xml";

		string fullpath = System.IO.Path.Combine(path, filename);

		Debug.WriteLine($"AppDataDirectory: {fullpath}");

		builder.Services.AddSingleton<IRepository>(new XmlRepository(fullpath));

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

