using System;
using Firebase.Database.Query;

namespace Groceries.Droid
{
    public static class DeleteFromCloud
    {
        public static void DeleteList (GroceryListClass inpList)
        {
            if (AppData_Droid.auth.CurrentUser == null)
                return;

            AppData_Droid.dataNode
                         .Child(inpList.Owner.Uid).Child(inpList.Name)
                         .DeleteAsync();

        }

        public static void DeleteItem (ItemClass inpItem, 
                                       GroceryListClass inpList)
        {
            if (AppData_Droid.auth.CurrentUser == null)
                return;

            AppData_Droid.dataNode
                         .Child(inpList.Owner.Uid).Child(inpList.Name)
                         .Child("Items").Child(inpItem.Name)
                         .DeleteAsync();

        }
    }
}
