using System.Collections.Generic;
using Firebase.Database.Query;

namespace Groceries.Droid
{
    public static class SaveListOnCloud
    {
        public static void Save(GroceryListClass inpList)
        {
            if (AppData_Droid.auth.CurrentUser == null)
                return;

            var allItemsDict = new Dictionary<string, object> { };

            foreach (ItemClass item in inpList.Items)
                allItemsDict.Add(item.Name, item);

            var userMainData = new Dictionary<string, object>
            {
                { "Items", allItemsDict },
                { "Name", inpList.Name },
                { "Owner", inpList.Owner }
            };

            if (allItemsDict.Count == 0)
                userMainData.Remove("Items");

            AppData_Droid.dataNode
                         .Child(AppData.curUser.Uid)
                         .Child(inpList.Name)
                         .PutAsync(userMainData);
        }
    }
}

