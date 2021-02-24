using System;
using Foundation;

namespace Groceries.iOS
{
    public static class SaveItemOnCloud
    {
        public static void Save (ItemClass inpItem, 
                                 GroceryListClass inpList)
        {
            object[] keys = { "Name", "Time", "Purchased" };
            object[] vals = {inpItem.Name, inpItem.Time, inpItem.Purchased };

            var itemDict = NSDictionary.FromObjectsAndKeys(vals, keys);

            AppData_iOS.DataNode.GetChild(inpList.Owner.Uid)
                       .GetChild(inpList.Name)
                       .GetChild("Items")
                       .GetChild(inpItem.Name)
                       .SetValue<NSDictionary>(itemDict);
        }

    }
}
