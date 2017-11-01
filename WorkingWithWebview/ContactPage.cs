using System;
using System.Text.RegularExpressions;
using Plugin.Messaging;
using Xamarin.Forms;

namespace WorkingWithWebview
{
    public class ContactPage : ContentPage
    {
        public ContactPage()
        {
            var browser = new WebView();

            browser.Source = DependencyService.Get<IBaseUrl>().Get() + "/contact.html";

            browser.Navigating += async (s, e) =>
            {
                if (e.Url.StartsWith("http"))
                {
                    try
                    {
                        var uri = new Uri(e.Url);
                        Device.OpenUri(uri);
                    }
                    catch (Exception)
                    {
                    }

                    e.Cancel = true;
                }
                else if (e.Url.StartsWith("mailto"))
                {
                    try
                    {
                        //var uri = new Uri(e.Url);
                        //Device.OpenUri(uri);
                        var email = e.Url.Substring(e.Url.IndexOf(":") + 1);
                        if (await DisplayAlert(null,
                                               "Email:\n" + email + "?",
                                               "Yes", "No"))
                        {
                            var emailTask = CrossMessaging.Current.EmailMessenger;
                            if (emailTask.CanSendEmail)
                                emailTask.SendEmail(email, "I need help with my App", "");
                        }
                    }
                    catch (Exception)
                    {
                    }

                    e.Cancel = true;
                }
                else if (e.Url.StartsWith("call"))
                {
                    try
                    {
                        //var uri = new Uri(e.Url);
                        var phoneToCall = e.Url.Substring(e.Url.IndexOf(":") + 1);
                        if (await DisplayAlert(null, $"Call {phoneToCall}",
                            //                   String.Format("Call",phoneToCall
                           // Regex.Replace(phoneToCall, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3")),
                                               "Yes", "No"))
                        {
                            var phoneCallTask = CrossMessaging.Current.PhoneDialer;
                            if (phoneCallTask.CanMakePhoneCall)
                                phoneCallTask.MakePhoneCall(phoneToCall);
                        }

                        //Device.OpenUri(uri);
                    }
                    catch (Exception)
                    {
                    }

                    e.Cancel = true;
                }
                else if (e.Url.StartsWith("feedback"))
                {
                    await App.Current.MainPage.Navigation.PushAsync(new FeedbackPage());
                }

            };


            Content = browser;
        }
    }
}
