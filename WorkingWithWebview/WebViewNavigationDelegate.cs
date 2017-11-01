using System;
using System.Diagnostics;
using Plugin.Messaging;
using Xamarin.Forms;

namespace WorkingWithWebview
{
    public class WebViewNavigationDelegate
    {
        public static async void OnNavigating(Page page, object sender, WebNavigatingEventArgs e)
        {
            e.Cancel = true;
            try
            {
                if (e.Url.StartsWith("http") || e.Url.StartsWith("https"))
                {
                    var uri = new Uri(e.Url);
                    Device.OpenUri(uri);
                }
                else if (e.Url.StartsWith("mailto"))
                {
                    var email = e.Url.Substring(e.Url.IndexOf(":") + 1);
                    if (await page.DisplayAlert(null,
                        "Email:\n" + email + "?",
                        "Yes", "No"))
                    {
                        var emailTask = CrossMessaging.Current.EmailMessenger;
                        if (emailTask.CanSendEmail)
                            emailTask.SendEmail(email, "I need help with my App", "");
                    }
                }
                else if (e.Url.StartsWith("call") || e.Url.StartsWith("tel"))
                {
                    var phoneToCall = e.Url.Substring(e.Url.IndexOf(":") + 1);
                    if (await page.DisplayAlert(null, $"Call {phoneToCall}",
                        "Yes", "No"))
                    {
                        var phoneCallTask = CrossMessaging.Current.PhoneDialer;
                        if (phoneCallTask.CanMakePhoneCall)
                            phoneCallTask.MakePhoneCall(phoneToCall);
                    }
                }
                else if (e.Url.StartsWith("feedback"))
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new FeedbackPage());
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }
        }
    }
}