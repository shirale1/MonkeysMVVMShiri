﻿using MonkeysMVVM.Views;

namespace MonkeysMVVM;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("FindMonkeyByLocation", typeof(FindMonkeyByLocationPage));
		Routing.RegisterRoute("MonkeysPage", typeof(MonkeysPage));
		Routing.RegisterRoute("ShowMonkey", typeof (ShowMonkeyView));
	}
}
