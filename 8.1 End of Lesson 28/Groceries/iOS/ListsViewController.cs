using Foundation;
using System;
using UIKit;
using System.Collections.Generic;

namespace Groceries.iOS
{
    public partial class ListsViewController : UIViewController
    {
        public ListsViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            ListsDataSource tableDs = new ListsDataSource(this);
            groceryListTableView.Source = tableDs;

            ReloadData();
        }

        // async Task
        public void ReloadData()
        {
            ReadAllData.Read(this);

            groceryListTableView.ReloadData();
        }


        public override void PrepareForSegue (UIStoryboardSegue segue, 
                                              NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            NSIndexPath senderIndexPath = (NSIndexPath)sender;
            var itemsCtrl = segue.DestinationViewController as ItemsViewController;
            itemsCtrl.curList = AppData.currentLists[senderIndexPath.Row];
        }


        partial void NewListButton_TouchUpInside(UIButton sender)
        {
            UIAlertController alert;
            alert = UIAlertController.Create("New List",
                                            "Please eneter a name",
                                             UIAlertControllerStyle.Alert);
            alert.AddTextField((field) =>
            {
                field.Placeholder = "list name";
                field.KeyboardType = UIKeyboardType.Default;
                field.Font = UIFont.SystemFontOfSize(22);
                field.TextAlignment = UITextAlignment.Center;
            });

            UIAlertAction saveAction;
            saveAction = UIAlertAction.Create("Save",
                                              UIAlertActionStyle.Default,
                         (obj) => SaveAction(alert.TextFields[0].Text));
            alert.AddAction(saveAction);

            alert.AddAction(UIAlertAction.Create("Cancel",
                                                 UIAlertActionStyle.Cancel,
                                                 null));
            PresentViewController(alert, true, null);
        }


        void SaveAction(string inpListName)
        {
            GroceryListClass newList = new GroceryListClass()
            {
                Name = inpListName,
                Owner = AppData.curUser,
                Items = new List<ItemClass>()
            };

            AppData.currentLists.Add(newList);
            ReadWrite.WriteData();

            groceryListTableView.ReloadData();
        }







        partial void PofileButton_TouchUpInside(UIButton sender)
        {
            throw new NotImplementedException();
        }
    }
}