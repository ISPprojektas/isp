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
	/// Database operations related to 'Vaistai' entity.
	/// </summary>
	public class VaistaiRepo
	{
		public static List<Vaistai> List()
		{
			var vaistai = new List<Vaistai>();

			var query =
				$@"SELECT
					vst.id,
					vst.pavadinimas,
                    vst.israsymo_data,
                    vst.kaina,
                    dar.asmens_kodas,
                    dar.vardas AS dvardas,
                    dar.pavarde,
                    gyv.kodas,
                    gyv.vardas
				FROM
					`{Config.TblPrefix}medikacijos` vst
					LEFT JOIN `{Config.TblPrefix}darbuotojai` dar ON vst.fk_DARBUOTOJASasmens_kodas=dar.asmens_kodas
                    LEFT JOIN `{Config.TblPrefix}gyvunai` gyv ON vst.fk_GYVUNASkodas=gyv.kodas
				ORDER BY gyv.kodas ASC";

			var dt = Sql.Query(query);

			foreach( DataRow item in dt )
			{
				vaistai.Add(new Vaistai
				{
					ID = Convert.ToInt32(item["id"]),
					Pavadinimas = Convert.ToString(item["pavadinimas"]),
                    IsrasymoData = Convert.ToDateTime(item["israsymo_data"]),
                    Kaina = Convert.ToDouble(item["kaina"]),
                    FkDarbuotojas = Convert.ToString(item["asmens_kodas"]),
                    FkDarbuotojasToString = Convert.ToString(item["dvardas"]) + " " + Convert.ToString(item["pavarde"]),
                    FkGyvunas = Convert.ToString(item["kodas"]),
                    FkGyvunasToString = Convert.ToString(item["vardas"])
				});
			}

			return vaistai;
		}

        /*
		public static List<Vaistai> ListForMiestas(string miestas)
		{
			var result = new List<Vaistai>();

			var query = $@"SELECT * FROM `{Config.TblPrefix}vaistai` WHERE fk_MIESTASpavadinimas=?miestas ORDER BY fk_MIESTASpavadinimas ASC";

			var dt =
				Sql.Query(query, args => {
					args.Add("?miestas", MySqlDbType.VarChar).Value = miestas;
				});

			foreach( DataRow item in dt )
			{
				result.Add(new Vaistai
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

		public static List<Vaistai> ListForLytis(int lytis)
		{
			var result = new List<Vaistai>();

			var query = $@"SELECT * FROM `{Config.TblPrefix}vaistai` WHERE lytis=?lytis ORDER BY lytis ASC";

			var dt =
				Sql.Query(query, args => {
					args.Add("?lytis", MySqlDbType.Int32).Value = lytis;
				});

			foreach( DataRow item in dt )
			{
				result.Add(new Vaistai
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

		public static VaistaiEditVM Find(int id)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}medikacijos` vaistai
            LEFT JOIN `{Config.TblPrefix}gyvunai` gyv ON vaistai.fk_GYVUNASkodas=gyv.kodas
            WHERE id=?id";

			var dt =
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});

			if( dt.Count > 0 )
			{
				var aevm = new VaistaiEditVM();

				foreach( DataRow item in dt )
				{
					aevm.Vaistai.ID = Convert.ToInt32(item["id"]);
					aevm.Vaistai.Pavadinimas = Convert.ToString(item["pavadinimas"]);
                    aevm.Vaistai.NustatymoData = Convert.ToDateTime(item["israsymo_data"]);
                    aevm.Vaistai.Kaina = Convert.ToDouble(item["kaina"]);
                    aevm.Vaistai.FkDarbuotojas = Convert.ToString(item["fk_DARBUOTOJASasmens_kodas"]);
                    aevm.Vaistai.FkGyvunas = Convert.ToString(item["kodas"]);
				}

				return aevm;
			}

			return null;
		}

		public static Vaistai FindForDeletion(int id)
		{
			var a = new Vaistai();

			var query =
				$@"SELECT
					vst.id,
					vst.pavadinimas,
                    vst.israsymo_data,
                    vst.kaina,
                    dar.asmens_kodas,
                    dar.vardas AS dvardas,
                    dar.pavarde,
                    gyv.kodas,
                    gyv.vardas
				FROM
					`{Config.TblPrefix}medikacijos` vst
					LEFT JOIN `{Config.TblPrefix}darbuotojai` dar ON vst.fk_DARBUOTOJASasmens_kodas=dar.asmens_kodas
                    LEFT JOIN `{Config.TblPrefix}gyvunai` gyv ON vst.fk_GYVUNASkodas=gyv.kodas
				WHERE vst.id=?id";

			var dt =
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});

			foreach( DataRow item in dt )
			{
				a.ID = Convert.ToInt32(item["id"]);
                a.Pavadinimas = Convert.ToString(item["pavadinimas"]);
                a.IsrasymoData = Convert.ToDateTime(item["israsymo_data"]);
                a.Kaina = Convert.ToDouble(item["kaina"]);
                a.FkDarbuotojas = Convert.ToString(item["asmens_kodas"]);
                a.FkDarbuotojasToString = Convert.ToString(item["dvardas"]) + " " + Convert.ToString(item["pavarde"]);
                a.FkGyvunas = Convert.ToString(item["kodas"]);
                a.FkGyvunasToString = Convert.ToString(item["vardas"]);
			}

			return a;
		}

		public static void Insert(VaistaiEditVM VaistaiEVM)
		{
			var query =
				$@"INSERT INTO `{Config.TblPrefix}medikacijos`
				(
					pavadinimas,
                    israsymo_data,
                    kaina,
                    fk_DARBUOTOJASasmens_kodas,
                    fk_GYVUNASkodas
				)
				VALUES(
					?pavadinimas,
                    ?israsymo_data,
                    ?kaina,
                    ?darb,
                    ?gyv
				)";

			Sql.Insert(query, args => {
				args.Add("?pavadinimas", MySqlDbType.VarChar).Value = VaistaiEVM.Vaistai.Pavadinimas;
				args.Add("?israsymo_data", MySqlDbType.Date).Value = VaistaiEVM.Vaistai.NustatymoData;
                args.Add("?kaina", MySqlDbType.Float).Value = VaistaiEVM.Vaistai.Kaina;
				args.Add("?darb", MySqlDbType.VarChar).Value = VaistaiEVM.Vaistai.FkDarbuotojas;
				args.Add("?gyv", MySqlDbType.VarChar).Value = VaistaiEVM.Vaistai.FkGyvunas;
			});
		}

		public static void Insert(VaistaiEditVM.VaistaiM vaistaiM)
		{
			var query =
				$@"INSERT INTO `{Config.TblPrefix}medikacijos`
				(
					pavadinimas,
                    israsymo_data,
                    kaina,
                    fk_DARBUOTOJASasmens_kodas,
                    fk_GYVUNASkodas
				)
				VALUES(
					?pavadinimas,
                    ?israsymo_data,
                    ?kaina,
                    ?darb,
                    ?gyv
				)";

			Sql.Insert(query, args => {
				args.Add("?pavadinimas", MySqlDbType.VarChar).Value = vaistaiM.Pavadinimas;
				args.Add("?israsymo_data", MySqlDbType.Date).Value = vaistaiM.NustatymoData;
                args.Add("?kaina", MySqlDbType.Float).Value = vaistaiM.Kaina;
				args.Add("?darb", MySqlDbType.VarChar).Value = vaistaiM.FkDarbuotojas;
				args.Add("?gyv", MySqlDbType.VarChar).Value = vaistaiM.FkGyvunas;
			});
		}

		public static void Update(VaistaiEditVM VaistaiEVM)
		{
			var query =
				$@"UPDATE `{Config.TblPrefix}medikacijos`
				SET
					pavadinimas=?pavadinimas,
                    israsymo_data=?data,
                    kaina=?kaina,
                    fk_DARBUOTOJASasmens_kodas=?darb,
                    fk_GYVUNASkodas=?gyv
				WHERE
					id=?id";

			Sql.Update(query, args => {
				args.Add("?pavadinimas", MySqlDbType.VarChar).Value = VaistaiEVM.Vaistai.Pavadinimas;
				args.Add("?data", MySqlDbType.Date).Value = VaistaiEVM.Vaistai.NustatymoData;
                args.Add("?kaina", MySqlDbType.Float).Value = VaistaiEVM.Vaistai.Kaina;
				args.Add("?darb", MySqlDbType.VarChar).Value = VaistaiEVM.Vaistai.FkDarbuotojas;
				args.Add("?gyv", MySqlDbType.VarChar).Value = VaistaiEVM.Vaistai.FkGyvunas;
				args.Add("?id", MySqlDbType.Int32).Value = VaistaiEVM.Vaistai.ID;
			});
		}

		public static void Delete(int id)
		{
			var query = $@"DELETE FROM `{Config.TblPrefix}medikacijos` WHERE id=?id";
			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.Int32).Value = id;
			});
		}

		public static void DeleteForGyvunas(string id)
		{
			var query =
				$@"DELETE FROM `{Config.TblPrefix}medikacijos`
				WHERE medikacijos.fk_GYVUNASkodas=?id";

			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.VarChar).Value = id;
			});
		}
	}
}