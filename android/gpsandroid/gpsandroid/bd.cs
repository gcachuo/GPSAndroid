using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace gpsandroid
{
	public class bd
	{
		public bd ()
		{
			
		}
		public MySqlConnection conexion ()
		{
			try {
				MySqlConnection sqlconn;
				string connsqlstring = "Server=192.168.1.71;Port=3306;database=gpsandroid;User Id=root;Password=sqlserver;charset=utf8";
				sqlconn = new MySqlConnection (connsqlstring);
				return sqlconn;
			} catch (Exception) {
				return null;
			} finally {
				
			}
		}
		public bool Update(string id,double lat, double lng){
			var conn = conexion ();
			try{
			conn.Open ();
			string queryString = "UPDATE coordenada set latitud_coordenada="+lat+", longitud_coordenada="+lng+" where id_usuario="+id;
			MySqlCommand sqlcmd = new MySqlCommand (queryString, conn);
			sqlcmd.ExecuteNonQuery ();
			conn.Close ();
			return true;
			}
			catch(Exception ex){
				var result = ex.Message;
				return false;
			}
		}
	}
}

