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

		MySqlConnection sqlconn;

		public MySqlConnection conexion ()
		{
			try {
				MySqlConnection sqlconn;
				string connsqlstring = "Server=192.168.1.71;Port=3306;database=gpsandroid;User Id=root;Password=sqlserver;charset=utf8";
				sqlconn = new MySqlConnection (connsqlstring);
				return sqlconn;
			} catch (Exception ex) {
				return null;
			} finally {
				
			}
		}
	}
}

