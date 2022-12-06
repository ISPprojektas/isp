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
	public class DarboDienaRepo
	{
		public static List<DarboDiena> List()
		{
			var darboDienos = new List<DarboDiena>();

			string query = $@"SELECT * FROM `{Config.TblPrefix}darbo_dienos` ORDER BY id_darbo_dienos ASC";
			var dt = Sql.Query(query);

			foreach( DataRow item in dt )
			{
				darboDienos.Add(new DarboDiena
				{
					ID = Convert.ToInt32(item["id_darbo_dienos"]),
					Pavadinimas = Convert.ToString(item["name"]),
				});
			}

			return darboDienos;
		}

		public static DarboDiena Find(int id)
		{
			var darboDiena = new DarboDiena();

			var query = $@"SELECT * FROM `{Config.TblPrefix}darbo_dienos` WHERE id_darbo_dienos=?id";
			var dt = 
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});

			foreach( DataRow item in dt )
			{
				darboDiena.ID = Convert.ToInt32(item["id_darbo_dienos"]);
				darboDiena.Pavadinimas = Convert.ToString(item["name"]);
			}

			return darboDiena;
		}

		public static void Update(DarboDiena darboDiena)
		{			
			var query = 
				$@"UPDATE `{Config.TblPrefix}darbo_dienos` 
				SET 
					name=?pavadinimas 
				WHERE 
					id_darbo_dienos=?id";

			Sql.Update(query, args => {
				args.Add("?pavadinimas", MySqlDbType.VarChar).Value = darboDiena.Pavadinimas;
				args.Add("?id", MySqlDbType.Int32).Value = darboDiena.ID;
			});							
		}

		public static void Insert(DarboDiena darboDiena)
		{			
			var query = $@"INSERT INTO `{Config.TblPrefix}darbo_dienos` ( name ) VALUES ( ?pavadinimas )";
			Sql.Insert(query, args => {
				args.Add("?pavadinimas", MySqlDbType.VarChar).Value = darboDiena.Pavadinimas;
			});
		}

		public static void Delete(int id)
		{			
			var query = $@"DELETE FROM `{Config.TblPrefix}darbo_dienos` where id_darbo_dienos=?id";
			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.Int32).Value = id;
			});			
		}
	}
}