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
	public class GyvunuDydisRepo
	{
		public static List<GyvunuDydis> List()
		{
			var gyvunuDydziai = new List<GyvunuDydis>();

			string query = $@"SELECT * FROM `{Config.TblPrefix}gyvunu_dydziai` ORDER BY id_gyvunu_dydziai ASC";
			var dt = Sql.Query(query);

			foreach( DataRow item in dt )
			{
				gyvunuDydziai.Add(new GyvunuDydis
				{
					ID = Convert.ToInt32(item["id_gyvunu_dydziai"]),
					Pavadinimas = Convert.ToString(item["name"]),
				});
			}

			return gyvunuDydziai;
		}

		public static GyvunuDydis Find(int id)
		{
			var gyvunuDydis = new GyvunuDydis();

			var query = $@"SELECT * FROM `{Config.TblPrefix}gyvunu_dydziai` WHERE id_gyvunu_dydziai=?id";
			var dt = 
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});

			foreach( DataRow item in dt )
			{
				gyvunuDydis.ID = Convert.ToInt32(item["id_gyvunu_dydziai"]);
				gyvunuDydis.Pavadinimas = Convert.ToString(item["name"]);
			}

			return gyvunuDydis;
		}

		public static void Update(GyvunuDydis gyvunuDydis)
		{			
			var query = 
				$@"UPDATE `{Config.TblPrefix}gyvunu_dydziai` 
				SET 
					name=?pavadinimas 
				WHERE 
					id_gyvunu_dydziai=?id";

			Sql.Update(query, args => {
				args.Add("?pavadinimas", MySqlDbType.VarChar).Value = gyvunuDydis.Pavadinimas;
				args.Add("?id", MySqlDbType.Int32).Value = gyvunuDydis.ID;
			});							
		}

		public static void Insert(GyvunuDydis gyvunuDydis)
		{			
			var query = $@"INSERT INTO `{Config.TblPrefix}gyvunu_dydziai` ( name ) VALUES ( ?pavadinimas )";
			Sql.Insert(query, args => {
				args.Add("?pavadinimas", MySqlDbType.VarChar).Value = gyvunuDydis.Pavadinimas;
			});
		}

		public static void Delete(int id)
		{			
			var query = $@"DELETE FROM `{Config.TblPrefix}gyvunu_dydziai` where id_gyvunu_dydziai=?id";
			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.Int32).Value = id;
			});			
		}
	}
}