using Xamarin.Forms;

namespace WorkingWithWebview
{
    public interface IBaseUrl { string Get(); }

    public class AboutPage: ContentPage
    {
        public AboutPage()
        {
            var browser = new WebView {Source = DependencyService.Get<IBaseUrl>().Get() + "/about.html"};

            browser.Navigating += (s, e) => WebViewNavigationDelegate.OnNavigating(this, s, e);

            Content = browser;
        }
    }
}
