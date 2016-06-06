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
		public bool Update(){
			conexion ().Open ();
			/*string queryString = "select count(0) from usuario where nombre_usuario='"+user+"' and pass_usuario='"+pass+"'";
			MySqlCommand sqlcmd = new MySqlCommand (queryString, conexion);
			String result = sqlcmd.ExecuteScalar ().ToString ();*/
			conexion ().Close ();
			return false;
		}
	}
}

