
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

namespace gpsandroid
{
	[Activity (Label = "gpsActivity")]			
	public class gpsActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.gps);
			TextView lblMysql = FindViewById<TextView> (Resource.Id.lblMysql);
			var bd = new bd ();

			//lblMysql.Text = bd.GetAccountCountFromMySQL();
		}
	}
}

