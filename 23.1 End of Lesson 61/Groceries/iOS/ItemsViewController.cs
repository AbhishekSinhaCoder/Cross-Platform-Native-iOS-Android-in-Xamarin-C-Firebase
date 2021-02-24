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
            throw new NotImplementedException();
        }
    }
}