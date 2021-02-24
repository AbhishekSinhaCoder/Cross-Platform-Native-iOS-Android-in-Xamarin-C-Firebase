using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;

namespace Groceries.Droid
{
public class ItemRowListAdapter : BaseAdapter<ItemClass>
	{
        readonly GroceryListClass thisList;
        readonly Activity myContext;

        public override ItemClass this[int position]
		{
			get
			{
                return thisList.Items[position];
			}
		}

        public ItemRowListAdapter(Activity context, GroceryListClass inpList) : base()
		{
			this.myContext = context;
            this.thisList = inpList;
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override int Count
		{
            get { return thisList.Items.Count; }
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView;
			if (view == null)
                view = myContext.LayoutInflater.Inflate(Android
                                                        .Resource
                                                        .Layout
                                                        .SimpleListItemChecked,
                                                        null);



			var checkedTextView = view.FindViewById<CheckedTextView>(Android
                                                                     .Resource
                                                                     .Id
                                                                     .Text1);

            ItemClass thisItem = thisList.Items[position];

            checkedTextView.Text = thisItem.Name;
            var pixelToDp = (int)Android.Content.Res.Resources.System.DisplayMetrics.Density;
            checkedTextView.SetMinimumHeight(64 * pixelToDp);


            bool curStatus = false;
            if (thisItem.Purchased == "True")
                curStatus = true;
            
            checkedTextView.Checked = curStatus;


            if (curStatus)
            {
                checkedTextView.SetBackgroundColor(Color.DarkGray);
                checkedTextView.PaintFlags = 
                                            PaintFlags.StrikeThruText | 
                                            PaintFlags.AntiAlias | 
                                            PaintFlags.SubpixelText;
                
                checkedTextView.SetTextColor(Color.LightGray);
            }
            else
            {
				checkedTextView.SetBackgroundColor(Color.LightGray);
                checkedTextView.PaintFlags = 
                                            PaintFlags.LinearText | 
                                            PaintFlags.AntiAlias | 
                                            PaintFlags.SubpixelText; ;
                checkedTextView.SetTextColor(Color.Black); 
            }


			return view;
		}

	}
}
