using System;
using Android.Content;

namespace Groceries.Droid
{
    public static class Logout
    {
        public static async void Out (Context inpContext)
        {
            AppData_Droid.auth.SignOut();

            AlertShow.Show(inpContext, 
                           "Logged Out", 
                          "You are now offline and you can still work on your own");

            await ((ListsActivity)inpContext).ReloadData();
        }
    }
}
