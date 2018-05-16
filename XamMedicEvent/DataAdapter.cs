using System;
using System.Collections.Generic;
using Android.App;
using Android.Widget;


namespace XamMedicEvent
{
    /// <summary>
    /// Adapter that presents Tasks in a row-view
    /// </summary>
    public class DataAdapter : BaseAdapter<Events>
    {
        Activity context = null;
        IList<Events> tasks = new List<Events>();

        public DataAdapter(Activity context, IList<Events> tasks)
        {
            //contructor injection
            this.context = context;
            this.tasks = tasks;
        }

        public override Events this[int position] {
            get { return tasks[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count {
            get { return tasks.Count; }
        }

        public override Android.Views.View GetView(int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
        {
            // Get our object for position
            var item = tasks[position];
            //create a view bind it to the data

            var view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.ListItem, null);

            view.FindViewById<TextView>(Resource.Id.tvlistID).Text = item.Id.ToString();
            view.FindViewById<TextView>(Resource.Id.tvlistdateDisplay).Text = item.Date.ToLongDateString();
            view.FindViewById<TextView>(Resource.Id.tvlisttimeDisplay).Text = item.Time.ToLongTimeString();
            view.FindViewById<TextView>(Resource.Id.tvlistHours).Text = item.Hours.ToString();
            view.FindViewById<TextView>(Resource.Id.tvlistStrength).Text = item.Strength.ToString();
            view.FindViewById<TextView>(Resource.Id.tvlistLength).Text = item.Length.ToString();
            view.FindViewById<TextView>(Resource.Id.tvlistMeal).Text = item.Food;

            //Finally return the view
            return view;
        }
    }
}