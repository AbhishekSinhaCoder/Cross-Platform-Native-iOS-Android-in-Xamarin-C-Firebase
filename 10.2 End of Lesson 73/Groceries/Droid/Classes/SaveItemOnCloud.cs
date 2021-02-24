using System;
using Firebase.Database.Query;

namespace Groceries.Droid
{
    public static class SaveItemOnCloud
    {
        public static void Save (ItemClass inpItem,
                                 GroceryListClass inpList)
        {
            if (AppData_Droid.auth.CurrentUser == null)
                return;

            AppData_Droid.dataNode
                         .Child(inpList.Owner.Uid)
                         .Child(inpList.Name)
                         .Child("Items")
                         .Child(inpItem.Name)
                         .PutAsync(inpItem);
        }
    }
}
