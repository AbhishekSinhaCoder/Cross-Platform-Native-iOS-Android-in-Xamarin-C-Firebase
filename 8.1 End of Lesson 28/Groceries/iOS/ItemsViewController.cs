using Foundation;
using System;
using UIKit;

namespace Groceries.iOS
{
    public partial class ItemsViewController : UIViewController
    {
        public GroceryListClass curList { get; set; }

        public ItemsViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            curListNameLabel.Text = curList.Name;
        }


        partial void BackButton_TouchUpInside(UIButton sender)
        {
            this.DismissViewController(true, null);
        }

        partial void ShareThisButton_TouchUpInside(UIButton sender)
        {
            throw new NotImplementedException();
        }
    }
}