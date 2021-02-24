
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


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ItemsLayout);
        }
    }
}
