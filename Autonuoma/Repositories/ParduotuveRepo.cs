using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models;
using Org.Ktu.Isk.P175B602.Autonuoma.ViewModels;


namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories
{
	public class ParduotuveRepo
	{
		public static List<Parduotuve> List()
		{
			var result = new List<Parduotuve>();

			var query =
				$@"SELECT
					par.kodas,
					par.pavadinimas,
                    par.adresas,
                    par.telefonas,
                    par.e_pastas,
					mst.pavadinimas AS miestas
				FROM
					`{Config.TblPrefix}parduotuves` par
					LEFT JOIN `{Config.TblPrefix}miestai` mst ON par.fk_MIESTASpavadinimas=mst.pavadinimas
				ORDER BY par.pavadinimas ASC, par.kodas ASC";

			var dt = Sql.Query(query);

			foreach( DataRow item in dt )
			{
				result.Add(new Parduotuve
				{
                    ID = Convert.ToString(item["kodas"]),
					Pavadinimas = Convert.ToString(item["pavadinimas"]),
                    Adresas = Convert.ToString(item["adresas"]),
                    Telefonas = Convert.ToString(item["telefonas"]),
                    EPastas = Convert.ToString(item["e_pastas"]),
                    FkMiestas = Convert.ToString(item["miestas"])
				});
			}

			return result;
		}

		public static List<Parduotuve> ListForMiestas(string miestas)
		{
			var result = new List<Parduotuve>();

			var query = $@"SELECT * FROM `{Config.TblPrefix}parduotuves` WHERE fk_MIESTASpavadinimas=?miestas ORDER BY fk_MIESTASpavadinimas ASC";

			var dt =
				Sql.Query(query, args => {
					args.Add("?miestas", MySqlDbType.VarChar).Value = miestas;
				});

			foreach( DataRow item in dt )
			{
				result.Add(new Parduotuve
				{
					ID = Convert.ToString(item["kodas"]),
					Pavadinimas = Convert.ToString(item["pavadinimas"]),
                    Adresas = Convert.ToString(item["adresas"]),
                    Telefonas = Convert.ToString(item["telefonas"]),
                    EPastas = Convert.ToString(item["e_pastas"]),
                    FkMiestas = Convert.ToString(item["fk_MIESTASpavadinimas"])
				});
			}

			return result;
		}

		public static ParduotuveEditVM Find(string id)
		{
			var pevm = new ParduotuveEditVM();

			var query = $@"SELECT * FROM `{Config.TblPrefix}parduotuves` WHERE kodas=?id";

			var dt =
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.VarChar).Value = id;
				});

			foreach( DataRow item in dt )
			{
                pevm.Parduotuve.ID = Convert.ToString(item["kodas"]);
                pevm.Parduotuve.Pavadinimas = Convert.ToString(item["pavadinimas"]);
                pevm.Parduotuve.Adresas = Convert.ToString(item["adresas"]);
                pevm.Parduotuve.Telefonas = Convert.ToString(item["telefonas"]);
                pevm.Parduotuve.EPastas = Convert.ToString(item["e_pastas"]);
                pevm.Parduotuve.FkMiestas = Convert.ToString(item["fk_MIESTASpavadinimas"]);
			}

			return pevm;
		}

		public static Parduotuve FindForDeletion(string id)
		{
			var p = new Parduotuve();

			var query =
				$@"SELECT
					par.kodas,
					par.pavadinimas,
                    par.adresas,
                    par.telefonas,
                    par.e_pastas,
					mst.pavadinimas AS miestas
				FROM
					`{Config.TblPrefix}parduotuves` par
					LEFT JOIN `{Config.TblPrefix}miestai` mst ON par.fk_MIESTASpavadinimas=mst.pavadinimas
				WHERE 
                    par.kodas=?kodas";

			var dt =
				Sql.Query(query, args => {
					args.Add("?kodas", MySqlDbType.VarChar).Value = id;
				});

			foreach( DataRow item in dt )
			{
				p.ID = Convert.ToString(item["kodas"]);
                p.Pavadinimas = Convert.ToString(item["pavadinimas"]);
                p.Adresas = Convert.ToString(item["adresas"]);
                p.Telefonas = Convert.ToString(item["telefonas"]);
                p.EPastas = Convert.ToString(item["e_pastas"]);
                p.FkMiestas = Convert.ToString(item["miestas"]);
			}

			return p;
		}

		public static void Update(ParduotuveEditVM parduotuveEVM)
		{
			var query =
				$@"UPDATE `{Config.TblPrefix}parduotuves`
				SET
					kodas=?kodas,
					pavadinimas=?pavadinimas,
                    adresas=?adresas,
                    telefonas=?telefonas,
                    e_pastas=?e_pastas,
					fk_MIESTASpavadinimas=?miestas
				WHERE
					kodas=?kodas";

			Sql.Update(query, args => {
                args.Add("?kodas", MySqlDbType.VarChar).Value = parduotuveEVM.Parduotuve.ID;
				args.Add("?pavadinimas", MySqlDbType.VarChar).Value = parduotuveEVM.Parduotuve.Pavadinimas;
                args.Add("?adresas", MySqlDbType.VarChar).Value = parduotuveEVM.Parduotuve.Adresas;
                args.Add("?telefonas", MySqlDbType.VarChar).Value = parduotuveEVM.Parduotuve.Telefonas;
                args.Add("?e_pastas", MySqlDbType.VarChar).Value = parduotuveEVM.Parduotuve.EPastas;
                args.Add("?miestas", MySqlDbType.VarChar).Value = parduotuveEVM.Parduotuve.FkMiestas;
			});
		}

		public static void Insert(ParduotuveEditVM parduotuveEVM)
		{
			var query =
				$@"INSERT INTO `{Config.TblPrefix}parduotuves`
				(
					kodas,
					pavadinimas,
                    adresas,
                    telefonas,
                    e_pastas,
					fk_MIESTASpavadinimas
				)
				VALUES(
					?kodas,
					?pavadinimas,
                    ?adresas,
                    ?telefonas,
                    ?e_pastas,
					?miestas
				)";

			Sql.Insert(query, args => {
				args.Add("?kodas", MySqlDbType.VarChar).Value = parduotuveEVM.Parduotuve.ID;
				args.Add("?pavadinimas", MySqlDbType.VarChar).Value = parduotuveEVM.Parduotuve.Pavadinimas;
                args.Add("?adresas", MySqlDbType.VarChar).Value = parduotuveEVM.Parduotuve.Adresas;
                args.Add("?telefonas", MySqlDbType.VarChar).Value = parduotuveEVM.Parduotuve.Telefonas;
                args.Add("?e_pastas", MySqlDbType.VarChar).Value = parduotuveEVM.Parduotuve.EPastas;
                args.Add("?miestas", MySqlDbType.VarChar).Value = parduotuveEVM.Parduotuve.FkMiestas;
			});
		}

		public static void Delete(string id)
		{
			var query = $@"DELETE FROM `{Config.TblPrefix}parduotuves` WHERE kodas=?id";
			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.VarChar).Value = id;
			});
		}

		public static bool Exists(string id)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}parduotuves` WHERE kodas=?id";
			var dt = 
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.VarChar).Value = id;
				});

			foreach( DataRow item in dt )
			{
                return true;
			}

			return false;
		}
	}
}