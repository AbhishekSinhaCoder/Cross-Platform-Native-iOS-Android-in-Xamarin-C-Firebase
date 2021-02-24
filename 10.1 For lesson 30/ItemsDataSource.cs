using System;
using Foundation;
using UIKit;

namespace 
{
    public class ItemsDataSource : UITableViewSource
    {
        readonly GroceryListClass thisList;

        public ItemsDataSource (GroceryListClass inpList)
        {
            thisList = inpList;
        }

        public override nint RowsInSection(UITableView tableview, 
                                           nint section)
        {
            return thisList.Items.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, 
                                                NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell("itemsCell");
            ItemClass item = thisList.Items[indexPath.Row];


            NSAttributedString attrStr;

            bool itemPurchased = (item.Purchased == "true" || item.Purchased == "True") ? true : false;


            if ( itemPurchased)
            {
                cell.Accessory = UITableViewCellAccessory.Checkmark;
                cell.BackgroundColor = UIColor.DarkGray;
                cell.TextLabel.TextColor = UIColor.LightGray;
                attrStr = new NSAttributedString(item.Name,
                                                 strikethroughStyle: NSUnderlineStyle.Single);
                cell.TextLabel.AttributedText = attrStr;
            }
            else
            {
                cell.Accessory = UITableViewCellAccessory.None;
                cell.BackgroundColor = UIColor.White;
                cell.TextLabel.TextColor = UIColor.Black;
                cell.TextLabel.AttributedText = new NSAttributedString(item.Name);
            }

            return cell;
        }
   



        public override void RowSelected(UITableView tableView, 
                                     NSIndexPath indexPath)
        {
            ItemClass thisItem = thisList.Items[indexPath.Row];

            bool curStatus = false;
            if (thisItem.Purchased == "True" || thisItem.Purchased == "true")
                curStatus = true;

            thisItem.Purchased = (!curStatus).ToString();
            thisItem.Time = DateTime.UtcNow.ToString();
            ReadWrite.WriteData();

            tableView.ReloadData();
        }



        public override bool CanEditRow(UITableView tableView, 
                                        NSIndexPath indexPath)
        {
            return true;
        }

        public override string TitleForDeleteConfirmation(UITableView tableView, 
                                                          NSIndexPath indexPath)
        {
            return "Delete?";
        }

        public override void CommitEditingStyle(UITableView tableView, 
                                                UITableViewCellEditingStyle editingStyle, 
                                                NSIndexPath indexPath)
        {
            ItemClass toDelete = thisList.Items[indexPath.Row];
            thisList.Items.Remove(toDelete);
            ReadWrite.WriteData();

            tableView.DeleteRows(new NSIndexPath[] {indexPath}, 
                                 UITableViewRowAnimation.Fade);
        }
    
    }
}
