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
	/// <summary>
	/// Database operations related to 'Alergija' entity.
	/// </summary>
	public class AlergijaRepo
	{
		public static List<Alergija> List()
		{
			var alergijos = new List<Alergija>();

			var query =
				$@"SELECT
					ale.id,
					ale.pavadinimas,
                    ale.data,
                    dar.asmens_kodas,
                    dar.vardas AS dvardas,
                    dar.pavarde,
                    gyv.kodas,
                    gyv.vardas
				FROM
					`{Config.TblPrefix}alergijos` ale
					LEFT JOIN `{Config.TblPrefix}darbuotojai` dar ON ale.fk_DARBUOTOJASasmens_kodas=dar.asmens_kodas
                    LEFT JOIN `{Config.TblPrefix}gyvunai` gyv ON ale.fk_GYVUNASkodas=gyv.kodas
				ORDER BY gyv.kodas ASC";

			var dt = Sql.Query(query);

			foreach( DataRow item in dt )
			{
				alergijos.Add(new Alergija
				{
					ID = Convert.ToInt32(item["id"]),
					Pavadinimas = Convert.ToString(item["pavadinimas"]),
                    NustatymoData = Convert.ToDateTime(item["data"]),
                    FkDarbuotojas = Convert.ToString(item["asmens_kodas"]),
                    FkDarbuotojasToString = Convert.ToString(item["dvardas"]) + " " + Convert.ToString(item["pavarde"]),
                    FkGyvunas = Convert.ToString(item["kodas"]),
                    FkGyvunasToString = Convert.ToString(item["vardas"])
				});
			}

			return alergijos;
		}

        /*
		public static List<Alergija> ListForMiestas(string miestas)
		{
			var result = new List<Alergija>();

			var query = $@"SELECT * FROM `{Config.TblPrefix}alergijos` WHERE fk_MIESTASpavadinimas=?miestas ORDER BY fk_MIESTASpavadinimas ASC";

			var dt =
				Sql.Query(query, args => {
					args.Add("?miestas", MySqlDbType.VarChar).Value = miestas;
				});

			foreach( DataRow item in dt )
			{
				result.Add(new Alergija
				{
					AsmensKodas = Convert.ToString(item["asmens_kodas"]),
					Vardas = Convert.ToString(item["vardas"]),
					Pavarde = Convert.ToString(item["pavarde"]),
					GimimoData = Convert.ToDateTime(item["gimimo_data"]),
					Telefonas = Convert.ToString(item["telefonas"]),
					EPastas = Convert.ToString(item["e_pastas"]),
					Adresas = Convert.ToString(item["adresas"]),
					Lytis = Convert.ToInt32(item["lytis"]),
					FkMiestas = Convert.ToString(item["fk_MIESTASpavadinimas"])
				});
			}

			return result;
		}

		public static List<Alergija> ListForLytis(int lytis)
		{
			var result = new List<Alergija>();

			var query = $@"SELECT * FROM `{Config.TblPrefix}alergijos` WHERE lytis=?lytis ORDER BY lytis ASC";

			var dt =
				Sql.Query(query, args => {
					args.Add("?lytis", MySqlDbType.Int32).Value = lytis;
				});

			foreach( DataRow item in dt )
			{
				result.Add(new Alergija
				{
					AsmensKodas = Convert.ToString(item["asmens_kodas"]),
					Vardas = Convert.ToString(item["vardas"]),
					Pavarde = Convert.ToString(item["pavarde"]),
					GimimoData = Convert.ToDateTime(item["gimimo_data"]),
					Telefonas = Convert.ToString(item["telefonas"]),
					EPastas = Convert.ToString(item["e_pastas"]),
					Adresas = Convert.ToString(item["adresas"]),
					Lytis = Convert.ToInt32(item["lytis"]),
					FkMiestas = Convert.ToString(item["fk_MIESTASpavadinimas"])
				});
			}

			return result;
		}*/

		public static AlergijaEditVM Find(int id)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}alergijos`
            LEFT JOIN `{Config.TblPrefix}gyvunai` gyv ON alergijos.fk_GYVUNASkodas=gyv.kodas
            WHERE id=?id";

			var dt =
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});

			if( dt.Count > 0 )
			{
				var aevm = new AlergijaEditVM();

				foreach( DataRow item in dt )
				{
					aevm.Alergija.ID = Convert.ToInt32(item["id"]);
					aevm.Alergija.Pavadinimas = Convert.ToString(item["pavadinimas"]);
                    aevm.Alergija.NustatymoData = Convert.ToDateTime(item["data"]);
                    aevm.Alergija.FkDarbuotojas = Convert.ToString(item["fk_DARBUOTOJASasmens_kodas"]);
                    aevm.Alergija.FkGyvunas = Convert.ToString(item["kodas"]);
				}

				return aevm;
			}

			return null;
		}

		public static Alergija FindForDeletion(int id)
		{
			var a = new Alergija();

			var query =
				$@"SELECT
					ale.id,
					ale.pavadinimas,
                    ale.data,
                    dar.asmens_kodas,
                    dar.vardas AS dvardas,
                    dar.pavarde,
                    gyv.kodas,
                    gyv.vardas
				FROM
					`{Config.TblPrefix}alergijos` ale
					LEFT JOIN `{Config.TblPrefix}darbuotojai` dar ON ale.fk_DARBUOTOJASasmens_kodas=dar.asmens_kodas
                    LEFT JOIN `{Config.TblPrefix}gyvunai` gyv ON ale.fk_GYVUNASkodas=gyv.kodas
				WHERE id=?id";

			var dt =
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});

			foreach( DataRow item in dt )
			{
				a.ID = Convert.ToInt32(item["id"]);
                a.Pavadinimas = Convert.ToString(item["pavadinimas"]);
                a.NustatymoData = Convert.ToDateTime(item["data"]);
                a.FkDarbuotojas = Convert.ToString(item["asmens_kodas"]);
                a.FkDarbuotojasToString = Convert.ToString(item["dvardas"]) + " " + Convert.ToString(item["pavarde"]);
                a.FkGyvunas = Convert.ToString(item["kodas"]);
                a.FkGyvunasToString = Convert.ToString(item["vardas"]);
			}

			return a;
		}

		public static void Insert(AlergijaEditVM AlergijaEVM)
		{
			var query =
				$@"INSERT INTO `{Config.TblPrefix}alergijos`
				(
					pavadinimas,
                    data,
                    fk_DARBUOTOJASasmens_kodas,
                    fk_GYVUNASkodas
				)
				VALUES(
					?pavadinimas,
                    ?data,
                    ?darb,
                    ?gyv
				)";

			Sql.Insert(query, args => {
				args.Add("?pavadinimas", MySqlDbType.VarChar).Value = AlergijaEVM.Alergija.Pavadinimas;
				args.Add("?data", MySqlDbType.Date).Value = AlergijaEVM.Alergija.NustatymoData;
				args.Add("?darb", MySqlDbType.VarChar).Value = AlergijaEVM.Alergija.FkDarbuotojas;
				args.Add("?gyv", MySqlDbType.VarChar).Value = AlergijaEVM.Alergija.FkGyvunas;
			});
		}

		public static void Insert(AlergijaEditVM.AlergijaM AlergijaM)
		{
			var query =
				$@"INSERT INTO `{Config.TblPrefix}alergijos`
				(
					pavadinimas,
                    data,
                    fk_DARBUOTOJASasmens_kodas,
                    fk_GYVUNASkodas
				)
				VALUES(
					?pavadinimas,
                    ?data,
                    ?darb,
                    ?gyv
				)";

			Sql.Insert(query, args => {
				args.Add("?pavadinimas", MySqlDbType.VarChar).Value = AlergijaM.Pavadinimas;
				args.Add("?data", MySqlDbType.Date).Value = AlergijaM.NustatymoData;
				args.Add("?darb", MySqlDbType.VarChar).Value = AlergijaM.FkDarbuotojas;
				args.Add("?gyv", MySqlDbType.VarChar).Value = AlergijaM.FkGyvunas;
			});
		}

		public static void Update(AlergijaEditVM AlergijaEVM)
		{
			var query =
				$@"UPDATE `{Config.TblPrefix}alergijos`
				SET
					pavadinimas=?pavadinimas,
                    data=?data,
                    fk_DARBUOTOJASasmens_kodas=?darb,
                    fk_GYVUNASkodas=?gyv
				WHERE
					id=?id";

			Sql.Update(query, args => {
				args.Add("?pavadinimas", MySqlDbType.VarChar).Value = AlergijaEVM.Alergija.Pavadinimas;
				args.Add("?data", MySqlDbType.Date).Value = AlergijaEVM.Alergija.NustatymoData;
				args.Add("?darb", MySqlDbType.VarChar).Value = AlergijaEVM.Alergija.FkDarbuotojas;
				args.Add("?gyv", MySqlDbType.VarChar).Value = AlergijaEVM.Alergija.FkGyvunas;
				args.Add("?id", MySqlDbType.Int32).Value = AlergijaEVM.Alergija.ID;
			});
		}

		public static void Delete(int id)
		{
			var query = $@"DELETE FROM `{Config.TblPrefix}alergijos` WHERE id=?id";
			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.Int32).Value = id;
			});
		}

		public static void DeleteForGyvunas(string id)
		{
			var query =
				$@"DELETE FROM `{Config.TblPrefix}alergijos`
				WHERE alergijos.fk_GYVUNASkodas=?id";

			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.VarChar).Value = id;
			});
		}
	}
}