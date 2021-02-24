using System;
using Foundation;
using UIKit;

namespace Groceries.iOS
{
    public class ListsDataSource : UITableViewSource
    {
        readonly UIViewController dataSourceController;

        public ListsDataSource (UIViewController inpCtrl)
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


            return cell;
        }
    }
}
