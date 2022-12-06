using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models;


namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories
{
	public class MiestasRepo
	{
		public static List<Miestas> List()
		{
			var miestai = new List<Miestas>();

			string query = $@"SELECT * FROM `{Config.TblPrefix}miestai` ORDER BY pavadinimas ASC";
			var dt = Sql.Query(query);

			foreach( DataRow item in dt )
			{
				miestai.Add(new Miestas
				{
					Pavadinimas = Convert.ToString(item["pavadinimas"]),
                    GyventojuSkaicius = Sql.AllowNull(item["gyventoju_skaicius"], it => (int?)Convert.ToInt32(it))
				});
			}

			return miestai;
		}

		public static Miestas Find(string name)
		{
			var miestas = new Miestas();

			var query = $@"SELECT * FROM `{Config.TblPrefix}miestai` WHERE pavadinimas=?name";
			var dt = 
				Sql.Query(query, args => {
					args.Add("?name", MySqlDbType.VarChar).Value = name;
				});

			foreach( DataRow item in dt )
			{
                miestas.Pavadinimas = Convert.ToString(item["pavadinimas"]);
                miestas.GyventojuSkaicius = Sql.AllowNull(item["gyventoju_skaicius"], it => (int?)Convert.ToInt32(it));
			}

			return miestas;
		}

		public static void Update(Miestas miestas)
		{			
			var query = 
				$@"UPDATE `{Config.TblPrefix}miestai` 
				SET 
					gyventoju_skaicius=?gyventoju_skaicius 
				WHERE 
					pavadinimas=?pavadinimas";

			Sql.Update(query, args => {
				args.Add("?pavadinimas", MySqlDbType.VarChar).Value = miestas.Pavadinimas;
				args.Add("?gyventoju_skaicius", MySqlDbType.Int32).Value = miestas.GyventojuSkaicius;
			});							
		}

		public static void Insert(Miestas miestas)
		{			
			var query = $@"INSERT INTO `{Config.TblPrefix}miestai` ( pavadinimas, gyventoju_skaicius ) VALUES ( ?pavadinimas, ?gyventojuSkaicius )";
			Sql.Insert(query, args => {
				args.Add("?pavadinimas", MySqlDbType.VarChar).Value = miestas.Pavadinimas;
                args.Add("?gyventojuSkaicius", MySqlDbType.Int32).Value = miestas.GyventojuSkaicius;
			});
		}

		public static void Delete(string name)
		{			
			var query = $@"DELETE FROM `{Config.TblPrefix}miestai` where pavadinimas=?name";
			Sql.Delete(query, args => {
				args.Add("?name", MySqlDbType.VarChar).Value = name;
			});			
		}

		public static bool Exists(string name)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}miestai` WHERE pavadinimas=?name";
			var dt = 
				Sql.Query(query, args => {
					args.Add("?name", MySqlDbType.VarChar).Value = name;
				});

			foreach( DataRow item in dt )
			{
                return true;
			}

			return false;
		}
	}
}