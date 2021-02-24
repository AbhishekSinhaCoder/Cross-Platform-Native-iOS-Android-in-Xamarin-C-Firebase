using System;
using System.Threading.Tasks;

namespace Groceries.Droid
{
    public static class ReadAllData
    {
        public static async Task Read (ListsActivity thisActivity)
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


            if ( AppData_Droid.auth.CurrentUser != null)
            {
                // we are ONLINE

                // we should read our online data
                // compare them
                // and place them in our list view
                // we have to read the lists we have been invited to
            }
        }
    }
}
