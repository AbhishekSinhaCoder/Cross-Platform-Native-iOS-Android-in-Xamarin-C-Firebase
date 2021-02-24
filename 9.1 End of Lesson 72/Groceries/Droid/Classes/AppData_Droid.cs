using System;
using Android.Content;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;

namespace Groceries.Droid
{
    public class AppData_Droid
    {
        private static AppData_Droid instance;
        public static ChildQuery dataNode;
        public static ChildQuery usersNode;
        static FirebaseApp fireApp;
        public static FirebaseAuth auth;

        private AppData_Droid (Context inpContext)
        {
            var options = new Firebase.FirebaseOptions.Builder()
                                      .SetApplicationId("1:295421707649:android:ca7e380966fd1e59")
                                      .SetApiKey("AIzaSyBc33Qe6ANpS81nPsOhBkolmEnAHLRS76k")
                                      .Build();
            
            if (fireApp == null)
                fireApp = FirebaseApp.InitializeApp(inpContext, options);

            auth = FirebaseAuth.GetInstance(fireApp);

            string FirebaseURL = "https://groceries-bde4c.firebaseio.com";

            FirebaseClient rootNode = new FirebaseClient(FirebaseURL);
            dataNode = rootNode.Child("data");
            usersNode = rootNode.Child("users");
        }

        public static AppData_Droid GetInstance(Context inpContext)
        {
            AppData.GetInstance();
            if (instance == null)
                instance = new AppData_Droid(inpContext);

            return instance;
        }
    }
}
