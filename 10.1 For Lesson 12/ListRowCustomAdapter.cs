using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;

namespace
{
    public class ListRowCustomAdapter : BaseAdapter<GroceryListClass>
    {
        readonly List<GroceryListClass> curLists;
        readonly Activity myContext;

        public override GroceryListClass this[int position]
        {
            get
            {
                return curLists[position];
            }
        }

        public ListRowCustomAdapter(Activity context, List<GroceryListClass> inpLists) : base()
        {
            this.myContext = context;
            this.curLists = inpLists;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count
        {
            get { return curLists.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
                view = myContext.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);

            var pixelToDp = (int)Android.Content.Res.Resources.System.DisplayMetrics.Density;

            view.SetMinimumHeight(96 * pixelToDp);
           
            TextView MainTextView = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            MainTextView.Text = curLists[position].Name;
            MainTextView.TextSize = 22;
            MainTextView.SetTypeface(null, TypefaceStyle.Bold);


            string subtext = curLists[position].Items.Count.ToString() + " for " + curLists[position].Owner.Name;
           
            TextView SubTextView = view.FindViewById<TextView>(Android.Resource.Id.Text2);
            SubTextView.Text = subtext;
            SubTextView.SetTextColor(Color.DarkGray);

            if (position % 2 == 0)
                view.SetBackgroundColor(Color.Rgb(230, 230, 230));
            else
                view.SetBackgroundColor(Color.Transparent);

            return view;
        }
    }
}