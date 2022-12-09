using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Model;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;
using Org.Ktu.Isk.P175B602.Autonuoma.ViewModels;


namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories
{
	/// <summary>
	/// Database operations related to 'RubuDydziai' entity.
	/// </summary>
	public class PrekiuKategorijosRepo
	{
		public static List<Kategorijos> List()
		{
			var kategorijos = new List<Kategorijos>();

			var query =
				$@"SELECT * FROM kategorijos";

			var dt = Sql.Query(query);

			foreach( DataRow item in dt )
			{
				kategorijos.Add(new Kategorijos
				{
					pk_SutrumpintasPavadinimas = Convert.ToString(item["Sutrumpintas_pav"]),
					Pavadinimas = Convert.ToString(item["Pavadinimas"]),
					Aprasymas = Convert.ToString(item["Aprašymas"]),
					NuotraukaLink = Convert.ToString(item["NuotraukaLink"])
				});
			}
			return kategorijos;
		}
	}
}