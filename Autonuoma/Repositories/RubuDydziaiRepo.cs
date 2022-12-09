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
	public class RubuDydziaiRepo
	{
		public static List<RubuDydziai> List()
		{
			var dydziai = new List<RubuDydziai>();

			var query =
				$@"SELECT * FROM rūbų_dydžiai";

			var dt = Sql.Query(query);

			foreach( DataRow item in dt )
			{
				dydziai.Add(new RubuDydziai
				{
					pk_Id = Convert.ToInt32(item["id"]),
					Pavadinimas = Convert.ToString(item["name"])
				});
			}
			return dydziai;
		}
	}
}