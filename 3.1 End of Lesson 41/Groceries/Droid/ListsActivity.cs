using System;
using System.Collections.Generic;


using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace Groceries.Droid
{
    [Activity(Label = "Groceries", MainLauncher = true, Icon = "@mipmap/icon")]
    public class ListsActivity : Activity
    {
        Button newListButton;
        ListView groceryListView;
        Button profileButton;
        ListRowCustomAdapter groceryAdapter;


        protected override void OnResume()
        {
            base.OnResume();
            groceryAdapter.NotifyDataSetChanged();
        }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ListsLayout);
            InterfaceBuilder();

            AppData.GetInstance();


            ReloadData();
        }


        public void ReloadData()
        {
            ReadAllData.Read(this);
            groceryAdapter = new ListRowCustomAdapter(this,
                                                      AppData.currentLists);
            groceryListView.Adapter = groceryAdapter;
        }


        void InterfaceBuilder()
        {
            newListButton = FindViewById<Button>(Resource.Id.newListButton_id);
            newListButton.Click += NewListAlertView;

            groceryListView = FindViewById<ListView>(Resource.Id.groceryListView_id);
            groceryListView.ItemClick += GotoItems;
            groceryListView.ItemLongClick += DeleteListAlert;

            profileButton = FindViewById<Button>(Resource.Id.profileButton_id);
            profileButton.Click += ProfileAction;
        }















        void DeleteListAlert(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            GroceryListClass toRemove = AppData.currentLists[e.Position];

            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Confirm Delete?");
            alert.SetMessage("Are you sure you want to delete this list?");

            alert.SetPositiveButton("Delete",
                                    (senderAlert, eAlert) => DeleteList(toRemove, e) );

            alert.SetNegativeButton("Cancel",(senderAlert, eAlert) => { });

            Dialog dialog = alert.Create();
            dialog.Show();
        }

        void DeleteList (GroceryListClass inpList, 
                         AdapterView.ItemLongClickEventArgs e)
        {
            e.View.Animate()
             .SetDuration(750)
             .Alpha(0)
             .WithEndAction(new Runnable(() =>
            {
                AppData.currentLists.Remove(inpList);
                ReadWrite.WriteData();
                groceryAdapter.NotifyDataSetChanged();

                e.View.Alpha = 1;
            }));
        }













        void GotoItems(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent itemsIntent = new Intent(this, typeof(ItemsActivity));
            itemsIntent.PutExtra("row", e.Position);
            StartActivity(itemsIntent);
        }


        void NewListAlertView(object sender, EventArgs e)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("new List");
            alert.SetMessage("Please enter the name of your new list");

            EditText input = new EditText(this)
            {
                TextSize = 22,
                Gravity = GravityFlags.Center,
                Hint = "new list"
            };
            input.SetSingleLine(true);
            alert.SetView(input);

            alert.SetPositiveButton("Save",
                (senderAlert, eAlert) => NewListSave(input.Text));

            alert.SetNegativeButton("Cancel",
                                    (senderAlert, eAlert) => { });

            Dialog dialog = alert.Create();
            dialog.Show();
        }

        void NewListSave(string inpListName)
        {
            GroceryListClass newList = new GroceryListClass()
            {
                Name = inpListName,
                Owner = AppData.curUser,
                Items = new List<ItemClass>()
            };

            AppData.currentLists.Add(newList);
            ReadWrite.WriteData();
            groceryAdapter.NotifyDataSetChanged();
        }



        void ProfileAction(object sender, EventArgs e)
        {

        }
    }
}
