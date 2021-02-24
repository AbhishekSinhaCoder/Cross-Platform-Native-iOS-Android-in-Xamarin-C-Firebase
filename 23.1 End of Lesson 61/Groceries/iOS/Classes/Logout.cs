using System;
using Foundation;
using UIKit;

namespace Groceries.iOS
{
    public static class Logout
    {
        public static async void LogoutAction(UIViewController inpView)
        {
            NSError error;
            bool signedOut = AppData_iOS.auth.SignOut(out error);

            if ( signedOut )
            {
                AlertShow.Show(inpView,
                               "Success", 
                               "You are now offline and you can work on your won disk");

                await ((ListsViewController)inpView).ReloadData();
            }
        }
    }
}
