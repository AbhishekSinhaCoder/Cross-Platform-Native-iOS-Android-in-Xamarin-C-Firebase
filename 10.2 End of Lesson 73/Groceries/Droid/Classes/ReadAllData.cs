using System;
using System.Threading.Tasks;
using Android.Graphics;

namespace Groceries.Droid
{
    public static class ReadAllData
    {
        public static async Task Read(ListsActivity thisActivity)
        {
            ReadWrite.ReadUser();
            if (AppData.curUser == null)
            {
                AppData.curUser = new UserClass()
                {
                    Name = "Me",
                    Email = "defEmail",
                    Uid = "defUid"
                };

                PrepareFirstLists.Prepare();
                ReadWrite.WriteUser();
                ReadWrite.WriteData();
            }
            else
            {
                ReadWrite.ReadData();
                AppData.currentLists = AppData.offlineLists;
            }

            // up until here, we are offline
            thisActivity.SetProfileButton("Login!", Color.Orange);



            if (AppData_Droid.auth.CurrentUser != null)
            {
                thisActivity.SetProfileButton("Online!", Color.Green);
                await ReadDataFromCloud.Read();
                AppData.currentLists = CompareLists.Compare(AppData.offlineLists, 
                                                             AppData.onlineLists);
                ReadWrite.WriteData();
                foreach (GroceryListClass any in AppData.currentLists)
                    SaveListOnCloud.Save(any);

                await ReadInvitations.Read(); // invitations data

                await FetchInvitations.Fetch(); // invitations list populated
                foreach (GroceryListClass any in AppData.invitationsLists)
                    AppData.currentLists.Add(any);

            }
        }
    }
}
