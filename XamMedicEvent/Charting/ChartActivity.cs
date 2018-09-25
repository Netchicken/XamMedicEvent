using Android.App;
using Android.Content;
using Android.OS;

using SkiaSharp;

using XamMedicEvent.Charting;
using Microcharts;
//using SkiaSharp;
//using Entry = Microcharts.Entry;

namespace XamMedicEvent
{
    //{//https://blog.xamarin.com/microcharts-elegant-cross-platform-charts-for-any-app/
    //https://github.com/aloisdeniel/Microcharts

    [Activity(Label = "ChartActivity")]
    public class ChartActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);



            //var charts = Charting.Data.CreateQuickstart();

            //FindViewById<ChartView>(Resource.Id.chartView1).Chart = charts[0];
            //FindViewById<ChartView>(Resource.Id.chartView2).Chart = charts[1];
            //FindViewById<ChartView>(Resource.Id.chartView3).Chart = charts[2];
            //FindViewById<ChartView>(Resource.Id.chartView4).Chart = charts[3];

            //    var entries = new[]
            //        {
            //new Charting.Entry(200)
            //{
            //    Label = "January",
            //    ValueLabel = "200",
            //FillColor = SKColor.Parse("#266489")
            //},
            //new Charting.Entry(400)
            //{
            //Label = "February",
            //ValueLabel = "400",
            //FillColor = SKColor.Parse("#68B9C0")
            //},
            //new Charting.Entry(-100)
            //{
            //Label = "March",
            //ValueLabel = "-100",
            //FillColor = SKColor.Parse("#90D585")
            //}
            //        };

            //}
        }
    }
}
