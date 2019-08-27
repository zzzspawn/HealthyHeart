using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Hardware;
using Android.Support.Wearable.Views;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.Wearable.Activity;
using Java.Interop;
using Android.Views.Animations;
using Xamarin.Essentials;
using Android.Util;
using Java.Util;
using Java.Lang;

namespace HealthyHeart
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : WearableActivity
    {
        TextView textView;
        SensorManager sensorManager;
        Sensor heartRatesensor;
        Sensor heartBeatsensor;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);
            Xamarin.Essentials.Platform.Init(this, bundle);

            sensorManager = (SensorManager)GetSystemService(Context.SensorService);
            heartRatesensor = sensorManager.GetDefaultSensor(SensorType.HeartRate);
            heartBeatsensor = sensorManager.GetDefaultSensor(SensorType.HeartBeat);


            //textView = FindViewById<TextView>(Resource.Id.text);
            //SetAmbientEnabled();
        }

        public void OnSensorChanged(SensorEvent e)
        {
            //DetectJump(e.Values[0], (int)e.Timestamp);
            Log.Debug("Test_HH", e.Sensor.GetType().ToString());
            Log.Debug("Test_HH", e.Values[0].ToString());
            Log.Debug("Test_HH", e.Timestamp.ToString());
            Console.WriteLine("I Ran!");
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}


