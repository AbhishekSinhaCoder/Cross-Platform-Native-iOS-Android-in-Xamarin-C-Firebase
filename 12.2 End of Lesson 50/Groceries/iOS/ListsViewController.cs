using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Groceries.iOS
{
    public partial class ListsViewController : UIViewController
    {
        public ListsViewController(IntPtr handle) : base(handle)
        {

        }


        // view controller life cycle
        public override void ViewWillAppear(bool animated)
        {
            groceryListTableView.ReloadData();
        }



        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            AppData_iOS.GetInstance();

            ListsDataSource tableDs = new ListsDataSource(this);
            groceryListTableView.Source = tableDs;

            await ReloadData();

        }


        // async Task
        public async Task ReloadData()
        {
            await ReadAllData.Read(this);

            groceryListTableView.ReloadData();
        }


        public override void PrepareForSegue(UIStoryboardSegue segue,
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
            UIAlertController alert = UIAlertController.Create("Profile",
                                                              "What would you like to do",
                                                               UIAlertControllerStyle.ActionSheet);

            alert.AddAction(UIAlertAction.Create("Register",
                                                 UIAlertActionStyle.Default,
                                                 (obj) => Register.Alert(this)));

            alert.AddAction(UIAlertAction.Create("Login",
                                                 UIAlertActionStyle.Default,
                                                 (obj) => Login.Alert(this)));

            alert.AddAction(UIAlertAction.Create("Logout",
                                                 UIAlertActionStyle.Default,
                                                 (obj) => Logout.LogoutAction(this)));

            alert.AddAction(UIAlertAction.Create("Cancel",
                                                 UIAlertActionStyle.Cancel,
                                                 null));

            this.PresentViewController(alert, true, null);
        }
    }
}