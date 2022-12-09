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
	/// Database operations related to 'Prekes' entity.
	/// </summary>
	public class PrekesRepo
	{
		public static List<PrekesListVM> List()
		{
			var prekes = new List<PrekesListVM>();

			var query =
				$@"SELECT 
            prekės.id,
            prekės.Pavadinimas,
            Kaina,
            prekės.Aprašymas,
            Patinka_paspaudimai,
            prekės.NuotraukaLink,
            Spalva,
            Medžiaga,
            Matmenys,
            Svoris,
            Garantijos_trukmė,
            rūbų_dydžiai.name as 'dydis',
            kategorijos.Pavadinimas as 'kategorija',
            gamybos_vietos.Šalis as 'Gamybos šalis'
            FROM `prekės`
            LEFT JOIN kategorijos ON kategorijos.Sutrumpintas_pav = prekės.fk_Kategorija
            LEFT JOIN rūbų_dydžiai ON rūbų_dydžiai.id = prekės.dydis
            LEFT JOIN gamybos_vietos ON gamybos_vietos.id = prekės.fk_GamybosVieta;";

			var dt = Sql.Query(query);

			foreach( DataRow item in dt )
			{
                string p_spalva = "Spalva: " + Convert.ToString(item["Spalva"]);
                string p_medžiaga = "Medžiaga: " + Convert.ToString(item["Medžiaga"]);
                string p_matmenys = "Matmenys: " + Convert.ToString(item["Matmenys"]);
                string p_svoris = "Svoris: " + Convert.ToString(item["Svoris"]);
                string p_garantijos_trukmė = "Garantijos trukmė: " + Convert.ToString(item["Garantijos_trukmė"]);
                string p_dydis = "Dydis: " + Convert.ToString(item["dydis"]);
				prekes.Add(new PrekesListVM
				{
					ID = Convert.ToInt32(item["id"]),
					Pavadinimas = Convert.ToString(item["Pavadinimas"]),
					Kaina = Convert.ToDouble(item["Kaina"]),
					Aprasymas = Convert.ToString(item["Aprašymas"]),
					Patinka_paspaudimai = Convert.ToInt32(item["Patinka_paspaudimai"]),
					Nuotrauka = Convert.ToString(item["NuotraukaLink"]),
					Spalva = p_spalva,
					Medžiaga = p_medžiaga,
					Matmenys = p_matmenys,
					Svoris = p_svoris,
					Garantijos_trukmė = p_garantijos_trukmė,
					dydis = p_dydis,
					Kategorija = Convert.ToString(item["kategorija"]),
					Gamybos_vieta = Convert.ToString(item["Gamybos šalis"]),
				});
			}

			return prekes;
		}

		public static PrekesEditVM Find(string id)
		{
			var query = $@"SELECT 
            prekės.id,
            prekės.Pavadinimas,
            Kaina,
            prekės.Aprašymas,
            Patinka_paspaudimai,
            prekės.NuotraukaLink,
            Spalva,
            Medžiaga,
            Matmenys,
            Svoris,
            Garantijos_trukmė,
            rūbų_dydžiai.id as 'dydis',
            kategorijos.Sutrumpintas_pav as 'kategorija',
            gamybos_vietos.id as 'Gamybos šalis'
            FROM `prekės`
            LEFT JOIN kategorijos ON kategorijos.Sutrumpintas_pav = prekės.fk_Kategorija
            LEFT JOIN rūbų_dydžiai ON rūbų_dydžiai.id = prekės.dydis
            LEFT JOIN gamybos_vietos ON gamybos_vietos.id = prekės.fk_GamybosVieta WHERE prekės.id=?id";

			var dt =
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = Convert.ToInt32(id);
				});
			if( dt.Count > 0 )
			{
				var gevm = new PrekesEditVM();

				foreach ( DataRow item in dt )
				{
					string p_spalva = Convert.ToString(item["Spalva"]);
					string p_medžiaga = Convert.ToString(item["Medžiaga"]);
					string p_matmenys = Convert.ToString(item["Matmenys"]);
					string p_svoris = Convert.ToString(item["Svoris"]);
					string p_garantijos_trukmė = Convert.ToString(item["Garantijos_trukmė"]);
					int? p_dydis = !Convert.IsDBNull(item["dydis"]) ? Convert.ToInt32(item["dydis"]) : null;					
					gevm.Prekes.ID = Convert.ToInt32(item["id"]);
					gevm.Prekes.Pavadinimas = Convert.ToString(item["Pavadinimas"]);
					gevm.Prekes.Kaina = Convert.ToDouble(item["Kaina"]);
					gevm.Prekes.Aprasymas = Convert.ToString(item["Aprašymas"]);
					gevm.Prekes.Patinka_paspaudimai = Convert.ToInt32(item["Patinka_paspaudimai"]);
					gevm.Prekes.Nuotrauka = Convert.ToString(item["NuotraukaLink"]);
					gevm.Prekes.Spalva = p_spalva;
					gevm.Prekes.Medžiaga = p_medžiaga;
					gevm.Prekes.Matmenys = p_matmenys;
					gevm.Prekes.Svoris = p_svoris;
					gevm.Prekes.Garantijos_trukmė = p_garantijos_trukmė;
					gevm.Prekes.dydis = p_dydis;
					gevm.Prekes.Kategorija = Convert.ToString(item["kategorija"]);
					gevm.Prekes.Gamybos_vieta = Convert.ToInt32(item["Gamybos šalis"]);
				}
				return gevm;
			}

			return null;
		}

		public static PrekesListVM FindForDeletion(string id)
		{
			var g = new PrekesListVM();

			var query = $@"SELECT 
            prekės.id,
            prekės.Pavadinimas,
            Kaina,
            prekės.Aprašymas,
            Patinka_paspaudimai,
            prekės.NuotraukaLink,
            Spalva,
            Medžiaga,
            Matmenys,
            Svoris,
            Garantijos_trukmė,
            rūbų_dydžiai.id as 'dydis',
            kategorijos.Sutrumpintas_pav as 'kategorija',
            gamybos_vietos.id as 'Gamybos šalis'
            FROM `prekės`
            LEFT JOIN kategorijos ON kategorijos.Sutrumpintas_pav = prekės.fk_Kategorija
            LEFT JOIN rūbų_dydžiai ON rūbų_dydžiai.id = prekės.dydis
            LEFT JOIN gamybos_vietos ON gamybos_vietos.id = prekės.fk_GamybosVieta WHERE prekės.id=?id";

			var dt =
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.VarChar).Value = id;
				});

			foreach( DataRow item in dt )
			{
                string p_spalva = "Spalva: " + Convert.ToString(item["Spalva"]);
				string p_medžiaga = "Medžiaga: " + Convert.ToString(item["Medžiaga"]);
				string p_matmenys = "Matmenys: " + Convert.ToString(item["Matmenys"]);
				string p_svoris = "Svoris: " + Convert.ToString(item["Svoris"]);
				string p_garantijos_trukmė = "Garantijos trukmė: " + Convert.ToString(item["Garantijos_trukmė"]);
				string p_dydis = "Dydis: " + Convert.ToString(item["dydis"]);					
				g.ID = Convert.ToInt32(item["id"]);
				g.Pavadinimas = Convert.ToString(item["Pavadinimas"]);
				g.Kaina = Convert.ToDouble(item["Kaina"]);
				g.Aprasymas = Convert.ToString(item["Aprašymas"]);
				g.Patinka_paspaudimai = Convert.ToInt32(item["Patinka_paspaudimai"]);
				g.Nuotrauka = Convert.ToString(item["NuotraukaLink"]);
				g.Spalva = p_spalva;
				g.Medžiaga = p_medžiaga;
				g.Matmenys = p_matmenys;
				g.Svoris = p_svoris;
				g.Garantijos_trukmė = p_garantijos_trukmė;
				g.dydis = p_dydis;
				g.Kategorija = Convert.ToString(item["kategorija"]);
				g.Gamybos_vieta = Convert.ToString(item["Gamybos šalis"]);
			}

			return g;
		}

		public static void Insert(PrekesEditVM PrekesEVM)
		{
			var query =
				$@"INSERT INTO `prekės` 
				(`Pavadinimas`, `Kaina`, `Aprašymas`, 
				`Patinka_paspaudimai`, `NuotraukaLink`, 
				`Spalva`, `Medžiaga`, `Matmenys`, `Svoris`, 
				`Garantijos_trukmė`, `Dydis`, `fk_Kategorija`, 
				`fk_GamybosVieta`) 
				VALUES (
					?pav,
					?kaina,
					?aprasymas,
					?patinka,
					?nuotrauka,
					?spalva,
					?medziaga,
					?matmenys,
					?svoris,
					?garantija,
					?dydis,
					?kategorija,
					?gamyba
				)";

			var scc = Sql.Insert(query, args => {
				args.Add("?pav", MySqlDbType.VarChar).Value = PrekesEVM.Prekes.Pavadinimas;
				args.Add("?kaina", MySqlDbType.Decimal).Value = PrekesEVM.Prekes.Kaina;
				args.Add("?aprasymas", MySqlDbType.VarChar).Value = PrekesEVM.Prekes.Aprasymas;
				args.Add("?patinka", MySqlDbType.Int32).Value = PrekesEVM.Prekes.Patinka_paspaudimai;
				args.Add("?nuotrauka", MySqlDbType.VarChar).Value = PrekesEVM.Prekes.Nuotrauka;
				args.Add("?spalva", MySqlDbType.VarChar).Value = PrekesEVM.Prekes.Spalva;
                args.Add("?medziaga", MySqlDbType.VarChar).Value = PrekesEVM.Prekes.Medžiaga;
                args.Add("?matmenys", MySqlDbType.VarChar).Value = PrekesEVM.Prekes.Matmenys;
                args.Add("?svoris", MySqlDbType.VarChar).Value = PrekesEVM.Prekes.Svoris;
                args.Add("?garantija", MySqlDbType.Int32).Value = PrekesEVM.Prekes.Garantijos_trukmė;
                args.Add("?dydis", MySqlDbType.Int32).Value = PrekesEVM.Prekes.dydis;
                args.Add("?kategorija", MySqlDbType.VarChar).Value = PrekesEVM.Prekes.Kategorija;
                args.Add("?gamyba", MySqlDbType.Int32).Value = PrekesEVM.Prekes.Gamybos_vieta;
			});
			int aaa = 2;
		}

		public static void Update(PrekesEditVM PrekesEVM)
		{
			var query =
				$@"UPDATE prekės
				SET
					Pavadinimas=?pav,
					Kaina=?kaina,
					Aprašymas=?aprasymas,
					Patinka_paspaudimai=?patinka,
					NuotraukaLink=?nuotrauka,
					Spalva=?spalva,
					Medžiaga=?medziaga,
					Matmenys=?matmenys,
					Svoris=?svoris,
					Garantijos_trukmė=?garantija,
					Dydis=?dydis,
					fk_Kategorija=?kategorija,
					fk_GamybosVieta=?gamyba
				WHERE
					id=?id";

			var succ = Sql.Update(query, args => {
				args.Add("?id", MySqlDbType.Int32).Value = PrekesEVM.Prekes.ID;
				args.Add("?pav", MySqlDbType.VarChar).Value = PrekesEVM.Prekes.Pavadinimas;
				args.Add("?kaina", MySqlDbType.Decimal).Value = PrekesEVM.Prekes.Kaina;
				args.Add("?aprasymas", MySqlDbType.VarChar).Value = PrekesEVM.Prekes.Aprasymas;
				args.Add("?patinka", MySqlDbType.Int32).Value = PrekesEVM.Prekes.Patinka_paspaudimai;
				args.Add("?nuotrauka", MySqlDbType.VarChar).Value = PrekesEVM.Prekes.Nuotrauka;
				args.Add("?spalva", MySqlDbType.VarChar).Value = PrekesEVM.Prekes.Spalva;
                args.Add("?medziaga", MySqlDbType.VarChar).Value = PrekesEVM.Prekes.Medžiaga;
                args.Add("?matmenys", MySqlDbType.VarChar).Value = PrekesEVM.Prekes.Matmenys;
                args.Add("?svoris", MySqlDbType.VarChar).Value = PrekesEVM.Prekes.Svoris;
                args.Add("?garantija", MySqlDbType.Int32).Value = PrekesEVM.Prekes.Garantijos_trukmė;
                args.Add("?dydis", MySqlDbType.Int32).Value = PrekesEVM.Prekes.dydis;
                args.Add("?kategorija", MySqlDbType.VarChar).Value = PrekesEVM.Prekes.Kategorija;
                args.Add("?gamyba", MySqlDbType.Int32).Value = PrekesEVM.Prekes.Gamybos_vieta;
			});
            Console.WriteLine(PrekesEVM.Prekes.ID);
            Console.WriteLine("----");
			var aaaa = 2;
		}

		public static void Delete(string id)
		{
			var query = $@"DELETE FROM `{Config.TblPrefix}prekės` WHERE id=?id";
			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.VarChar).Value = id;
			});
		}

		public static bool Exists(int id)
		{
			var query = $@"SELECT * FROM `prekės` WHERE id=?id";
			var dt = 
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});

			foreach( DataRow item in dt )
			{
                return true;
			}

			return false;
		}
	}
}