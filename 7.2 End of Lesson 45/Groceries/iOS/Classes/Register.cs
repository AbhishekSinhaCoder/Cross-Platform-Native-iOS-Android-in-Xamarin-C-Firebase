using System.Threading.Tasks;
using Firebase.Auth;
using Foundation;
using UIKit;

namespace Groceries.iOS
{
    public static class Register
    {
        public static void Alert (UIViewController inpView)
        {
            UIAlertController regAlert;
            regAlert = UIAlertController.Create("Register",
                                               "Please enter name, email and password",
                                                UIAlertControllerStyle.Alert);

            regAlert.AddTextField((textFld) => {
                textFld.Placeholder = "name";
                textFld.TextAlignment = UITextAlignment.Center;
                textFld.Font = UIFont.SystemFontOfSize(22);
                textFld.KeyboardType = UIKeyboardType.Default;

            });

            regAlert.AddTextField((textFld) => {
                textFld.Placeholder = "email";
                textFld.TextAlignment = UITextAlignment.Center;
                textFld.Font = UIFont.SystemFontOfSize(22);
                textFld.KeyboardType = UIKeyboardType.EmailAddress;
            });

            regAlert.AddTextField((textFld) => {
                textFld.Placeholder = "password";
                textFld.KeyboardType = UIKeyboardType.Default;
                textFld.SecureTextEntry = true;
                textFld.TextAlignment = UITextAlignment.Center;
                textFld.Font = UIFont.SystemFontOfSize(22);
            });

            UIAlertAction register;
            register = UIAlertAction.Create("Register",
                                            UIAlertActionStyle.Default,
                                            async (obj) => await RegisterAction(inpView,
                                                                          regAlert.TextFields[0].Text,
                                                                          regAlert.TextFields[1].Text,
                                                                          regAlert.TextFields[2].Text));
            regAlert.AddAction(register);
            regAlert.AddAction(UIAlertAction.Create("Cancel", 
                                                    UIAlertActionStyle.Cancel, 
                                                    null));
            
            inpView.PresentViewController(regAlert, true, null);
        }



        public static async Task RegisterAction(UIViewController inpView,
                                           string inpName, string inpEmail, string inpPassword)
        {
            bool done = false;
            //
            AppData_iOS.auth.CreateUser(inpEmail, inpPassword, (user, error) =>
             {
                if ( error == null)
                {
                     UserProfileChangeRequest changReq = user.ProfileChangeRequest();
                     changReq.DisplayName = inpName; 
                    changReq.CommitChanges(  (changeError) => 
                    {
                        if ( changeError == null)
                        {
                            UserClass localUser = new UserClass()
                            {
                                Name = user.DisplayName,
                                Email = user.Email,
                                Uid = user.Uid
                            };
                            // Set Our Local User

                            object[] keys = { "Name", "Email", "Uid" };
                            object[] vals = { user.DisplayName, user.Email, user.Uid };
                            var userDict = NSDictionary.FromObjectsAndKeys(vals, keys);
                            AppData_iOS.UsersNode.GetChild(user.Uid).SetValue<NSDictionary>(userDict);

                            // write user's lists online

                            AlertShow.Show(inpView, 
                                           "Success", 
                                           "You are now online and can save lists to cloud");
                            done = true;
                        }
                    });
                }
             });
            // 
            while (!done)
            {
                await Task.Delay(50);
            }
        }
    }
}
