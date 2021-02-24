using System;
using System.Collections.Generic;

namespace Groceries
{
    public static class PrepareFirstLists
    {
        public static void Prepare()
        {
            AppData.currentLists = new List<GroceryListClass>();

            AppData.currentLists.Add(new GroceryListClass()
            {
                Name = "Sample List",
                Owner = AppData.curUser,
                Items = new List<ItemClass>()
            });

            AppData.currentLists[0].Items.Add(new ItemClass()
            {
                Name = "Bread",
                Time = DateTime.UtcNow.ToString(),
                Purchased = false.ToString()
            });

            AppData.currentLists[0].Items.Add(new ItemClass()
            {
                Name = "Milk",
                Time = DateTime.UtcNow.ToString(),
                Purchased = true.ToString()
            });








            AppData.currentLists.Add(new GroceryListClass()
            {
                Name = "Office Stuff",
                Owner = AppData.curUser,
                Items = new List<ItemClass>()
            });

            AppData.currentLists[1].Items.Add(new ItemClass()
            {
                Name = "Pens",
                Time = DateTime.UtcNow.ToString(),
                Purchased = false.ToString()
            });

            AppData.currentLists[1].Items.Add(new ItemClass()
            {
                Name = "Papers",
                Time = DateTime.UtcNow.ToString(),
                Purchased = true.ToString()
            });
        }
    }
}
