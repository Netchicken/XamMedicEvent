using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System;
using System.IO;

namespace XamMedicEvent
{
    [Activity(Label = "Medical Event Logger", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Events myEvent = new Events();
        private TextView dateDisplay;
        private Button dateSelectButton;

        private TextView timeDisplay;
        private SeekBar sbHours;
        private TextView hoursSeek;

        private TextView eventStrength;
        private SeekBar sbEventStrength;
        private TextView strengthSeek;

        private TextView eventLength;
        private SeekBar sbEventLength;
        private TextView lengthSeek;

        private EditText edtMealType;
        private Button SaveButton;
        private Button HistoryButton;

        //static string dbName = "EventsDB.db";
        //string dbPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), dbName);
        DataManager myDbManager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            myDbManager = new DataManager();

            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //Date and time settings
            dateDisplay = FindViewById<TextView>(Resource.Id.dateDisplay);
            timeDisplay = FindViewById<TextView>(Resource.Id.timeDisplay);
            dateSelectButton = FindViewById<Button>(Resource.Id.dateSelectButton);
            dateSelectButton.Click += (sender, eventArgs) =>
            {
                TimeClickEvent();
                DateClickEvent();
            };


            //event strength
            eventStrength = FindViewById<TextView>(Resource.Id.TVEventStrength);
            sbEventStrength = FindViewById<SeekBar>(Resource.Id.skbEventStrength);
            strengthSeek = FindViewById<TextView>(Resource.Id.tvStrengthSeek);
            sbEventStrength.Progress = 1;
            sbEventStrength.Max = 10;
            sbEventStrength.ProgressChanged += (sender, e) =>
            {
                if (e.FromUser)
                {
                    strengthSeek.Text = string.Format("The strength was a {0}", e.Progress);

                    myEvent.Strength = e.Progress;
                }
            };
            //Event Length 
            eventLength = FindViewById<TextView>(Resource.Id.TVEventLength);
            sbEventLength = FindViewById<SeekBar>(Resource.Id.skbEventLength);
            lengthSeek = FindViewById<TextView>(Resource.Id.tvLengthSeek);
            sbEventLength.Progress = 10;
            sbEventLength.Max = 60;
            sbEventLength.ProgressChanged += (sender, e) =>
            {
                if (e.FromUser)
                {
                    lengthSeek.Text = string.Format("It lasted for {0} minutes", e.Progress);
                    myEvent.Length = e.Progress;
                }
            };

            //Hours ago it happened
            timeDisplay = FindViewById<TextView>(Resource.Id.timeDisplay);
            sbHours = FindViewById<SeekBar>(Resource.Id.skbHoursAgo);
            hoursSeek = FindViewById<TextView>(Resource.Id.tvHoursSeek);
            sbHours.Progress = 1;
            sbHours.Max = 5;
            sbHours.ProgressChanged += (sender, e) =>
            {
                if (e.FromUser)
                {
                    hoursSeek.Text = string.Format("Last meal {0} hours ago", e.Progress);

                    myEvent.Hours = e.Progress;
                }
            };

            //Meal Type
            edtMealType = FindViewById<EditText>(Resource.Id.edtMeal);
            // myEvent.Food = edtMealType.Text;

            //Save button and History
            SaveButton = FindViewById<Button>(Resource.Id.btnSave);
            HistoryButton = FindViewById<Button>(Resource.Id.btnHistory);

            SaveButton.Click += (sender, eventArgs) =>
            {
                //Read the specific parth and see if a db is there
                Java.IO.File filePath = new Java.IO.File(myDbManager.databasePath);

                myEvent.Food = edtMealType.Text;
                if (filePath.Exists()) //if it exists then save to it
                {
                    try
                    {
                        myDbManager.AddItem(myDbManager.FakeEntry());
                        //  myDbManager.AddItem(myEvent);
                        Toast.MakeText(this, "Success! Event saved", ToastLength.Long).Show();
                    }
                    catch (Exception e)
                    {
                        try
                        {
                            myDbManager.AddItem(myDbManager.FakeEntry());
                        }
                        catch (Exception)
                        {

                            Toast.MakeText(this, "Not working tried fake data " + e.Message, ToastLength.Long).Show();
                        }

                        Toast.MakeText(this, "Not working " + e.Message, ToastLength.Long).Show();
                    }
                }
                else
                {
                    Toast.MakeText(this, "DB not found" + filePath.AbsolutePath.ToString(), ToastLength.Long).Show();

                }

            };

            HistoryButton.Click += (sender, eventArgs) =>
            {
                //Open Results page
            };
        }



        private void TimeClickEvent()
        {
            var frag = TimePickerFragment.NewInstance(delegate (DateTime time)
            {
                timeDisplay.Text = time.ToLongTimeString();
                myEvent.Time = time;
            });
            frag.Show(FragmentManager, TimePickerFragment.TAG);
        }


        private void DateClickEvent()
        {
            var frag = DatePickerFragment.NewInstance((System.DateTime date) =>
            {
                myEvent.Date = date;
                dateDisplay.Text = date.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }
    }
}

