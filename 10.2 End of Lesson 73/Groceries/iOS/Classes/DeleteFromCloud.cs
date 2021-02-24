using System;
using Firebase.Database;


namespace Groceries.iOS
{
    public static class DeleteFromCloud
    {
        public static void DeleteList(GroceryListClass inpList)
        {
            if (AppData_iOS.auth.CurrentUser == null)
                return;

            DatabaseReference listNode = AppData_iOS.DataNode
                                                .GetChild(inpList.Owner.Uid)
                                                .GetChild(inpList.Name);

            listNode.RemoveValue();
        }


        public static void DeleteItem(ItemClass inpItem,
                                      GroceryListClass inpList)
        {
            if (AppData_iOS.auth.CurrentUser == null)
                return;

            DatabaseReference itemNode = AppData_iOS.DataNode
                                                .GetChild(inpList.Owner.Uid)
                                                .GetChild(inpList.Name)
                                                .GetChild("Items")
                                                .GetChild(inpItem.Name);
            itemNode.RemoveValue();
        }
    }
}
