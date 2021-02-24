using Foundation;
using System;
using UIKit;

namespace Groceries.iOS
{
    public partial class ListsViewController : UIViewController
    {
        public ListsViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad();


            ListsDataSource tableDs = new ListsDataSource(this);
            groceryListTableView.Source = tableDs;




            AppData.curUser = new UserClass()
            {
                Name = "Amir",
                Email = "defEmail",
                Uid = "defUid"
            };
            PrepareFirstLists.Prepare();
            groceryListTableView.ReloadData();




        }

        partial void NewListButton_TouchUpInside(UIButton sender)
        {
            throw new NotImplementedException();
        }

        partial void PofileButton_TouchUpInside(UIButton sender)
        {
            throw new NotImplementedException();
        }
    }
}