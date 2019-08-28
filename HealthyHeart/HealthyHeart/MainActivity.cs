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
    public class MainActivity : WearableActivity, Android.Hardware.ISensorEventListener
    {
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
            sensorManager.RegisterListener(this, heartRatesensor, SensorDelay.Fastest);

            //textView = FindViewById<TextView>(Resource.Id.text);
            //SetAmbientEnabled();
        }

        protected override void OnResume()
        {
            base.OnResume();
            sensorManager.RegisterListener(this, heartRatesensor, SensorDelay.Fastest); //finn ut av disse dersom en skal lytte konstant (+1)
        }

        protected override void OnPause()
        {
            base.OnPause();
            sensorManager.UnregisterListener(this); //finn ut av disse dersom en skal lytte konstant (+1)
        }

        public void OnSensorChanged(SensorEvent e)
        {
            //DetectJump(e.Values[0], (int)e.Timestamp);
            Log.Debug("Test_HH", e.Sensor.GetType().ToString());
            Log.Debug("Test_HH", e.Values[0].ToString());
            Log.Debug("Test_HH", e.Timestamp.ToString());
            Console.WriteLine("I Ran!");
            TextView textView = FindViewById<TextView>(Resource.Id.checkText);
            textView.Text = "Sensor was changed, sensor was: " + e.Sensor.GetType().ToString();
        }

        

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
            Log.Debug("Test_HH", "Accuracy changed");
            //throw new NotImplementedException();
        }
    }


}


