using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models;
using Org.Ktu.Isk.P175B602.Autonuoma.Model;
using Org.Ktu.Isk.P175B602.Autonuoma.ViewModels;


namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories
{
	public class ParduotuveRepo
	{
		public static List<Parduotuves> List()
		{
			var result = new List<Parduotuves>();

			var query =
				$@"SELECT
					Pavadinimas,
                    Adresas,
                    Darbo_laikas,
                    Miestas,
                    NuotraukaLink,
					Pašto_kodas,
					id
				FROM parduotuvės";

			var dt = Sql.Query(query);

			foreach( DataRow item in dt )
			{
				result.Add(new Parduotuves
				{
                    ID = Convert.ToString(item["id"]),
					Pavadinimas = Convert.ToString(item["Pavadinimas"]),
                    Adresas = Convert.ToString(item["Adresas"]),
                    Darbo_laikas = Convert.ToString(item["Darbo_laikas"]),
                    Miestas = Convert.ToString(item["Miestas"]),
                    NuotraukaLink = Convert.ToString(item["NuotraukaLink"]),
                    Pašto_kodas = Convert.ToString(item["Pašto_kodas"])
				});
			}

			return result;
		}
	}
}