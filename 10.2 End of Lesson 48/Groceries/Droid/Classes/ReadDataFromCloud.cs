using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;

namespace Groceries.Droid
{
    public class TempGroceryListClass
    {
        public string Name { get; set; }
        public UserClass Owner { get; set; }
    }


    public static class ReadDataFromCloud
    {
        public static async Task Read()
        {
            AppData.onlineLists = new List<GroceryListClass>();

            ChildQuery listsNode = AppData_Droid.dataNode
                .Child(AppData.curUser.Uid);

            var allListsData = await listsNode
                .OnceAsync<TempGroceryListClass>();

            foreach (FirebaseObject<TempGroceryListClass> any in allListsData)
            {
                List<ItemClass> itemsOfList = new List<ItemClass>();

                ChildQuery thisListNode = listsNode
                    .Child(any.Object.Name);

                var readItems = await thisListNode.Child("Items")
                    .OnceAsync<ItemClass>();

                foreach (FirebaseObject<ItemClass> anyItem in readItems)
                    itemsOfList.Add(anyItem.Object);

                GroceryListClass newReadList = new GroceryListClass
                {
                    Name = any.Object.Name,
                    Items = itemsOfList,
                    Owner = any.Object.Owner
                };

                AppData.onlineLists.Add(newReadList);
            }
        }
    }
}