using System;
using Xamarin.Forms;

namespace WorkingWithWebview
{
	public class App : Application  
	{
		public App ()
		{
			var tabs = new TabbedPage ();

			tabs.Children.Add (new ContactPage { Title = "Contact Page"});
            tabs.Children.Add (new AboutPage { Title = "About" });

            MainPage = new NavigationPage(tabs);
		}
	}
}

