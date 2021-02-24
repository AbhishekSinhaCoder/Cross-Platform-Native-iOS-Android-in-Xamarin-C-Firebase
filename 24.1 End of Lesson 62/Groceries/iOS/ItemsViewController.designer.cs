// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Groceries.iOS
{
    [Register("ItemsViewController")]
    partial class ItemsViewController
    {
        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        UIKit.UIButton backButton { get; set; }

        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        UIKit.UILabel curListNameLabel { get; set; }

        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        UIKit.UITableView itemsTableView { get; set; }

        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        UIKit.UITextField newItemTextField { get; set; }

        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        UIKit.UIButton shareThisButton { get; set; }

        [Action("BackButton_TouchUpInside:")]
        [GeneratedCode("iOS Designer", "1.0")]
        partial void BackButton_TouchUpInside(UIKit.UIButton sender);

        [Action("ShareThisButton_TouchUpInside:")]
        [GeneratedCode("iOS Designer", "1.0")]
        partial void ShareThisButton_TouchUpInside(UIKit.UIButton sender);

        void ReleaseDesignerOutlets()
        {
            if (backButton != null)
            {
                backButton.Dispose();
                backButton = null;
            }

            if (curListNameLabel != null)
            {
                curListNameLabel.Dispose();
                curListNameLabel = null;
            }

            if (itemsTableView != null)
            {
                itemsTableView.Dispose();
                itemsTableView = null;
            }

            if (newItemTextField != null)
            {
                newItemTextField.Dispose();
                newItemTextField = null;
            }

            if (shareThisButton != null)
            {
                shareThisButton.Dispose();
                shareThisButton = null;
            }
        }
    }
}