using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Foundation;

namespace Groceries.iOS
{
    public static class ReadOnlineData
    {
        public static async Task Read()
        {
            AppData.onlineLists = new List<GroceryListClass>();

            if (AppData_iOS.auth.CurrentUser == null)
                return;

            bool done = false;

            AppData_iOS.DataNode
                   .GetChild(AppData.curUser.Uid)
                   .ObserveSingleEvent(DataEventType.Value, (snapshot) =>
            {
                if (!snapshot.HasChildren)
                    goto ThisIsDone;

                var allListsData = snapshot.GetValue<NSDictionary>();

                if (allListsData.Count == 0)
                    goto ThisIsDone;
                
                if (!(allListsData.IsKindOfClass(new ObjCRuntime.Class(typeof(NSDictionary)))))
                    goto ThisIsDone;



                foreach (NSDictionary eachListAllVals in allListsData.Values)
                {
                    NSString listName = (NSString)eachListAllVals.ValueForKey((NSString)"Name");

                    List<ItemClass> itemsInList = new List<ItemClass>();

                    if (eachListAllVals.ValueForKey((NSString)"Items") != null)
                    {
                        if ((eachListAllVals.ValueForKey((NSString)"Items")).IsKindOfClass(new ObjCRuntime.Class(typeof(NSDictionary))))
                        {
                            NSDictionary itemsOfListVals = (NSDictionary)NSObject.FromObject(eachListAllVals.ValueForKey((NSString)"Items"));

                            for (int j = 0; j < (int)itemsOfListVals.Values.Length; j++)
                            {
                                NSDictionary eachItemVals = (NSDictionary)NSObject.FromObject(itemsOfListVals.Values[j]);
                                var fetchedItemName = (NSString)eachItemVals.ValueForKey((NSString)"Name");
                                var fetchedItemTime = (NSString)eachItemVals.ValueForKey((NSString)"Time");
                                var fetchedItemPurchased = ((NSString)(eachItemVals.ValueForKey((NSString)"Purchased")));



                                itemsInList.Add(new ItemClass
                                {
                                    Name = fetchedItemName,
                                    Purchased = fetchedItemPurchased,
                                    Time = fetchedItemTime
                                });
                            }
                        }
                    }

                    GroceryListClass thisList = new GroceryListClass
                    {
                        Name = listName,
                        Owner = AppData.curUser,
                        Items = itemsInList
                    };

                    AppData.onlineLists.Add(thisList);
                }


            ThisIsDone:
                done = true;
            });
            while (!done)
            {
                await Task.Delay(50);
            }
        }
    }
}
