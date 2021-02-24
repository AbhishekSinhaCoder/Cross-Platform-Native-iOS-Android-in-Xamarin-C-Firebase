using System;
using System.Collections.Generic;

namespace Groceries
{
    public class AppData
    {
        public static UserClass curUser;
        public static List<GroceryListClass> currentLists;

        public static List<GroceryListClass> offlineLists;

        public static List<GroceryListClass> onlineLists;

        public static List<GroceryListClass> invitationsLists;

        public static List<InvitationClass> invitationsData;



        private static AppData instance;


        public static AppData GetInstance ()
        {
            if (instance == null)
                instance = new AppData();

            return instance;
        }

    }
}
