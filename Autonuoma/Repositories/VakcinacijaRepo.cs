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
	/// Database operations related to 'Vakcinacija' entity.
	/// </summary>
	public class VakcinacijaRepo
	{
		public static List<Vakcinacija> List()
		{
			var vakcinacijos = new List<Vakcinacija>();

			var query =
				$@"SELECT
					vkc.id,
					vkc.pavadinimas,
                    vkc.data,
					vkc.kaina,
                    dar.asmens_kodas,
                    dar.vardas AS dvardas,
                    dar.pavarde,
                    gyv.kodas,
                    gyv.vardas
				FROM
					`{Config.TblPrefix}vakcinacijos` vkc
					LEFT JOIN `{Config.TblPrefix}darbuotojai` dar ON vkc.fk_DARBUOTOJASasmens_kodas=dar.asmens_kodas
                    LEFT JOIN `{Config.TblPrefix}gyvunai` gyv ON vkc.fk_GYVUNASkodas=gyv.kodas
				ORDER BY gyv.kodas ASC";

			var dt = Sql.Query(query);

			foreach( DataRow item in dt )
			{
				vakcinacijos.Add(new Vakcinacija
				{
					ID = Convert.ToInt32(item["id"]),
					Pavadinimas = Convert.ToString(item["pavadinimas"]),
                    NustatymoData = Convert.ToDateTime(item["data"]),
					Kaina = Convert.ToDouble(item["kaina"]),
                    FkDarbuotojas = Convert.ToString(item["asmens_kodas"]),
                    FkDarbuotojasToString = Convert.ToString(item["dvardas"]) + " " + Convert.ToString(item["pavarde"]),
                    FkGyvunas = Convert.ToString(item["kodas"]),
                    FkGyvunasToString = Convert.ToString(item["vardas"])
				});
			}

			return vakcinacijos;
		}

        /*
		public static List<Vakcinacija> ListForMiestas(string miestas)
		{
			var result = new List<Vakcinacija>();

			var query = $@"SELECT * FROM `{Config.TblPrefix}vakcinacijos` WHERE fk_MIESTASpavadinimas=?miestas ORDER BY fk_MIESTASpavadinimas ASC";

			var dt =
				Sql.Query(query, args => {
					args.Add("?miestas", MySqlDbType.VarChar).Value = miestas;
				});

			foreach( DataRow item in dt )
			{
				result.Add(new Vakcinacija
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

		public static List<Vakcinacija> ListForLytis(int lytis)
		{
			var result = new List<Vakcinacija>();

			var query = $@"SELECT * FROM `{Config.TblPrefix}vakcinacijos` WHERE lytis=?lytis ORDER BY lytis ASC";

			var dt =
				Sql.Query(query, args => {
					args.Add("?lytis", MySqlDbType.Int32).Value = lytis;
				});

			foreach( DataRow item in dt )
			{
				result.Add(new Vakcinacija
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

		public static VakcinacijaEditVM Find(int id)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}vakcinacijos`
            LEFT JOIN `{Config.TblPrefix}gyvunai` gyv ON vakcinacijos.fk_GYVUNASkodas=gyv.kodas
            WHERE id=?id";

			var dt =
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});

			if( dt.Count > 0 )
			{
				var vevm = new VakcinacijaEditVM();

				foreach( DataRow item in dt )
				{
					vevm.Vakcinacija.ID = Convert.ToInt32(item["id"]);
					vevm.Vakcinacija.Pavadinimas = Convert.ToString(item["pavadinimas"]);
                    vevm.Vakcinacija.NustatymoData = Convert.ToDateTime(item["data"]);
					vevm.Vakcinacija.Kaina = Convert.ToDouble(item["kaina"]);
                    vevm.Vakcinacija.FkDarbuotojas = Convert.ToString(item["fk_DARBUOTOJASasmens_kodas"]);
                    vevm.Vakcinacija.FkGyvunas = Convert.ToString(item["kodas"]);
				}

				return vevm;
			}

			return null;
		}

		public static Vakcinacija FindForDeletion(int id)
		{
			var a = new Vakcinacija();

			var query =
				$@"SELECT
					vkc.id,
					vkc.pavadinimas,
                    vkc.data,
					vkc.kaina,
                    dar.asmens_kodas,
                    dar.vardas AS dvardas,
                    dar.pavarde,
                    gyv.kodas,
                    gyv.vardas
				FROM
					`{Config.TblPrefix}vakcinacijos` vkc
					LEFT JOIN `{Config.TblPrefix}darbuotojai` dar ON vkc.fk_DARBUOTOJASasmens_kodas=dar.asmens_kodas
                    LEFT JOIN `{Config.TblPrefix}gyvunai` gyv ON vkc.fk_GYVUNASkodas=gyv.kodas
				WHERE vkc.id=?id";

			var dt =
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});

			foreach( DataRow item in dt )
			{
				a.ID = Convert.ToInt32(item["id"]);
                a.Pavadinimas = Convert.ToString(item["pavadinimas"]);
                a.NustatymoData = Convert.ToDateTime(item["data"]);
				a.Kaina = Convert.ToDouble(item["kaina"]);
                a.FkDarbuotojas = Convert.ToString(item["asmens_kodas"]);
                a.FkDarbuotojasToString = Convert.ToString(item["dvardas"]) + " " + Convert.ToString(item["pavarde"]);
                a.FkGyvunas = Convert.ToString(item["kodas"]);
                a.FkGyvunasToString = Convert.ToString(item["vardas"]);
			}

			return a;
		}

		public static void Insert(VakcinacijaEditVM VakcinacijaEVM)
		{
			var query =
				$@"INSERT INTO `{Config.TblPrefix}vakcinacijos`
				(
					pavadinimas,
                    data,
					kaina,
                    fk_DARBUOTOJASasmens_kodas,
                    fk_GYVUNASkodas
				)
				VALUES(
					?pavadinimas,
                    ?data,
					?kaina,
                    ?darb,
                    ?gyv
				)";

			Sql.Insert(query, args => {
				args.Add("?pavadinimas", MySqlDbType.VarChar).Value = VakcinacijaEVM.Vakcinacija.Pavadinimas;
				args.Add("?data", MySqlDbType.Date).Value = VakcinacijaEVM.Vakcinacija.NustatymoData;
				args.Add("?kaina", MySqlDbType.Double).Value = VakcinacijaEVM.Vakcinacija.Kaina;
				args.Add("?darb", MySqlDbType.VarChar).Value = VakcinacijaEVM.Vakcinacija.FkDarbuotojas;
				args.Add("?gyv", MySqlDbType.VarChar).Value = VakcinacijaEVM.Vakcinacija.FkGyvunas;
			});
		}

		public static void Insert(VakcinacijaEditVM.VakcinacijaM vakcinacijaM)
		{
			var query =
				$@"INSERT INTO `{Config.TblPrefix}vakcinacijos`
				(
					pavadinimas,
                    data,
					kaina,
                    fk_DARBUOTOJASasmens_kodas,
                    fk_GYVUNASkodas
				)
				VALUES(
					?pavadinimas,
                    ?data,
					?kaina,
                    ?darb,
                    ?gyv
				)";

			Sql.Insert(query, args => {
				args.Add("?pavadinimas", MySqlDbType.VarChar).Value = vakcinacijaM.Pavadinimas;
				args.Add("?data", MySqlDbType.Date).Value = vakcinacijaM.NustatymoData;
				args.Add("?kaina", MySqlDbType.Double).Value = vakcinacijaM.Kaina;
				args.Add("?darb", MySqlDbType.VarChar).Value = vakcinacijaM.FkDarbuotojas;
				args.Add("?gyv", MySqlDbType.VarChar).Value = vakcinacijaM.FkGyvunas;
			});
		}

		public static void Update(VakcinacijaEditVM VakcinacijaEVM)
		{
			var query =
				$@"UPDATE `{Config.TblPrefix}vakcinacijos`
				SET
					pavadinimas=?pavadinimas,
                    data=?data,
					kaina=?kaina,
                    fk_DARBUOTOJASasmens_kodas=?darb,
                    fk_GYVUNASkodas=?gyv
				WHERE
					id=?id";

			Sql.Update(query, args => {
				args.Add("?pavadinimas", MySqlDbType.VarChar).Value = VakcinacijaEVM.Vakcinacija.Pavadinimas;
				args.Add("?data", MySqlDbType.Date).Value = VakcinacijaEVM.Vakcinacija.NustatymoData;
				args.Add("?kaina", MySqlDbType.Double).Value = VakcinacijaEVM.Vakcinacija.Kaina;
				args.Add("?darb", MySqlDbType.VarChar).Value = VakcinacijaEVM.Vakcinacija.FkDarbuotojas;
				args.Add("?gyv", MySqlDbType.VarChar).Value = VakcinacijaEVM.Vakcinacija.FkGyvunas;
				args.Add("?id", MySqlDbType.Int32).Value = VakcinacijaEVM.Vakcinacija.ID;
			});
		}

		public static void Delete(int id)
		{
			var query = $@"DELETE FROM `{Config.TblPrefix}vakcinacijos` WHERE id=?id";
			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.VarChar).Value = id;
			});
		}

		public static void DeleteForGyvunas(string id)
		{
			var query =
				$@"DELETE FROM `{Config.TblPrefix}vakcinacijos`
				WHERE vakcinacijos.fk_GYVUNASkodas=?id";

			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.VarChar).Value = id;
			});
		}
	}
}