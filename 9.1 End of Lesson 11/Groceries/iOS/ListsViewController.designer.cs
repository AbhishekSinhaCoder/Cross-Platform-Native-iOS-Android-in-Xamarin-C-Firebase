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
    [Register ("ListsViewController")]
    partial class ListsViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView groceryListTableView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton newListButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton pofileButton { get; set; }

        [Action ("NewListButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void NewListButton_TouchUpInside (UIKit.UIButton sender);

        [Action ("PofileButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void PofileButton_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (groceryListTableView != null) {
                groceryListTableView.Dispose ();
                groceryListTableView = null;
            }

            if (newListButton != null) {
                newListButton.Dispose ();
                newListButton = null;
            }

            if (pofileButton != null) {
                pofileButton.Dispose ();
                pofileButton = null;
            }
        }
    }
}