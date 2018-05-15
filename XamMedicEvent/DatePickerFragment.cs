using System;
using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace XamMedicEvent
{
    //https://docs.microsoft.com/en-us/xamarin/android/user-interface/controls/pickers/date-picker
    public class DatePickerFragment : DialogFragment,
        DatePickerDialog.IOnDateSetListener
    {
        // TAG can be any string of your choice.
        public static readonly string TAG = "X:" + typeof(DatePickerFragment).Name.ToUpper();

        // Initialize this value to prevent NullReferenceExceptions.
        private Action<DateTime> _dateSelectedHandler = delegate { };


        //The NewInstance method is invoked to instantiate a new DatePickerFragment. This method takes an Action<DateTime> that will be invoked when the user clicks on the OK button in the DatePickerDialog.
        public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected)
        {
            DatePickerFragment frag = new DatePickerFragment();
            frag._dateSelectedHandler = onDateSelected;
            return frag;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime currently = DateTime.Now;
            DatePickerDialog dialog = new DatePickerDialog(Activity,
                this,
                currently.Year,
                currently.Month - 1,
                currently.Day);
            return dialog;
        }

        //Be aware that the value of the month when IOnDateSetListener.OnDateSet is invoked is in the range of 0 to 11, and not 1 to 12. The day of the month will be in the range of 1 to 31 (depending on which month was selected).
        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            // Note: monthOfYear is a value between 0 and 11, not 1 and 12!
            DateTime selectedDate = new DateTime(year, month + 1, dayOfMonth);
            Log.Debug(TAG, selectedDate.ToLongDateString());
            _dateSelectedHandler(selectedDate);
        }
    }
}
