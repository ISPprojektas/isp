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
	/// <summary>th
	/// Database operations related to 'uzsakymai' entity.
	/// </summary>
	public class UzsakymuBusenosRepo
	{
		public static List<UzsakymoBusenos> List()
		{
			var busenos = new List<UzsakymoBusenos>();

			var query =
				$@"SELECT
                    id,
                    name
				FROM
                    `{Config.TblPrefix}užsakymo_būsenos`";

			var dt = Sql.Query(query);

            
			foreach( DataRow item in dt )
			{
				busenos.Add(new UzsakymoBusenos
				{
					Id = Convert.ToInt32(item["id"]),
                    Pavadinimas = Convert.ToString(item["name"])
				});
			}
            
			return busenos;
		}
	}
}