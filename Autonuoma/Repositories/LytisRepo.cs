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
	public class LytisRepo
	{
		public static List<Lytis> List()
		{
			var lytys = new List<Lytis>();

			string query = $@"SELECT * FROM `{Config.TblPrefix}lytys` ORDER BY id_lytys ASC";
			var dt = Sql.Query(query);

			foreach( DataRow item in dt )
			{
				lytys.Add(new Lytis
				{
					ID = Convert.ToInt32(item["id_lytys"]),
					Pavadinimas = Convert.ToString(item["name"]),
				});
			}

			return lytys;
		}

		public static Lytis Find(int id)
		{
			var lytis = new Lytis();

			var query = $@"SELECT * FROM `{Config.TblPrefix}lytys` WHERE id_lytys=?id";
			var dt = 
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});

			foreach( DataRow item in dt )
			{
				lytis.ID = Convert.ToInt32(item["id_lytys"]);
				lytis.Pavadinimas = Convert.ToString(item["name"]);
			}

			return lytis;
		}

		public static void Update(Lytis lytis)
		{			
			var query = 
				$@"UPDATE `{Config.TblPrefix}lytys` 
				SET 
					name=?pavadinimas 
				WHERE 
					id_lytys=?id";

			Sql.Update(query, args => {
				args.Add("?pavadinimas", MySqlDbType.VarChar).Value = lytis.Pavadinimas;
				args.Add("?id", MySqlDbType.Int32).Value = lytis.ID;
			});							
		}

		public static void Insert(Lytis lytis)
		{			
			var query = $@"INSERT INTO `{Config.TblPrefix}lytys` ( name ) VALUES ( ?pavadinimas )";
			Sql.Insert(query, args => {
				args.Add("?pavadinimas", MySqlDbType.VarChar).Value = lytis.Pavadinimas;
			});
		}

		public static void Delete(int id)
		{			
			var query = $@"DELETE FROM `{Config.TblPrefix}lytys` where id_lytys=?id";
			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.Int32).Value = id;
			});			
		}
	}
}