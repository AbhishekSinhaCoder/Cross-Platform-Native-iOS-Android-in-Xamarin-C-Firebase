using System;
using System.Threading.Tasks;

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

            if ( AppData_iOS.auth.CurrentUser != null)
            {
                // we have a Logged In user
                // we have to read our online Lists
                // compare both Online and Offline lists
                // place it in the current lists
                // read our invitation lists
            }


        }
    }
}
