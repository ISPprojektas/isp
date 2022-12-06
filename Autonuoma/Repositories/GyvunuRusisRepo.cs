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
	public class GyvunuRusisRepo
	{
		public static List<GyvunuRusis> List()
		{
			var gyvunuRusys = new List<GyvunuRusis>();

			string query = $@"SELECT * FROM `{Config.TblPrefix}gyvunu_rusys` ORDER BY id_gyvunu_rusys ASC";
			var dt = Sql.Query(query);

			foreach( DataRow item in dt )
			{
				gyvunuRusys.Add(new GyvunuRusis
				{
					ID = Convert.ToInt32(item["id_gyvunu_rusys"]),
					Pavadinimas = Convert.ToString(item["name"]),
				});
			}

			return gyvunuRusys;
		}

		public static GyvunuRusis Find(int id)
		{
			var gyvunuRusis = new GyvunuRusis();

			var query = $@"SELECT * FROM `{Config.TblPrefix}gyvunu_rusys` WHERE id_gyvunu_rusys=?id";
			var dt = 
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});

			foreach( DataRow item in dt )
			{
				gyvunuRusis.ID = Convert.ToInt32(item["id_gyvunu_rusys"]);
				gyvunuRusis.Pavadinimas = Convert.ToString(item["name"]);
			}

			return gyvunuRusis;
		}

		public static void Update(GyvunuRusis gyvunuRusis)
		{			
			var query = 
				$@"UPDATE `{Config.TblPrefix}gyvunu_rusys` 
				SET 
					name=?pavadinimas 
				WHERE 
					id_gyvunu_rusys=?id";

			Sql.Update(query, args => {
				args.Add("?pavadinimas", MySqlDbType.VarChar).Value = gyvunuRusis.Pavadinimas;
				args.Add("?id", MySqlDbType.Int32).Value = gyvunuRusis.ID;
			});							
		}

		public static void Insert(GyvunuRusis gyvunuRusis)
		{			
			var query = $@"INSERT INTO `{Config.TblPrefix}gyvunu_rusys` ( name ) VALUES ( ?pavadinimas )";
			Sql.Insert(query, args => {
				args.Add("?pavadinimas", MySqlDbType.VarChar).Value = gyvunuRusis.Pavadinimas;
			});
		}

		public static void Delete(int id)
		{			
			var query = $@"DELETE FROM `{Config.TblPrefix}gyvunu_rusys` where id_gyvunu_rusys=?id";
			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.Int32).Value = id;
			});			
		}
	}
}