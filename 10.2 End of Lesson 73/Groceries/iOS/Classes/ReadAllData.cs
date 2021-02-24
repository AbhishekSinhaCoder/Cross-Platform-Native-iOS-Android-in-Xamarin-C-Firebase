using System;
using System.Threading.Tasks;
using UIKit;

namespace Groceries.iOS
{
    public static class ReadAllData
    {
        public static async Task Read (ListsViewController thisView)
        {
            ReadWrite.ReadUser();
            if ( AppData.curUser == null)
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


            thisView.SetProfileButton("Login!", UIColor.Orange);

            if ( AppData_iOS.auth.CurrentUser != null)
            {
                
                thisView.SetProfileButton("Online!", UIColor.Green);
                await ReadOnlineData.Read();
                AppData.currentLists = CompareLists.Compare(AppData.offlineLists,
                                                            AppData.onlineLists);
                ReadWrite.WriteData();
                foreach (GroceryListClass any in AppData.currentLists)
                    SaveListOnCloud.Save(any);

                await ReadInvitations.Read();

                await FetchInvitations.Fetch();
                // we have our Invitationslsist populated

                foreach (GroceryListClass any in AppData.invitationsLists)
                    AppData.currentLists.Add(any);

            }
        }
    }
}
