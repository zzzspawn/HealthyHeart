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
        Sensor stepCounter;
        TextView textView;

        

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);
            Xamarin.Essentials.Platform.Init(this, bundle);

            sensorManager = (SensorManager)GetSystemService(Context.SensorService);
            heartRatesensor = sensorManager.GetDefaultSensor(SensorType.HeartRate);
            heartBeatsensor = sensorManager.GetDefaultSensor(SensorType.HeartBeat);
            stepCounter = sensorManager.GetDefaultSensor(SensorType.StepCounter);
            sensorManager.RegisterListener(this, heartRatesensor, SensorDelay.Fastest);
            sensorManager.RegisterListener(this, heartBeatsensor, SensorDelay.Fastest);
            sensorManager.RegisterListener(this, stepCounter, SensorDelay.Fastest);
            textView = FindViewById<TextView>(Resource.Id.checkText);
            //textView = FindViewById<TextView>(Resource.Id.text);
            SetAmbientEnabled();
            textView.Text = "Nothing Has Happened.";
        }

        protected override void OnResume()
        {
            base.OnResume();
            if(heartRatesensor != null)
            {
                sensorManager.RegisterListener(this, heartRatesensor, SensorDelay.Fastest); //finn ut av disse dersom en skal lytte konstant (+1)
            }
            else
            {
                Log.Debug("Test_HH", "heartRatesensor was null");
                heartRatesensor = sensorManager.GetDefaultSensor(SensorType.HeartRate);
                sensorManager.RegisterListener(this, heartRatesensor, SensorDelay.Fastest);

            }
            if (heartBeatsensor != null)
            {
                sensorManager.RegisterListener(this, heartBeatsensor, SensorDelay.Fastest); //finn ut av disse dersom en skal lytte konstant (+1)
            }
            else
            {
                Log.Debug("Test_HH", "heartBeatsensor was null");
                heartBeatsensor = sensorManager.GetDefaultSensor(SensorType.HeartBeat);
                sensorManager.RegisterListener(this, heartBeatsensor, SensorDelay.Fastest);
            }
            if (stepCounter != null)
            {
                sensorManager.RegisterListener(this, stepCounter, SensorDelay.Fastest); //finn ut av disse dersom en skal lytte konstant (+1)
            }
            else
            {
                Log.Debug("Test_HH", "stepCounter was null");
                stepCounter = sensorManager.GetDefaultSensor(SensorType.StepCounter);
                sensorManager.RegisterListener(this, stepCounter, SensorDelay.Fastest);
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            sensorManager.UnregisterListener(this); //finn ut av disse dersom en skal lytte konstant (+1)
        }

        public void OnSensorChanged(SensorEvent e)
        {
            if (e.Sensor != null)
            {
                //DetectJump(e.Values[0], (int)e.Timestamp);
                Log.Debug("Test_HH", e.Sensor.Type.ToString());
                Log.Debug("Test_HH", e.Values[0].ToString());
                Log.Debug("Test_HH", e.Timestamp.ToString());
                Console.WriteLine("I Ran!");
            
                textView.Text = "Sensor was changed, sensor was: " + e.Sensor.Type.ToString();

                if (e.Sensor.Type == stepCounter.Type)
                {
                    textView.Text = ("StepCounter Count: " + e.Values[0]);
                }
                else if (e.Sensor.Type == heartBeatsensor.Type)
                {
                    textView.Text = ("Heartbeat value: " + e.Values[0]);
                }
                else if (e.Sensor.Type == heartRatesensor.Type)
                {
                    textView.Text = ("Heart rate value: " + e.Values[0]);
                }
            }

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
            if(sensor != null)
            {
                Log.Debug("Test_HH", "Accuracy changed");
                //textView.Text = "Accuracy changed, sensor was: " + sensor.Type.ToString();

                if (sensor.Type == stepCounter.Type)
                {
                    textView.Text = ("StepCounter accuracy changed");
                }
                else if (sensor.Type == heartBeatsensor.Type)
                {
                    textView.Text = ("Heartbeat accuracy changed");
                }
                else if (sensor.Type == heartRatesensor.Type)
                {
                    textView.Text = ("Heart rate accuracy changed");
                }
            }
        }
    }


}


