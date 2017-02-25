using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


namespace OS_Grid_Calculator.Droid
{
	[Activity (Label = "OS Grid Calculator", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            // Get our text fields from the layout resource,
            // and attach events to them
            EditText yourGrid = FindViewById<EditText>(Resource.Id.yourGrid);
            EditText targetDistance = FindViewById<EditText>(Resource.Id.targetDistance);
            EditText targetBearing = FindViewById<EditText>(Resource.Id.targetBearing);
            EditText targetGrid = FindViewById<EditText>(Resource.Id.targetGrid);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.solveButton);
			button.Click += delegate {
                int bearing = 0;
                int distance = 0;
                bool isInt;

                isInt = int.TryParse(targetBearing.Text, out bearing);
                if (isInt == false)
                {
                    // DisplayAlert("Error", "Invalid bearing entered. Must be a number 0-359", "OK");
                    return;
                }

                isInt = int.TryParse(targetDistance.Text, out distance);
                if (isInt == false)
                {
                    // DisplayAlert("Error", "Invalid distance entered. Must be a whole number", "OK");
                    return;
                }

                if (OSGridCalculator.osGridValidation.IsMatch(yourGrid.Text) == false)
                {
                    return;
                }

                string result = OSGridCalculator.ComputeTargetGrid(yourGrid.Text, distance, bearing);
				targetGrid.Text = result;
			};
		}
	}
}


