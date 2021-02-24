using Foundation;
using System;
using UIKit;

namespace Groceries.iOS
{
    public partial class ItemsViewController : UIViewController
    {
        public GroceryListClass curList { get; set; }
        ItemsDataSource itemsDS;

        public ItemsViewController(IntPtr handle) : base(handle)
        {
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //AppData_iOS.GetInstance();


            curListNameLabel.Text = curList.Name;

            itemsDS = new ItemsDataSource(curList);
            itemsTableView.Source = itemsDS;


            newItemTextField.ReturnKeyType = UIReturnKeyType.Done;
            newItemTextField.ShouldReturn += MakeNewItem;
        }

        bool MakeNewItem(UITextField textField)
        {
            ItemClass newItem = new ItemClass()
            {
                Name = textField.Text,
                Time = DateTime.UtcNow.ToString(),
                Purchased = false.ToString()
            };

            curList.Items.Add(newItem);
            ReadWrite.WriteData();
            SaveItemOnCloud.Save(newItem, curList);

            itemsTableView.ReloadData();

            textField.Text = "";
            textField.ResignFirstResponder();

            return true;
        }






        partial void BackButton_TouchUpInside(UIButton sender)
        {
            this.DismissViewController(true, null);
        }

        partial void ShareThisButton_TouchUpInside(UIButton sender)
        {
            if (AppData_iOS.auth.CurrentUser == null)
            {
                AlertShow.Show(this, "Offline", "You have to login first");
                return;
            }

            UIAlertController shareAlert;
            shareAlert = UIAlertController.Create("Inviting Somone?",
                                                 "Please enter their email address",
                                                  UIAlertControllerStyle.Alert);

            shareAlert.AddTextField((obj) =>
            {
                obj.Font = UIFont.SystemFontOfSize(22);
                obj.TextAlignment = UITextAlignment.Center;
                obj.Placeholder = "email address";
            });


            shareAlert.AddAction(UIAlertAction.Create("Invite",
                                                      UIAlertActionStyle.Default,
                                                      (obj) =>
                                                      InviteSomeone.Invite(this,
                                                                           curList,
                                                                           shareAlert.TextFields[0].Text)
                                                     ));

            shareAlert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));

            PresentViewController(shareAlert, true, null);
        }
    }
}