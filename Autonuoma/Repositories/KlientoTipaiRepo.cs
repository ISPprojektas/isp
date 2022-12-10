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
	public class KlientoTipaiRepo
	{
		public static List<KlientoTipai> List()
		{
			var klientoTip = new List<KlientoTipai>();

			var query =
				$@"SELECT * FROM kliento_tipai";

			var dt = Sql.Query(query);

			foreach (DataRow item in dt)
			{
				klientoTip.Add(new KlientoTipai
				{
					pk_Id = Convert.ToInt32(item["id"]),
					Pavadinimas = Convert.ToString(item["name"])
				});
			}
			return klientoTip;
		}
	}
}
