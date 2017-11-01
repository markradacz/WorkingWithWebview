using System;

using Xamarin.Forms;

namespace WorkingWithWebview
{
    public class FeedbackPage : ContentPage
    {
        public FeedbackPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello FeedbackPage" }
                }
            };
        }
    }
}

