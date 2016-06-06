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
			var bd = new bd ();
			var conexion = bd.conexion ();



			button.Click += delegate {
				try {
					var user = txtUser.Text;
					var pass = txtPass.Text;

					var id = login (user, pass, conexion);
					if (id != "false") {
						var activity2 = new Intent (this, typeof(gpsActivity));
						activity2.PutExtra ("id_usuario", id);
						StartActivity (activity2);
					} else {
						new AlertDialog.Builder (this)
							.SetNeutralButton ("Ok", (sender, args) => {
							// User pressed yes
						})
							.SetMessage ("Datos Incorrectos")
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
				}
				//lblMysql.Text="AAAAHHHH";
			};
		}

		string login (string user, string pass, MySqlConnection conexion)
		{
			try {
				
				conexion.Open ();
				string queryString = "select id_usuario id from usuario where nombre_usuario='" + user + "' and pass_usuario='" + pass + "'";
				MySqlCommand sqlcmd = new MySqlCommand (queryString, conexion);
				String result = sqlcmd.ExecuteScalar ().ToString ();
				conexion.Close ();
				return result;
			} catch (Exception ex) {
				var result = ex.Message;
				conexion.Close ();
				return "false";
			}
		}
	}
}


