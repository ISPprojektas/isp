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
	/// Database operations related to 'Darbuotojas' entity.
	/// </summary>
	public class DarbuotojasRepo
	{
		public static List<DarbuotojasListVM> List()
		{
			var darbuotojai = new List<DarbuotojasListVM>();

			var query =
				$@"SELECT
					darb.asmens_kodas,
					darb.vardas,
                    darb.pavarde,
                    darb.telefonas,
                    darb.e_pastas,
					darb.adresas,
					darb.gimimo_data,
					darb.pasamdymo_data,
					darb.pareigos,
					lyt.name AS lytis,
					mst.pavadinimas AS miestas,
					pard.kodas AS parduotuve,
					pard.pavadinimas AS pardpav
				FROM
					`{Config.TblPrefix}darbuotojai` darb
					LEFT JOIN `{Config.TblPrefix}miestai` mst ON darb.fk_MIESTASpavadinimas=mst.pavadinimas
					LEFT JOIN `{Config.TblPrefix}lytys` lyt ON darb.lytis=lyt.id_lytys
					LEFT JOIN `{Config.TblPrefix}parduotuves` pard ON darb.fk_PARDUOTUVEkodas=pard.kodas
				ORDER BY darb.asmens_kodas ASC";

			var dt = Sql.Query(query);

			foreach( DataRow item in dt )
			{
				darbuotojai.Add(new DarbuotojasListVM
				{
					AsmensKodas = Convert.ToString(item["asmens_kodas"]),
					Vardas = Convert.ToString(item["vardas"]),
					Pavarde = Convert.ToString(item["pavarde"]),
					GimimoData = Convert.ToDateTime(item["gimimo_data"]),
					PasamdymoData = Convert.ToDateTime(item["pasamdymo_data"]),
					Telefonas = Convert.ToString(item["telefonas"]),
					EPastas = Convert.ToString(item["e_pastas"]),
					Adresas = Convert.ToString(item["adresas"]),
					Pareigos = Convert.ToString(item["pareigos"]),
					Lytis = Convert.ToString(item["lytis"]),
					FkMiestas = Convert.ToString(item["miestas"]),
					FkParduotuve = Convert.ToString(item["pardpav"])
				});
			}

			return darbuotojai;
		}

		public static List<Darbuotojas> ListForMiestas(string miestas)
		{
			var result = new List<Darbuotojas>();

			var query = $@"SELECT * FROM `{Config.TblPrefix}darbuotojai` WHERE fk_MIESTASpavadinimas=?miestas ORDER BY fk_MIESTASpavadinimas ASC";

			var dt =
				Sql.Query(query, args => {
					args.Add("?miestas", MySqlDbType.VarChar).Value = miestas;
				});

			foreach( DataRow item in dt )
			{
				result.Add(new Darbuotojas
				{
					AsmensKodas = Convert.ToString(item["asmens_kodas"]),
					Vardas = Convert.ToString(item["vardas"]),
					Pavarde = Convert.ToString(item["pavarde"]),
					GimimoData = Convert.ToDateTime(item["gimimo_data"]),
					PasamdymoData = Convert.ToDateTime(item["pasamdymo_data"]),
					Telefonas = Convert.ToString(item["telefonas"]),
					EPastas = Convert.ToString(item["e_pastas"]),
					Adresas = Convert.ToString(item["adresas"]),
					Pareigos = Convert.ToString(item["pareigos"]),
					Lytis = Convert.ToInt32(item["lytis"]),
					FkMiestas = Convert.ToString(item["miestas"]),
					FkParduotuve = Convert.ToString(item["parduotuve"])
				});
			}

			return result;
		}

		public static List<Darbuotojas> ListForLytis(int lytis)
		{
			var result = new List<Darbuotojas>();

			var query = $@"SELECT * FROM `{Config.TblPrefix}darbuotojai` WHERE lytis=?lytis ORDER BY lytis ASC";

			var dt =
				Sql.Query(query, args => {
					args.Add("?lytis", MySqlDbType.Int32).Value = lytis;
				});

			foreach( DataRow item in dt )
			{
				result.Add(new Darbuotojas
				{
					AsmensKodas = Convert.ToString(item["asmens_kodas"]),
					Vardas = Convert.ToString(item["vardas"]),
					Pavarde = Convert.ToString(item["pavarde"]),
					GimimoData = Convert.ToDateTime(item["gimimo_data"]),
					PasamdymoData = Convert.ToDateTime(item["pasamdymo_data"]),
					Telefonas = Convert.ToString(item["telefonas"]),
					EPastas = Convert.ToString(item["e_pastas"]),
					Adresas = Convert.ToString(item["adresas"]),
					Pareigos = Convert.ToString(item["pareigos"]),
					Lytis = Convert.ToInt32(item["lytis"]),
					FkMiestas = Convert.ToString(item["miestas"]),
					FkParduotuve = Convert.ToString(item["parduotuve"])
				});
			}

			return result;
		}

		public static List<Darbuotojas> ListForParduotuve(int parduotuve)
		{
			var result = new List<Darbuotojas>();

			var query = $@"SELECT * FROM `{Config.TblPrefix}darbuotojai` WHERE fk_PARDUOTUVEkodas=?parduotuve ORDER BY fk_PARDUOTUVEkodas ASC";

			var dt =
				Sql.Query(query, args => {
					args.Add("?lytis", MySqlDbType.Int32).Value = parduotuve;
				});

			foreach( DataRow item in dt )
			{
				result.Add(new Darbuotojas
				{
					AsmensKodas = Convert.ToString(item["asmens_kodas"]),
					Vardas = Convert.ToString(item["vardas"]),
					Pavarde = Convert.ToString(item["pavarde"]),
					GimimoData = Convert.ToDateTime(item["gimimo_data"]),
					PasamdymoData = Convert.ToDateTime(item["pasamdymo_data"]),
					Telefonas = Convert.ToString(item["telefonas"]),
					EPastas = Convert.ToString(item["e_pastas"]),
					Adresas = Convert.ToString(item["adresas"]),
					Pareigos = Convert.ToString(item["pareigos"]),
					Lytis = Convert.ToInt32(item["lytis"]),
					FkMiestas = Convert.ToString(item["miestas"]),
					FkParduotuve = Convert.ToString(item["parduotuve"])
				});
			}

			return result;
		}

		public static DarbuotojasEditVM Find(string asmkodas)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}darbuotojai` WHERE asmens_kodas=?asmkodas";

			var dt =
				Sql.Query(query, args => {
					args.Add("?asmkodas", MySqlDbType.VarChar).Value = asmkodas;
				});

			if( dt.Count > 0 )
			{
				var devm = new DarbuotojasEditVM();

				foreach( DataRow item in dt )
				{
					devm.Darbuotojas.AsmensKodas = Convert.ToString(item["asmens_kodas"]);
					devm.Darbuotojas.Vardas = Convert.ToString(item["vardas"]);
					devm.Darbuotojas.Pavarde = Convert.ToString(item["pavarde"]);
					devm.Darbuotojas.GimimoData = Convert.ToDateTime(item["gimimo_data"]);
					devm.Darbuotojas.PasamdymoData = Convert.ToDateTime(item["pasamdymo_data"]);
					devm.Darbuotojas.Telefonas = Convert.ToString(item["telefonas"]);
					devm.Darbuotojas.EPastas = Convert.ToString(item["e_pastas"]);
					devm.Darbuotojas.Adresas = Convert.ToString(item["adresas"]);
					devm.Darbuotojas.Pareigos = Convert.ToString(item["pareigos"]);
					devm.Darbuotojas.Lytis = Convert.ToInt32(item["lytis"]);
					devm.Darbuotojas.FkMiestas = Convert.ToString(item["fk_MIESTASpavadinimas"]);
					devm.Darbuotojas.FkParduotuve = Convert.ToString(item["fk_PARDUOTUVEkodas"]);
				}

				return devm;
			}

			return null;
		}

		public static DarbuotojasListVM FindForDeletion(string id)
		{
			var d = new DarbuotojasListVM();

			var query =
				$@"SELECT
					darb.asmens_kodas,
					darb.vardas,
                    darb.pavarde,
                    darb.telefonas,
                    darb.e_pastas,
					darb.adresas,
					darb.gimimo_data,
					darb.pasamdymo_data,
					darb.pareigos,
					lyt.name AS lytis,
					mst.pavadinimas AS miestas,
					pard.kodas AS parduotuve,
					pard.pavadinimas AS pardpav
				FROM
					`{Config.TblPrefix}darbuotojai` darb
					LEFT JOIN `{Config.TblPrefix}miestai` mst ON darb.fk_MIESTASpavadinimas=mst.pavadinimas
					LEFT JOIN `{Config.TblPrefix}lytys` lyt ON darb.lytis=lyt.id_lytys
					LEFT JOIN `{Config.TblPrefix}parduotuves` pard ON darb.fk_PARDUOTUVEkodas=pard.kodas
				WHERE darb.asmens_kodas=?kodas";

			var dt =
				Sql.Query(query, args => {
					args.Add("?kodas", MySqlDbType.VarChar).Value = id;
				});

			foreach( DataRow item in dt )
			{
				d.AsmensKodas = Convert.ToString(item["asmens_kodas"]);
				d.Vardas = Convert.ToString(item["vardas"]);
				d.Pavarde = Convert.ToString(item["pavarde"]);
				d.GimimoData = Convert.ToDateTime(item["gimimo_data"]);
				d.Telefonas = Convert.ToString(item["telefonas"]);
				d.EPastas = Convert.ToString(item["e_pastas"]);
				d.Adresas = Convert.ToString(item["adresas"]);
				d.Lytis = Convert.ToString(item["lytis"]);
				d.FkMiestas = Convert.ToString(item["miestas"]);
				d.FkParduotuve = Convert.ToString(item["pardpav"]);
			}

			return d;
		}

		public static void Insert(DarbuotojasEditVM darbuotojasEVM)
		{
			var query =
				$@"INSERT INTO `{Config.TblPrefix}darbuotojai`
				(
					asmens_kodas,
					vardas,
					pavarde,
					gimimo_data,
					pasamdymo_data,
					telefonas,
					e_pastas,
					pareigos,
					adresas,
					lytis,
					fk_MIESTASpavadinimas,
					fk_PARDUOTUVEkodas
				)
				VALUES(
					?asmkod,
					?vardas,
					?pavarde,
					?gimdata,
					?samddata,
					?tel,
					?email,
					?pareigos,
					?adresas,
					?lytis,
					?miestas,
					?pard
				)";

			Sql.Insert(query, args => {
				args.Add("?asmkod", MySqlDbType.VarChar).Value = darbuotojasEVM.Darbuotojas.AsmensKodas;
				args.Add("?vardas", MySqlDbType.VarChar).Value = darbuotojasEVM.Darbuotojas.Vardas;
				args.Add("?pavarde", MySqlDbType.VarChar).Value = darbuotojasEVM.Darbuotojas.Pavarde;
				args.Add("?gimdata", MySqlDbType.Date).Value = darbuotojasEVM.Darbuotojas.GimimoData;
				args.Add("?samddata", MySqlDbType.Date).Value = darbuotojasEVM.Darbuotojas.PasamdymoData;
				args.Add("?tel", MySqlDbType.VarChar).Value = darbuotojasEVM.Darbuotojas.Telefonas;
				args.Add("?email", MySqlDbType.VarChar).Value = darbuotojasEVM.Darbuotojas.EPastas;
				args.Add("?adresas", MySqlDbType.VarChar).Value = darbuotojasEVM.Darbuotojas.Adresas;
				args.Add("?pareigos", MySqlDbType.VarChar).Value = darbuotojasEVM.Darbuotojas.Pareigos;
				args.Add("?lytis", MySqlDbType.Int32).Value = darbuotojasEVM.Darbuotojas.Lytis;
				args.Add("?miestas", MySqlDbType.VarChar).Value = darbuotojasEVM.Darbuotojas.FkMiestas;
				args.Add("?pard", MySqlDbType.VarChar).Value = darbuotojasEVM.Darbuotojas.FkParduotuve;
			});
		}

		public static void Update(DarbuotojasEditVM darbuotojasEVM)
		{
			var query =
				$@"UPDATE `{Config.TblPrefix}darbuotojai`
				SET
					vardas=?vardas,
					pavarde=?pavarde,
					gimimo_data=?gimdata,
					pasamdymo_data=?samddata,
					telefonas=?tel,
					e_pastas=?email,
					adresas=?adresas,
					pareigos=?pareigos,
					lytis=?lytis,
					fk_MIESTASpavadinimas=?miestas,
					fk_PARDUOTUVEkodas=?pard
				WHERE
					asmens_kodas=?asmkod";

			Sql.Update(query, args => {
				args.Add("?asmkod", MySqlDbType.VarChar).Value = darbuotojasEVM.Darbuotojas.AsmensKodas;
				args.Add("?vardas", MySqlDbType.VarChar).Value = darbuotojasEVM.Darbuotojas.Vardas;
				args.Add("?pavarde", MySqlDbType.VarChar).Value = darbuotojasEVM.Darbuotojas.Pavarde;
				args.Add("?gimdata", MySqlDbType.Date).Value = darbuotojasEVM.Darbuotojas.GimimoData;
				args.Add("?samddata", MySqlDbType.Date).Value = darbuotojasEVM.Darbuotojas.PasamdymoData;
				args.Add("?tel", MySqlDbType.VarChar).Value = darbuotojasEVM.Darbuotojas.Telefonas;
				args.Add("?email", MySqlDbType.VarChar).Value = darbuotojasEVM.Darbuotojas.EPastas;
				args.Add("?adresas", MySqlDbType.VarChar).Value = darbuotojasEVM.Darbuotojas.Adresas;
				args.Add("?pareigos", MySqlDbType.VarChar).Value = darbuotojasEVM.Darbuotojas.Pareigos;
				args.Add("?lytis", MySqlDbType.Int32).Value = darbuotojasEVM.Darbuotojas.Lytis;
				args.Add("?miestas", MySqlDbType.VarChar).Value = darbuotojasEVM.Darbuotojas.FkMiestas;
				args.Add("?pard", MySqlDbType.VarChar).Value = darbuotojasEVM.Darbuotojas.FkParduotuve;
			});
		}

		public static void Delete(string id)
		{
			var query = $@"DELETE FROM `{Config.TblPrefix}darbuotojai` WHERE asmens_kodas=?id";
			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.VarChar).Value = id;
			});
		}

		public static bool Exists(string id)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}darbuotojai` WHERE asmens_kodas=?id";
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