﻿using RoutingProjectNet.src;
using RoutingProjectNet.ViewModels;

namespace RoutingProjectNet;

public partial class MainPage : ContentPage
{
	//int count = 0;

	public MainPage(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	//private void OnCounterClicked(object sender, EventArgs e)
	//{
	//	count++;

	//	if (count == 1)
	//		CounterBtn.Text = $"Clicked {count} time";
	//	else
	//		CounterBtn.Text = $"Clicked {count} times";

	//	SemanticScreenReader.Announce(CounterBtn.Text);
	//}

	//private void GenMap_Clicked(object sender, EventArgs e)
 //   {
	//	StatusLabel.Text = $"Current Status: Generating map.";
	//	RoutingMap map = RoutingMap.GetMapInstance();
	//	map.GenerateMap();
 //   }

  //  private void SolveMap_Clicked(object sender, EventArgs e)
  //  {
  //      StatusLabel.Text = "Current Status: Solving map";
		//StaticRouter router = new();
		//router.OptimizePath();
  //  }
}

