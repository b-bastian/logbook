using System;
using LogBook.Core.Services;

namespace LogBook.MApp.Services
{
	public class AlertService : IAlertService
	{
		public async void ShowAlert(string title, string message)
		{
			Application.Current.MainPage.Dispatcher.Dispatch(async() => {
				await ShowAlertAsync(title, message);
			});
		}

		public Task ShowAlertAsync(string title, string message)
		{
			return Application.Current.MainPage.DisplayAlert(title, message, "OK");
		}
	}
}

