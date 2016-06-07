
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
using Android.Locations;
using System.Threading.Tasks;

namespace gpsandroid
{
	[Activity (Label = "gpsActivity")]			
	public class gpsActivity : Activity, ILocationListener
	{
		Location _currentLocation;
		LocationManager _locationManager;
		string _locationProvider;
		TextView lblEstatus;
		string id;
		string ip;
		Button btnEnviar;
		bd bd;

		protected override void OnPause()
		{
			base.OnPause();
			_locationManager.RemoveUpdates(this);
		}

		protected override void OnResume()
		{
			base.OnResume();
			_locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
			lblEstatus.Text = "Cargando...";
			btnEnviar.Enabled = false;
		}

		public async void OnLocationChanged(Location location)
		{
			_currentLocation = location;
			if (_currentLocation == null)
			{
				lblEstatus.Text = "Unable to determine your location. Try again in a short while.";
				btnEnviar.Enabled = false;
			}
			else
			{
				lblEstatus.Text = string.Format("{0:f6},{1:f6}", _currentLocation.Latitude, _currentLocation.Longitude);
				btnEnviar.Enabled = true;
				if(!enviarCoordenadas (id)){
					StartActivity (typeof(MainActivity));
				}
				//Address address = await ReverseGeocodeCurrentLocation();
				//DisplayAddress(address);
			}
		}

		public void OnProviderDisabled(string provider) {}

		public void OnProviderEnabled(string provider) {}

		public void OnStatusChanged(string provider, Availability status, Bundle extras) {}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			InitializeLocationManager();

			SetContentView (Resource.Layout.gps);
			id = Intent.GetStringExtra ("id_usuario") ?? "Id no disponible";
			ip = Intent.GetStringExtra ("ip") ?? "Ip no disponible";
			btnEnviar = FindViewById<Button> (Resource.Id.btnEnviar);
			lblEstatus = FindViewById<TextView> (Resource.Id.lblEstatus);
			lblEstatus.Text = "Cargando...";
			btnEnviar.Enabled = false;

			bd= new bd(ip);
			var resultBD = bd.conexion ();
			if (resultBD!="true") {
				lblEstatus.Text = resultBD;
			}

			btnEnviar.Click += delegate {
				lblEstatus.Text="";
				try {
					var result=enviarHistorial (id);
					if(result){
						lblEstatus.Text="Enviadas Correctamente";
					}
					else{
						lblEstatus.Text="Error al enviar";
						StartActivity (typeof(MainActivity));
					}
					_locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
				} catch (Exception ex) {
					new AlertDialog.Builder (this)
						.SetNeutralButton ("Ok", (sender, args) => {
						// User pressed yes
					})
						.SetMessage (ex.Message)
						.SetTitle ("Error")
						.Show ();
				}
			};
		}
		bool enviarCoordenadas(string id){
			_locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
			return bd.Update (id,_currentLocation.Latitude,_currentLocation.Longitude);
		}
		bool enviarHistorial(string id){
			_locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
			return bd.Insert (id,_currentLocation.Latitude,_currentLocation.Longitude);
		}

		async void getCoordenadas(){
			if (_currentLocation == null)
			{
				lblEstatus.Text = "Can't determine the current address. Try again in a few minutes.";
				return;
			}

			//Address address = await ReverseGeocodeCurrentLocation();
			//DisplayAddress (address);
		}

		/*void DisplayAddress(Address address)
		{
			if (address != null)
			{
				StringBuilder deviceAddress = new StringBuilder();
				for (int i = 0; i < address.MaxAddressLineIndex; i++)
				{
					deviceAddress.AppendLine(address.GetAddressLine(i));
				}
				// Remove the last comma from the end of the address.
				lblEstatus.Text = deviceAddress.ToString();
			}
			else
			{
				lblEstatus.Text = "Unable to determine the address. Try again in a few minutes.";
			}
		}*/

		async Task<Address> ReverseGeocodeCurrentLocation()
		{
			Geocoder geocoder = new Geocoder(this);
			IList<Address> addressList =
				await geocoder.GetFromLocationAsync(_currentLocation.Latitude, _currentLocation.Longitude, 10);

			Address address = addressList.FirstOrDefault();
			return address;
		}

		void InitializeLocationManager(){
			_locationManager = (LocationManager) GetSystemService(LocationService);
			Criteria criteriaForLocationService = new Criteria
			{
				Accuracy = Accuracy.Fine
			};
			IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

			if (acceptableLocationProviders.Any())
			{
				_locationProvider = acceptableLocationProviders.First();
			}
			else
			{
				_locationProvider = string.Empty;
			}
		}
	}
}

