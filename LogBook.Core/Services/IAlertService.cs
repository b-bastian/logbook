using System;
namespace LogBook.Core.Services;

public interface IAlertService
{
	void ShowAlert(string title, string message);

	Task ShowAlertAsync(string title, string message);
}

