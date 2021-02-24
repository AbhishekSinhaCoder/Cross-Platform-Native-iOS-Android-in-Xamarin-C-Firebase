using System;
using Foundation;
using UIKit;

namespace Groceries.iOS
{
    public class ListsDataSource : UITableViewSource
    {
        readonly UIViewController dataSourceController;

        public ListsDataSource(UIViewController inpCtrl)
        {
            dataSourceController = inpCtrl;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return AppData.currentLists.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell("listsCell");

            GroceryListClass thisList = AppData.currentLists[indexPath.Row];

            cell.TextLabel.Text = thisList.Name;
            string sub = thisList.Items.Count.ToString() +
                                 " items for " +
                                 thisList.Owner.Name;

            cell.DetailTextLabel.Text = sub;

            if (thisList.Owner.Uid != AppData.curUser.Uid)
                cell.DetailTextLabel.TextColor = UIColor.Red;
            else
                cell.DetailTextLabel.TextColor = UIColor.Black;


            return cell;
        }



        public override bool CanEditRow(UITableView tableView,
                                        NSIndexPath indexPath)
        {
            return true;
        }

        public override string TitleForDeleteConfirmation(UITableView tableView,
                                                          NSIndexPath indexPath)
        {
            GroceryListClass toRemove = AppData.currentLists[indexPath.Row];
            if (toRemove.Owner.Uid == AppData.curUser.Uid)
                return "Delete List?";
            else
                return "Remove Invitation?";
        }

        public override void CommitEditingStyle(UITableView tableView,
                                                UITableViewCellEditingStyle editingStyle,
                                                NSIndexPath indexPath)
        {
            GroceryListClass toRemove = AppData.currentLists[indexPath.Row];
            AppData.currentLists.Remove(toRemove);
            ReadWrite.WriteData();

            if ( toRemove.Owner.Uid == AppData.curUser.Uid)
                DeleteFromCloud.DeleteList(toRemove);
            else
            {
                InvitationClass removeInvitation = new InvitationClass()
                {
                    Name = toRemove.Name,
                    Owner = toRemove.Owner
                };
                RemoveInvitation.Remove(removeInvitation);
            }


            tableView.DeleteRows(new NSIndexPath[] { indexPath },
                                 UITableViewRowAnimation.Fade);
        }


        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            dataSourceController.PerformSegue("toItemsSegue_id", indexPath);
        }


    }
}
