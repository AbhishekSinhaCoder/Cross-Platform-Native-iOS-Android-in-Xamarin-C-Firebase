using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace 
{
    public static class Login
    {
        public static void Alert(Context inpContext)
        {
            AlertDialog.Builder loginAlert = new AlertDialog.Builder(inpContext);
            loginAlert.SetTitle("Login Online");
            loginAlert.SetMessage("Please enter your email and password.");

            LinearLayout textEditsLayout = new LinearLayout(inpContext);
            textEditsLayout.Orientation = Orientation.Vertical;

            EditText emailInput = new EditText(inpContext);
            emailInput.TextSize = 22;
            emailInput.Gravity = GravityFlags.Center;
            emailInput.Hint = "Email";
            emailInput.InputType = Android.Text.InputTypes.TextVariationEmailAddress;
            emailInput.SetSingleLine(true);
            textEditsLayout.AddView(emailInput);

            EditText passwordInput = new EditText(inpContext);
            passwordInput.TextSize = 22;
            passwordInput.Gravity = GravityFlags.Center;
            passwordInput.InputType = Android.Text.InputTypes.NumberVariationPassword;
            passwordInput.Hint = "Password";
            passwordInput.InputType = Android.Text.InputTypes.TextVariationPassword;
            passwordInput.SetSingleLine(true);
            textEditsLayout.AddView(passwordInput);

            loginAlert.SetView(textEditsLayout);
            loginAlert.SetPositiveButton("Login",
                                         async (sender,
                                                e) => await LoginAction((ListsActivity)inpContext,
                                                                   emailInput.Text,
                                                                   passwordInput.Text));
            loginAlert.SetNegativeButton("Cancel", (senderAlert, args) => { });
            Dialog dialog = loginAlert.Create();
            dialog.Show();
        }


        public static async Task LoginAction(ListsActivity thisActivity, 
                                       string inpEmail, 
                                       string inpPassword)
        {
            await AppData_Droid.auth.SignInWithEmailAndPasswordAsync(inpEmail, 
                                                                     inpPassword);

            UserClass newLocalUser = new UserClass
            {
                Name = AppData_Droid.auth.CurrentUser.DisplayName,
                Uid = AppData_Droid.auth.CurrentUser.Uid,
                Email = AppData_Droid.auth.CurrentUser.Email
            };

            SetLocalUser.Set(newLocalUser);

            AlertShow.Show(thisActivity,
                           "Login Was Successful",
                           "Welcome back " + newLocalUser.Name + ". You can now share your lists with your friends.");


        }
    }
}
