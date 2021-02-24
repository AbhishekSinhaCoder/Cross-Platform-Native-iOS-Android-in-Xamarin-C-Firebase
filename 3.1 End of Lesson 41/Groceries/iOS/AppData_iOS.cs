using System;
using Firebase.Auth;
using Firebase.Core;
using Firebase.Database;

namespace Groceries.iOS
{
    public class AppData_iOS
    {
        private static AppData_iOS instance;

        public static DatabaseReference DataNode { get; set; }
        public static DatabaseReference UsersNode { get; set; }
        public static Auth auth;

        private AppData_iOS ()  
        {
            App.Configure();

            DataNode = Database.DefaultInstance
                .GetRootReference().GetChild("data");

            UsersNode = Database.DefaultInstance
                .GetRootReference().GetChild("users");
            
            auth = Auth.DefaultInstance;
        }
        public static AppData_iOS GetInstance ()
        {
            AppData.GetInstance();
            if (instance == null)
                instance = new AppData_iOS();

            return instance;
        }

    }
}
