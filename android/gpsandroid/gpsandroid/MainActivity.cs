using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MySql.Data.MySqlClient;

namespace gpsandroid
{
	[Activity (Label = "gpsandroid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		bd bd;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			EditText txtUser = FindViewById<EditText> (Resource.Id.txtUser);
			EditText txtPass = FindViewById<EditText> (Resource.Id.txtPass);
			EditText txtip = FindViewById<EditText> (Resource.Id.txtip);

			button.Click += delegate {
				try {
					bd = new bd (txtip.Text);
					var result = bd.conexion ();
					if (result=="true") {
						var conexion = bd.cnn;
						button.Enabled = false;
						var user = txtUser.Text;
						var pass = txtPass.Text;


						try {
							var id = login (user, pass, conexion);
							var activity2 = new Intent (this, typeof(gpsActivity));
							activity2.PutExtra ("id_usuario", id);
							activity2.PutExtra ("ip", txtip.Text);
							StartActivity (activity2);
						} catch(Exception ex) {
							new AlertDialog.Builder (this)
							.SetNeutralButton ("Ok", (sender, args) => {
								// User pressed yes
							})
								.SetMessage (ex.Message)
							.SetTitle ("Error")
							.Show ();
						}

						button.Enabled = true;
					}
					else{
						new AlertDialog.Builder (this)
							.SetNeutralButton ("Ok", (sender, args) => {
								// User pressed yes
							})
							.SetMessage (result)
							.SetTitle ("Error")
							.Show ();
					}
				} catch (Exception ex) {
					new AlertDialog.Builder (this)
						.SetNeutralButton ("Ok", (sender, args) => {
						// User pressed yes
					})
						.SetMessage (ex.Message)
						.SetTitle ("Error")
						.Show ();

					button.Enabled = true;
				}
				//lblMysql.Text="AAAAHHHH";
			};
		}

		string login (string user, string pass, MySqlConnection conexion)
		{
			conexion.Open ();
				string queryString = "select id_usuario id from usuario where nombre_usuario='" + user + "' and pass_usuario='" + pass + "'";
				MySqlCommand sqlcmd = new MySqlCommand (queryString, conexion);
				String result = sqlcmd.ExecuteScalar ().ToString ();
			conexion.Close ();
				return result;
		}
	}
}


