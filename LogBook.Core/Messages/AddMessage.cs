using System;
using CommunityToolkit.Mvvm.Messaging.Messages;
using LogBook.Lib.Models;

namespace LogBook.Core.Messages;

public class AddMessage : ValueChangedMessage<Entry>
{
	public AddMessage(Entry value) : base(value)
	{
	}
}

