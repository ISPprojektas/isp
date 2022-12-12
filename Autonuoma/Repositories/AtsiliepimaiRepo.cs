using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Model;
using Org.Ktu.Isk.P175B602.Autonuoma.ViewModels;


namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories
{
	public class AtsiliepimaiRepo
	{
		public static List<Atsiliepimai> ListOfProduct(int id)
		{
			var result = new List<Atsiliepimai>();

			var query =
				$@"SELECT
					Autorius,
					Atsiliepimo_tekstas,
                    Data,
                    Įvertinimas,
                    atsiliepimai.NuotraukaLink,
					atsiliepimai.id,
                    fk_Prekė
				FROM
					atsiliepimai
					JOIN prekės ON prekės.id = atsiliepimai.fk_Prekė
				WHERE prekės.id = ?id 
                ORDER BY atsiliepimai.Data ASC";

			var dt =
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});

			foreach( DataRow item in dt )
			{
				result.Add(new Atsiliepimai
				{
                    Autorius = Convert.ToString(item["Autorius"]),
					Atsiliepimo_Tekstas = Convert.ToString(item["Atsiliepimo_tekstas"]),
                    Data = Convert.ToDateTime(item["Data"]),
                    Ivertinimas = Convert.ToInt32(item["Įvertinimas"]),
                    Nuotraukos_Link = Convert.ToString(item["NuotraukaLink"]),
                    pk_Id = Convert.ToInt32(item["id"]),
                    fk_Preke = Convert.ToInt32(item["fk_Prekė"])
				});
			}

			return result;
		}

		public static void Insert(Atsiliepimai komentaras) {
			var query =
				$@"INSERT INTO `atsiliepimai` 
				(`Autorius`, `Atsiliepimo_tekstas`, `Data`, 
				`Įvertinimas`, `NuotraukaLink`, 
				`fk_Prekė`) 
				VALUES (
					?autorius,
					?tekstas,
					?data,
					?ivert,
					?nuotrauka,
					?fk
				)";
			
			string link = "";
			if (komentaras.Nuotraukos_Link != null)
				link =komentaras.Nuotraukos_Link;
			var scc = Sql.Insert(query, args => {
				args.Add("?autorius", MySqlDbType.VarChar).Value = komentaras.Autorius;
				args.Add("?tekstas", MySqlDbType.VarChar).Value = komentaras.Atsiliepimo_Tekstas;
				args.Add("?data", MySqlDbType.Date).Value = komentaras.Data;
				args.Add("?ivert", MySqlDbType.Int32).Value = komentaras.Ivertinimas;
				args.Add("?nuotrauka", MySqlDbType.VarChar).Value = link;
                args.Add("?fk", MySqlDbType.Int32).Value = komentaras.fk_Preke;
			});
		}
	}
}