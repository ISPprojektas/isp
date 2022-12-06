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
	public class SutartiesBusenaRepo
	{
		public static List<SutartiesBusena> List()
		{
			var sutartiesBusenos = new List<SutartiesBusena>();

			string query = $@"SELECT * FROM `{Config.TblPrefix}sutarties_busenos` ORDER BY id_sutarties_busenos ASC";
			var dt = Sql.Query(query);

			foreach( DataRow item in dt )
			{
				sutartiesBusenos.Add(new SutartiesBusena
				{
					ID = Convert.ToInt32(item["id_sutarties_busenos"]),
					Pavadinimas = Convert.ToString(item["name"]),
				});
			}

			return sutartiesBusenos;
		}

		public static SutartiesBusena Find(int id)
		{
			var sutartiesBusena = new SutartiesBusena();

			var query = $@"SELECT * FROM `{Config.TblPrefix}sutarties_busenos` WHERE id_sutarties_busenos=?id";
			var dt = 
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});

			foreach( DataRow item in dt )
			{
				sutartiesBusena.ID = Convert.ToInt32(item["id_sutarties_busenos"]);
				sutartiesBusena.Pavadinimas = Convert.ToString(item["name"]);
			}

			return sutartiesBusena;
		}

		public static void Update(SutartiesBusena sutartiesBusena)
		{			
			var query = 
				$@"UPDATE `{Config.TblPrefix}sutarties_busenos` 
				SET 
					name=?pavadinimas 
				WHERE 
					id_sutarties_busenos=?id";

			Sql.Update(query, args => {
				args.Add("?pavadinimas", MySqlDbType.VarChar).Value = sutartiesBusena.Pavadinimas;
				args.Add("?id", MySqlDbType.Int32).Value = sutartiesBusena.ID;
			});							
		}

		public static void Insert(SutartiesBusena sutartiesBusena)
		{			
			var query = $@"INSERT INTO `{Config.TblPrefix}sutarties_busenos` ( name ) VALUES ( ?pavadinimas )";
			Sql.Insert(query, args => {
				args.Add("?pavadinimas", MySqlDbType.VarChar).Value = sutartiesBusena.Pavadinimas;
			});
		}

		public static void Delete(int id)
		{			
			var query = $@"DELETE FROM `{Config.TblPrefix}sutarties_busenos` where id_sutarties_busenos=?id";
			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.Int32).Value = id;
			});			
		}
	}
}