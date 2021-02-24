using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Foundation;

namespace 
{
    public static class FetchInvitations
    {
        public static async Task Fetch()
		{
            AppData.invitationsLists = new List<GroceryListClass>();

            if (AppData_iOS.auth.CurrentUser == null)
                return;

            bool done = true;

            foreach (InvitationClass anyCoord in AppData.invitationsData)
            {
                done = false;
                string listName = anyCoord.Name;
                string ownerUid = anyCoord.Owner.Uid;

                AppData_iOS.DataNode
                       .GetChild(ownerUid)
                       .GetChild(listName)
                       .ObserveSingleEvent(DataEventType.Value, (snapshot) =>
                {
                    var thisListAllData = snapshot.GetValue<NSDictionary>();


                    List<ItemClass> itemsInList = new List<ItemClass>();

                    if (thisListAllData.ValueForKey((NSString)"Items") != null)
                    {
                        if ((thisListAllData.ValueForKey((NSString)"Items")).IsKindOfClass(new ObjCRuntime.Class(typeof(NSDictionary))))
                        {
                            NSDictionary itemsOfListVals = (NSDictionary)NSObject.FromObject(thisListAllData.ValueForKey((NSString)"Items"));

                            for (int i = 0; i < (int)itemsOfListVals.Values.Length; i++)
                            {
                                NSDictionary eachItemVals = (NSDictionary)NSObject.FromObject(itemsOfListVals.Values[i]);
                                var fetchedItemName = (NSString)eachItemVals.ValueForKey((NSString)"Name");
                                var fetchedItemTime = (NSString)eachItemVals.ValueForKey((NSString)"Time");
                                var fetchedItemPurchased = (NSString)eachItemVals.ValueForKey((NSString)"Purchased");

                                ItemClass newItem = new ItemClass
                                {
                                    Name = fetchedItemName,
                                    Purchased = fetchedItemPurchased,
                                    Time = fetchedItemTime
                                };

                                itemsInList.Add(newItem);
                            }
                        }
                    }

                    GroceryListClass thisList = new GroceryListClass
                    {
                        Name = listName,
                        Owner = anyCoord.Owner,
                        Items = itemsInList
                    };

                    AppData.invitationsLists.Add(thisList);

                    done = true;
                });
            }
            while (!done)
			{
				await Task.Delay(50);
			}
		}
	}
}
