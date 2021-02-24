using System;
namespace Groceries
{
    public static class SetLocalUser
    {
        public static void Set (UserClass inpUser)
        {
            foreach (GroceryListClass any in AppData.currentLists)
                if (any.Owner.Uid == AppData.curUser.Uid)
                    any.Owner = inpUser;

            AppData.curUser = inpUser;

            ReadWrite.WriteData();
            ReadWrite.WriteUser();
        }
    }
}
