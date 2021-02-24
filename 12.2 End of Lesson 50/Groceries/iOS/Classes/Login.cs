using System;
using System.Threading.Tasks;
using UIKit;

namespace Groceries.iOS
{
    public static class Login
    {
        public static void Alert(UIViewController inpView)
        {
            UIAlertController alert;
            alert = UIAlertController.Create("Login Online",
                                             "Please enter your email and password",
                                             UIAlertControllerStyle.Alert);

            alert.AddTextField((field) =>
            {
                field.Font = UIFont.SystemFontOfSize(22);
                field.Placeholder = "Email";
                field.KeyboardType = UIKeyboardType.EmailAddress;
                field.TextAlignment = UITextAlignment.Center;
            });

            alert.AddTextField((field) =>
            {
                field.TextAlignment = UITextAlignment.Center;
                field.Font = UIFont.SystemFontOfSize(22);
                field.Placeholder = "Password";
                field.SecureTextEntry = true;
            });


            UIAlertAction loginAction;
            loginAction = UIAlertAction.Create("Login",
                  UIAlertActionStyle.Default,
                                               async (UIAlertAction obj) => await 
                                               LoginAction(inpView, 
                                                     alert.TextFields[0].Text,
                                                     alert.TextFields[1].Text));

            alert.AddAction(loginAction);

            alert.AddAction(UIAlertAction.Create("Cancel", 
                                                 UIAlertActionStyle.Destructive, 
                                                 null));

            inpView.PresentViewController(alert, true, null);
        }


        public static async Task LoginAction(UIViewController thisView, 
                                       string inpEmail, string inpPassword)
        {
            bool done = false;
            AppData_iOS.auth.SignIn(inpEmail, inpPassword,
                                     async (user, error) => 
            {
                if (error == null)
                {
                    UserClass newUser = new UserClass()
                    {
                        Name = user.DisplayName,
                        Email = user.Email,
                        Uid = user.Uid
                    };
                    SetLocalUser.Set(newUser);
                    AlertShow.Show(thisView, 
                                   "Success", 
                                   "You are now logged in and can use the cloud");
                    await ((ListsViewController)thisView).ReloadData();
                    done = true;
                }
            });




            while (!done)
            {
                await Task.Delay(50);
            }
        }
    }
}
