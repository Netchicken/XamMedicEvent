using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System;

namespace XamMedicEvent
{
    [Activity(Label = "Medical Event Logger", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Event myEvent = new Event();
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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //Date and time settings
            dateDisplay = FindViewById<TextView>(Resource.Id.dateDisplay);
            timeDisplay = FindViewById<TextView>(Resource.Id.timeDisplay);
            dateSelectButton = FindViewById<Button>(Resource.Id.dateSelectButton);
            dateSelectButton.Click += (sender, eventArgs) =>
            {
                DateClickEvent();
                TimeClickEvent();
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
            myEvent.Food = edtMealType.Text;

            //Save button and History
            SaveButton = FindViewById<Button>(Resource.Id.btnSave);
            HistoryButton = FindViewById<Button>(Resource.Id.btnHistory);

            SaveButton.Click += (sender, eventArgs) =>
            {
           //     DataManager.AddItem(myEvent);

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

