using Xamarin.Forms;

namespace WorkingWithWebview
{
    public class ContactPage : ContentPage
    {
        public delegate void SharedNavigationHandler(object sender, WebNavigatingEventArgs e);

        public ContactPage()
        {
            var browser = new WebView { Source = DependencyService.Get<IBaseUrl>().Get() + "/contact.html"  };

            browser.Navigating += (s, e) => WebViewNavigationDelegate.OnNavigating(this, s, e);

            Content = browser;
        }
    }
}