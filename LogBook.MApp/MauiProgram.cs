﻿using Microsoft.Extensions.Logging;
using LogBook.Lib.Services;
using LogBook.Lib.Interfaces;
using Debug = System.Diagnostics.Debug;
using CommunityToolkit.Maui;
using LogBook.Core.ViewModels;
using LogBook.MApp.Pages;
using LogBook.Core.Services;
using LogBook.MApp.Services;
using Syncfusion.Maui.Core.Hosting;

namespace LogBook.MApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureSyncfusionCore()
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

		// add ialertservice
		builder.Services.AddSingleton<IAlertService, AlertService>();

		// add syncfusion license
		Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NAaF1cVGhNYVJ1WmFZfVpgd19EY1ZTQWYuP1ZhSXxXdkZiUX9YdHZRR2leVkc=");

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

