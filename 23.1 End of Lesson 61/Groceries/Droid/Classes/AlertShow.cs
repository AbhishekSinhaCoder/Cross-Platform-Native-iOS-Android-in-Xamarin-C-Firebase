using System;
using Android.App;
using Android.Content;

namespace Groceries.Droid
{
    public static class AlertShow
    {
        public static void Show (Context thisContext, 
                                string title, 
                                string message)
        {
            AlertDialog.Builder alert;
            alert = new AlertDialog.Builder(thisContext);
            alert.SetTitle(title);
            alert.SetMessage(message);

            alert.SetPositiveButton("Ok", (sender, e) => {});
            Dialog dialog = alert.Create();
            dialog.Show();
        }
    }
}
