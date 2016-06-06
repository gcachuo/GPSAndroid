using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace gpsandroid
{
	public class bd
	{
		public string ip {
			get;
			set;
		}
		public MySqlConnection cnn {
			get;
			set;
		}
		string _ip;
		public bd(string txtIp){
			ip = txtIp;
			_ip = ip;
		}
		public bd(){
			_ip = ip;
		}
		public string conexion ()
		{
			try {
				MySqlConnection sqlconn;
				string connsqlstring = "Server="+_ip+";Port=3306;Database=gpsandroid;Uid=root;Pwd=sqlserver;";
				sqlconn = new MySqlConnection (connsqlstring);
				cnn=sqlconn;
				return "true";
			} catch (Exception ex) {
				return ex.Message;
			} finally {
				
			}
		}
		public bool Update(string id,double lat, double lng){
			try{
			cnn.Open ();
			string queryString = "UPDATE coordenada set latitud_coordenada="+lat+", longitud_coordenada="+lng+" where id_usuario="+id;
			MySqlCommand sqlcmd = new MySqlCommand (queryString, cnn);
			sqlcmd.ExecuteNonQuery ();
			cnn.Close ();
			return true;
			}
			catch(Exception ex){
				var result = ex.Message;
				return false;
			}
		}
		public bool Insert(string id,double lat, double lng){
			try{
				cnn.Open ();
				string queryString = "Insert into historial(id_usuario,latitud_historial,longitud_historial) values("+id+","+lat+","+lng+")";
				MySqlCommand sqlcmd = new MySqlCommand (queryString, cnn);
				sqlcmd.ExecuteNonQuery ();
				cnn.Close ();
				return true;
			}
			catch(Exception ex){
				var result = ex.Message;
				return false;
			}
		}
	}
}

