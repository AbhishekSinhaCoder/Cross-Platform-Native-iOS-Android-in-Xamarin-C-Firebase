using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;

namespace Groceries.Droid
{
    public static class FetchInvitations
    {
        public static async Task Fetch()
        {
            if (AppData_Droid.auth.CurrentUser == null)
                return;

            AppData.invitationsLists = new List<GroceryListClass>();

            foreach (InvitationClass anyInvite in AppData.invitationsData)
            {
                var allItems = await AppData_Droid.dataNode
                                            .Child(anyInvite.Owner.Uid)
                                            .Child(anyInvite.Name)
                                            .Child("Items")
                                            .OnceAsync<ItemClass>();

                List<ItemClass> itemsOfList = new List<ItemClass>();

                foreach (FirebaseObject<ItemClass> any in allItems)
                    itemsOfList.Add(any.Object);

                AppData.invitationsLists.Add(new GroceryListClass
                {
                    Name = anyInvite.Name,
                    Items = itemsOfList,
                    Owner = anyInvite.Owner
                });
            }
        }
    }
}
