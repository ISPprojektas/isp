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
	/// Database operations related to 'Gyvunas' entity.
	/// </summary>
	public class GyvunasRepo
	{
		public static List<GyvunasListVM> List()
		{
			var gyvunai = new List<GyvunasListVM>();

			var query =
				$@"SELECT
					gyv.kodas,
                    gyv.vardas,
                    gyv.gimimo_data,
                    gyv.svoris,
                    gyv.kaina,
                    lyt.name AS lytis,
                    rus.name AS rusis,
                    dyd.name AS dydis,
                    par.pavadinimas AS parduotuve
				FROM
					`{Config.TblPrefix}gyvunai` gyv
					LEFT JOIN `{Config.TblPrefix}lytys` lyt ON gyv.lytis=lyt.id_lytys
                    LEFT JOIN `{Config.TblPrefix}gyvunu_rusys` rus ON gyv.rusis=rus.id_gyvunu_rusys
                    LEFT JOIN `{Config.TblPrefix}gyvunu_dydziai` dyd ON gyv.dydis=dyd.id_gyvunu_dydziai
                    LEFT JOIN `{Config.TblPrefix}parduotuves` par ON gyv.fk_PARDUOTUVEkodas=par.kodas
				ORDER BY gyv.kodas ASC";

			var dt = Sql.Query(query);

			foreach( DataRow item in dt )
			{
				gyvunai.Add(new GyvunasListVM
				{
					ID = Convert.ToString(item["kodas"]),
                    Vardas = Convert.ToString(item["vardas"]),
                    GimimoData = Convert.ToDateTime(item["gimimo_data"]),
                    Svoris = Convert.ToDouble(item["svoris"]),
                    Kaina = Convert.ToDouble(item["kaina"]),
                    FkLytis = Convert.ToString(item["lytis"]),
                    FkRusis = Convert.ToString(item["rusis"]),
                    FkDydis = Convert.ToString(item["dydis"]),
                    //FkSutartis = Convert.ToString(item["sutartis"]),
                    FkParduotuve = Convert.ToString(item["parduotuve"]),
				});
			}

			return gyvunai;
		}

        /*
		public static List<Gyvunas> ListForMiestas(string miestas)
		{
			var result = new List<Gyvunas>();

			var query = $@"SELECT * FROM `{Config.TblPrefix}gyvunai` WHERE fk_MIESTASpavadinimas=?miestas ORDER BY fk_MIESTASpavadinimas ASC";

			var dt =
				Sql.Query(query, args => {
					args.Add("?miestas", MySqlDbType.VarChar).Value = miestas;
				});

			foreach( DataRow item in dt )
			{
				result.Add(new Gyvunas
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

		public static List<Gyvunas> ListForLytis(int lytis)
		{
			var result = new List<Gyvunas>();

			var query = $@"SELECT * FROM `{Config.TblPrefix}gyvunai` WHERE lytis=?lytis ORDER BY lytis ASC";

			var dt =
				Sql.Query(query, args => {
					args.Add("?lytis", MySqlDbType.Int32).Value = lytis;
				});

			foreach( DataRow item in dt )
			{
				result.Add(new Gyvunas
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

		public static GyvunasEditVM Find(string id)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}gyvunai` WHERE kodas=?id";

			var dt =
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.VarChar).Value = id;
				});

			if( dt.Count > 0 )
			{
				var gevm = new GyvunasEditVM();

				foreach( DataRow item in dt )
				{
					gevm.Gyvunas.ID = Convert.ToString(item["kodas"]);
                    gevm.Gyvunas.Vardas = Convert.ToString(item["vardas"]);
                    gevm.Gyvunas.GimimoData = Convert.ToDateTime(item["gimimo_data"]);
                    gevm.Gyvunas.Svoris = Convert.ToDouble(item["svoris"]);
                    gevm.Gyvunas.Kaina = Convert.ToDouble(item["kaina"]);
                    gevm.Gyvunas.FkLytis = Convert.ToInt32(item["lytis"]);
                    gevm.Gyvunas.FkRusis = Convert.ToInt32(item["rusis"]);
                    gevm.Gyvunas.FkDydis = Convert.ToInt32(item["dydis"]);
                    //gevm.Gyvunas.FkSutartis = Convert.ToString(item["fk_PARDAVIMO_SUTARTISkodas"]);
                    gevm.Gyvunas.FkParduotuve = Convert.ToString(item["fk_PARDUOTUVEkodas"]);
				}

				return gevm;
			}

			return null;
		}

		public static GyvunasListVM FindForDeletion(string id)
		{
			var g = new GyvunasListVM();

			var query =
				$@"SELECT
					gyv.kodas,
                    gyv.vardas,
                    gyv.gimimo_data,
                    gyv.svoris,
                    gyv.kaina,
                    lyt.name AS lytis,
                    rus.name AS rusis,
                    dyd.name AS dydis,
                    par.pavadinimas AS parduotuve
				FROM
					`{Config.TblPrefix}gyvunai` gyv
					LEFT JOIN `{Config.TblPrefix}lytys` lyt ON gyv.lytis=lyt.id_lytys
                    LEFT JOIN `{Config.TblPrefix}gyvunu_rusys` rus ON gyv.rusis=rus.id_gyvunu_rusys
                    LEFT JOIN `{Config.TblPrefix}gyvunu_dydziai` dyd ON gyv.dydis=dyd.id_gyvunu_dydziai
                    LEFT JOIN `{Config.TblPrefix}parduotuves` par ON gyv.fk_PARDUOTUVEkodas=par.kodas
				WHERE
					gyv.kodas=?kodas";

			var dt =
				Sql.Query(query, args => {
					args.Add("?kodas", MySqlDbType.VarChar).Value = id;
				});

			foreach( DataRow item in dt )
			{
                g.ID = Convert.ToString(item["kodas"]);
                g.Vardas = Convert.ToString(item["vardas"]);
                g.GimimoData = Convert.ToDateTime(item["gimimo_data"]);
                g.Svoris = Convert.ToDouble(item["svoris"]);
                g.Kaina = Convert.ToDouble(item["kaina"]);
                g.FkLytis = Convert.ToString(item["lytis"]);
                g.FkRusis = Convert.ToString(item["rusis"]);
                g.FkDydis = Convert.ToString(item["dydis"]);
                //g.FkSutartis = Convert.ToString(item["sutartis"]);
                g.FkParduotuve = Convert.ToString(item["parduotuve"]);
			}

			return g;
		}

		public static void Insert(GyvunasEditVM GyvunasEVM)
		{
			var query =
				$@"INSERT INTO `{Config.TblPrefix}gyvunai`
				(
					`kodas`,
					`vardas`,
					`gimimo_data`,
                    `svoris`,
                    `kaina`,
					`lytis`,
                    `rusis`,
                    `dydis`,
                    `fk_PARDUOTUVEkodas`
				)
				VALUES(
					?kodas,
					?vardas,
					?gimdata,
					?svoris,
					?kaina,
					?lytis,
					?rusis,
                    ?dydis,
                    ?parduotuve
				)";

			Sql.Insert(query, args => {
				args.Add("?kodas", MySqlDbType.VarChar).Value = GyvunasEVM.Gyvunas.ID;
				args.Add("?vardas", MySqlDbType.VarChar).Value = GyvunasEVM.Gyvunas.Vardas;
				args.Add("?gimdata", MySqlDbType.Date).Value = GyvunasEVM.Gyvunas.GimimoData;
				args.Add("?svoris", MySqlDbType.Float).Value = GyvunasEVM.Gyvunas.Svoris;
				args.Add("?kaina", MySqlDbType.Int32).Value = GyvunasEVM.Gyvunas.Kaina;
				args.Add("?lytis", MySqlDbType.Int32).Value = GyvunasEVM.Gyvunas.FkLytis;
				args.Add("?rusis", MySqlDbType.Int32).Value = GyvunasEVM.Gyvunas.FkRusis;
                args.Add("?dydis", MySqlDbType.Int32).Value = GyvunasEVM.Gyvunas.FkDydis;
                //args.Add("?sutartis", MySqlDbType.VarChar).Value = GyvunasEVM.Gyvunas.FkSutartis;
                args.Add("?parduotuve", MySqlDbType.VarChar).Value = GyvunasEVM.Gyvunas.FkParduotuve;
			});
		}

		public static void Update(GyvunasEditVM GyvunasEVM)
		{
			var query =
				$@"UPDATE `{Config.TblPrefix}gyvunai`
				SET
					kodas=?kodas,
					vardas=?vardas,
					gimimo_data=?gimdata,
                    svoris=?svoris,
                    kaina=?kaina,
					lytis=?lytis,
                    rusis=?rusis,
                    dydis=?dydis,
                    fk_PARDUOTUVEkodas=?parduotuve
				WHERE
					kodas=?kodas";

			Sql.Update(query, args => {
				args.Add("?kodas", MySqlDbType.VarChar).Value = GyvunasEVM.Gyvunas.ID;
				args.Add("?vardas", MySqlDbType.VarChar).Value = GyvunasEVM.Gyvunas.Vardas;
				args.Add("?gimdata", MySqlDbType.Date).Value = GyvunasEVM.Gyvunas.GimimoData;
				args.Add("?svoris", MySqlDbType.Double).Value = GyvunasEVM.Gyvunas.Svoris;
				args.Add("?kaina", MySqlDbType.Double).Value = GyvunasEVM.Gyvunas.Kaina;
				args.Add("?lytis", MySqlDbType.Int32).Value = GyvunasEVM.Gyvunas.FkLytis;
				args.Add("?rusis", MySqlDbType.Int32).Value = GyvunasEVM.Gyvunas.FkRusis;
                args.Add("?dydis", MySqlDbType.Int32).Value = GyvunasEVM.Gyvunas.FkDydis;
                //args.Add("?sutartis", MySqlDbType.VarChar).Value = GyvunasEVM.Gyvunas.FkSutartis;
                args.Add("?parduotuve", MySqlDbType.VarChar).Value = GyvunasEVM.Gyvunas.FkParduotuve;
			});
		}

		public static void Delete(string id)
		{
			var query = $@"DELETE FROM `{Config.TblPrefix}gyvunai` WHERE kodas=?id";
			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.VarChar).Value = id;
			});
		}

		public static bool Exists(string id)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}gyvunai` WHERE kodas=?id";
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