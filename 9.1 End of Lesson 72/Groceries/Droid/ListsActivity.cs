using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Firebase.Database.Query;
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

            if (groceryAdapter != null)
                groceryAdapter.NotifyDataSetChanged();
        }


        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ListsLayout);
            InterfaceBuilder();

            AppData_Droid.GetInstance(this);

            await ReloadData();
        }


        public async Task ReloadData()
        {
            await ReadAllData.Read(this);
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

            if (toRemove.Owner.Uid == AppData.curUser.Uid)
            {
                alert.SetTitle("Confirm Delete?");
                alert.SetMessage("Are you sure you want to delete this list?");

                alert.SetPositiveButton("Delete",
                                        (senderAlert, eAlert) => DeleteList(toRemove, e));
            }
            else
            {
                alert.SetTitle("Confirm Remove?");
                alert.SetMessage("Are you sure you want to remove tis invitation?");

                alert.SetPositiveButton("Remove",
                                        (senderAlert, eAlert) => DeleteList(toRemove, e));
            }

            alert.SetNegativeButton("Cancel", (senderAlert, eAlert) => { });

            Dialog dialog = alert.Create();
            dialog.Show();
        }

        void DeleteList(GroceryListClass inpList,
                         AdapterView.ItemLongClickEventArgs e)
        {
            e.View.Animate()
             .SetDuration(750)
             .Alpha(0)
             .WithEndAction(new Runnable(() =>
            {
                if (inpList.Owner.Uid == AppData.curUser.Uid)
                    DeleteFromCloud.DeleteList(inpList);
                else
                {
                    InvitationClass toRemoveInvitation = new InvitationClass()
                    {
                        Name = inpList.Name,
                        Owner = inpList.Owner
                    };
                    RemoveInvitation.Remove(toRemoveInvitation);
                }

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
            SaveListOnCloud.Save(newList);
            groceryAdapter.NotifyDataSetChanged();
        }



        void ProfileAction(object sender, EventArgs e)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Profile management");
            alert.SetMessage("What would you like to do?");

            alert.SetPositiveButton("Register", (s, ev) => Register.Alert(this));
            alert.SetNegativeButton("Login", (s, ev) => Login.Alert(this));
            alert.SetNeutralButton("Logout", (s, ev) => Logout.Out(this));

            Dialog dialog = alert.Create();
            dialog.Window.SetGravity(GravityFlags.Bottom);
            dialog.Show();
        }


        public void SetProfileButton(string statusStr, Color bgColor)
        {
            profileButton.Text = statusStr;
            profileButton.SetBackgroundColor(bgColor);
        }

    }
}
