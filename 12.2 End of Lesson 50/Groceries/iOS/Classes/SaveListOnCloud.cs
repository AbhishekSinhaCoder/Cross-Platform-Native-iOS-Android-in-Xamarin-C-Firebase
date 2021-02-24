using System;
using Foundation;

namespace Groceries.iOS
{
    public static class SaveListOnCloud
    {
        public static void Save(GroceryListClass inpList)
        {
            if (AppData_iOS.auth.CurrentUser == null)
                return;

            NSDictionary toWriteDict = ListToDict(inpList);

            AppData_iOS.DataNode.GetChild(AppData.curUser.Uid)
                       .GetChild(inpList.Name)
                   .SetValue<NSDictionary>(toWriteDict);
        }

        static NSDictionary ListToDict(GroceryListClass inpList)
        {
            var allItemsDict = new NSMutableDictionary();
            foreach (ItemClass item in inpList.Items)
            {
                NSMutableDictionary eachItemDict = new NSMutableDictionary();

                eachItemDict.SetValueForKey((NSString)item.Name, (NSString)"Name");
                eachItemDict.SetValueForKey((NSString)item.Purchased, (NSString)"Purchased");
                eachItemDict.SetValueForKey((NSString)(item.Time), (NSString)"Time");
                allItemsDict.SetValueForKey(eachItemDict, (NSString)item.Name);
            }

            object[] keys = { "Name", "Email", "Uid" };
            object[] valus = { inpList.Owner.Name, inpList.Owner.Email, inpList.Owner.Uid };
            var listOwnerDict = NSDictionary.FromObjectsAndKeys(valus, keys);


            NSMutableDictionary anyListDataDict = new NSMutableDictionary();

            anyListDataDict.SetValueForKey(allItemsDict, (NSString)"Items");
            anyListDataDict.SetValueForKey((NSString)inpList.Name, (NSString)"Name");
            anyListDataDict.SetValueForKey(listOwnerDict, (NSString)"Owner");


            if (allItemsDict.Count == 0)
                anyListDataDict.Remove((NSString)"Items");

            return anyListDataDict;
        }
    }
}
