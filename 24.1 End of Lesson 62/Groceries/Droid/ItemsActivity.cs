using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views.InputMethods;
using Android.Widget;
using Java.Lang;

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

        ItemRowListAdapter itemsAdapter;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ItemsLayout);

            InterfaceBuilder();

            AppData.GetInstance();

            int row = this.Intent.Extras.GetInt("row");
            curList = AppData.currentLists[row];
            curListNameTextView.Text = curList.Name;

            itemsAdapter = new ItemRowListAdapter(this, curList);
            itemsListView.Adapter = itemsAdapter;
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
            if (e.ActionId != ImeAction.Done)
                return;
            
            ItemClass newItem = new ItemClass()
            {
                Name = newItemEditText.Text,
                Purchased = false.ToString(),
                Time = DateTime.UtcNow.ToString()
            };

            curList.Items.Add(newItem);
            ReadWrite.WriteData();
            SaveItemOnCloud.Save(newItem, curList);


            newItemEditText.Text = "";
            itemsAdapter.NotifyDataSetChanged();

            this.CurrentFocus.ClearFocus();
            InputMethodManager inputManager = 
                (InputMethodManager)GetSystemService(Context.InputMethodService);
            inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken,
                                                 HideSoftInputFlags.None);
        }






        void ItemClicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            ItemClass thisItem = curList.Items[e.Position];

            bool curStatus = false;
            if (thisItem.Purchased == "True" || thisItem.Purchased == "true")
                curStatus = true;

            thisItem.Purchased = (!curStatus).ToString();
            thisItem.Time = DateTime.UtcNow.ToString();

            ReadWrite.WriteData();
            itemsAdapter.NotifyDataSetChanged();
        }



        void ItemLongClicked(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            e.View.Animate()
             .SetDuration(750)
             .Alpha(0)
             .WithEndAction(new Runnable(() =>
           {
                ItemClass toDelete = curList.Items[e.Position];
                curList.Items.Remove(toDelete);

                DeleteFromCloud.DeleteItem(toDelete, curList);

               e.View.Alpha = 1;
                itemsAdapter.NotifyDataSetChanged();
                ReadWrite.WriteData();
           }));
        }

        void ShareThisAction(object sender, EventArgs e)
        {

        }
    }
}
