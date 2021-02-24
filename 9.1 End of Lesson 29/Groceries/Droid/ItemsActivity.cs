
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Groceries.Droid
{
    [Activity(Label = "ItemsActivity")]
    public class ItemsActivity : Activity
    {
        Button backButton;
        TextView curListNameTextView;
        EditText newItemEditText;
        ListView itemsListView;
        Button shareThisButton;
        GroceryListClass curList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ItemsLayout);

            InterfaceBuilder();

            AppData.GetInstance();

            int row = this.Intent.Extras.GetInt("row");
            curList = AppData.currentLists[row];
            curListNameTextView.Text = curList.Name;
        }


        void InterfaceBuilder ()
        {
            backButton = FindViewById(Resource.Id.backButton_id) as Button;
            backButton.Click += GoBackAction;

            curListNameTextView = FindViewById(Resource.Id.curListTextView_id) as TextView;

            newItemEditText = FindViewById(Resource.Id.newItemEditText_id) as EditText;
            newItemEditText.EditorAction += AddNewItem;


            itemsListView = FindViewById(Resource.Id.itemsListView_id) as ListView;
            itemsListView.ItemClick += ItemClicked;
            itemsListView.ItemLongClick += ItemLongClicked;


            shareThisButton = FindViewById(Resource.Id.shareThisButton_id) as Button;
            shareThisButton.Click += ShareThisAction;
        }

        void GoBackAction(object sender, EventArgs e)
        {
            Finish();
        }

        void AddNewItem(object sender, TextView.EditorActionEventArgs e)
        {

        }

        void ItemClicked(object sender, AdapterView.ItemClickEventArgs e)
        {

        }

        void ItemLongClicked(object sender, AdapterView.ItemLongClickEventArgs e)
        {

        }

        void ShareThisAction(object sender, EventArgs e)
        {

        }
    }
}
