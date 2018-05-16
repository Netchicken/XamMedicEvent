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

namespace XamMedicEvent
{
    //https://docs.microsoft.com/en-us/xamarin/android/user-interface/layouts/list-view/ 
    [Activity(Label = "ReportActivity")]
    public class ReportActivity : Activity
    {
        Events myEvent = new Events();
        DataManager mydm = new DataManager();
        private TextView Id;
        private TextView date;
        private TextView time;
        private TextView hours;

        private TextView strength;
        private TextView length;
        private TextView meal;


        ListView EventList;
        List<Events> myList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.reports);

            Id = FindViewById<TextView>(Resource.Id.tvlistID);
            date = FindViewById<TextView>(Resource.Id.tvlistdateDisplay);
            time = FindViewById<TextView>(Resource.Id.tvlisttimeDisplay);

            hours = FindViewById<TextView>(Resource.Id.tvlistHours);
            strength = FindViewById<TextView>(Resource.Id.tvlistStrength);
            length = FindViewById<TextView>(Resource.Id.tvlistLength);
            meal = FindViewById<TextView>(Resource.Id.tvlistMeal);
            //listview
            EventList = FindViewById<ListView>(Resource.Id.listView1);
            myList = (List<Events>)mydm.GetItems();
            EventList.Adapter = new DataAdapter(this, myList);
            EventList.ItemClick += EventList_Click;
        }

        private void EventList_Click(object sender, AdapterView.ItemClickEventArgs e)
        {
           // var LV = sender as ListView;
          
            
            //so we are not really getting the data in that view, but getting the item from the db in the position of the one you click on in the list. It has nothing to do with the ID at all, just the poition in the list. 0,1,2,3,4 etc

            var SingleEvent = myList[e.Position];
            mydm.DeleteItem(SingleEvent.Id);
            Toast.MakeText(this, "Deleted " + SingleEvent.Id, ToastLength.Long).Show();
            reloadData();
        }


        //Basically reload stuff when the app resumes operation after being pauused
        protected override void OnResume()
        {
            base.OnResume();
            myList = (List<Events>)mydm.GetItems();
            EventList.Adapter = new DataAdapter(this, myList);
        }

        public void reloadData()
        {
            myList = (List<Events>)mydm.GetItems();
            EventList.Adapter = new DataAdapter(this, myList);

        }
    }
}