using System.Threading.Tasks;
using UIKit;

namespace 
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
                                           string inpName,
                                          string inpEmail,
                                          string inpPassword)
        {
            bool done = false;




            while (!done)
            {
                await Task.Delay(50);
            }
         
        }
    }
}
