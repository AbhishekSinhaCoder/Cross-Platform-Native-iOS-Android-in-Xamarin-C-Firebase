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
            return 5;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell("listsCell");
            cell.TextLabel.Text = "Test";
            return cell;
        }
    }
}
