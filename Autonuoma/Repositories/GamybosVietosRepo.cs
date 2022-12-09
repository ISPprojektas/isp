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
	public class GamybosVietosRepo
	{
		public static List<GamybosVietos> List()
		{
			var vietos = new List<GamybosVietos>();

			var query =
				$@"SELECT * FROM gamybos_vietos";

			var dt = Sql.Query(query);

			foreach( DataRow item in dt )
			{
				vietos.Add(new GamybosVietos
				{
					pk_Id = Convert.ToInt32(item["id"]),
					Salis = Convert.ToString(item["Šalis"]),
					Miestas = Convert.ToString(item["Miestas"]),
					Adresas = Convert.ToString(item["Adresas"]),
					TelNr = Convert.ToString(item["TelefonoNr"]),
					PastoKodas = Convert.ToString(item["PaštoKodas"]),
				});
			}
			return vietos;
		}
	}
}