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
	public class UzsakymaiRepo
	{
		public static List<Uzsakymai> List(int id = -1)
		{
			var uzsakymai = new List<Uzsakymai>();

			// Console.WriteLine($"id {id}");
			var query =
				$@"SELECT
                    uz.id,
                    uz.Užsakymo_laikas,
                    uz.Užsakymo_kaina,
                    uz.Apmokėjimo_laikas,
                    uz.Nuolaida,
                    uz.Būsena,
                    uz.fk_Naudotojas,
                    uz.fk_Parduotuvė,
                    nau.Vardas,
                    nau.Pavardė,
                    par.Pavadinimas,
                    ub.name,
                    ub.id AS ubid
				FROM
					`{Config.TblPrefix}užsakymai` uz
					LEFT JOIN `{Config.TblPrefix}naudotojai` nau ON uz.fk_Naudotojas=nau.id
                    LEFT JOIN `{Config.TblPrefix}parduotuvės` par ON uz.fk_Parduotuvė=par.id
                    LEFT JOIN `{Config.TblPrefix}užsakymo_būsenos` ub ON uz.Būsena=ub.id";
			DataRowCollection dt;
			if (id != -1) {
				query += @" WHERE uz.fk_Naudotojas = ?id";
				query +=@" ORDER BY uz.id ASC";
				dt = Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});
			} else {
				query +=@" ORDER BY uz.id ASC";
				dt = Sql.Query(query);
			}
            
			foreach( DataRow item in dt )
			{
                var up = new List<UzsakymoPrekes>();
				// Console.WriteLine($"q1 {item["id"]}");
                var query2 =
				$@"SELECT
                    up.Prekės_kiekis,
                    up.fk_Prekė,
                    up.fk_Užsakymas,
                    pr.pavadinimas,
                    pr.kaina
				FROM
					`{Config.TblPrefix}užsakymo_prekės` up
					LEFT JOIN `{Config.TblPrefix}prekės` pr ON fk_Prekė=pr.id
                    LEFT JOIN `{Config.TblPrefix}užsakymai` uz ON fk_Užsakymas=uz.id
				WHERE fk_Užsakymas={Convert.ToInt32(item["id"])}";

                var dt2 = Sql.Query(query2);

                
                foreach( DataRow item2 in dt2 )
                {
					// Console.WriteLine($"q2 {item2["fk_Užsakymas"]}");
                    up.Add(new UzsakymoPrekes
                    {
                        PrekesKiekis = Convert.ToInt32(item2["Prekės_kiekis"]),
                        fk_Preke = Convert.ToInt32(item2["fk_Prekė"]),
                        fk_PrekeToString = Convert.ToString(item2["pavadinimas"]),
                        fk_Uzsakymas = Convert.ToInt32(item2["fk_Užsakymas"]),
                        VienetoKaina = Convert.ToDouble(item2["kaina"]),
                        Kaina = Convert.ToDouble(item2["kaina"]) * Convert.ToInt32(item2["Prekės_kiekis"])
                    });
                }

				uzsakymai.Add(new Uzsakymai
				{
					pk_Id = Convert.ToInt32(item["id"]),
					UzsakymoLaikas = Convert.ToDateTime(item["Užsakymo_laikas"]),
                    UzsakymoKaina = Convert.ToDouble(item["Užsakymo_kaina"]),
                    ApmokejimoLaikas = item["Apmokėjimo_laikas"] != DBNull.Value ? Convert.ToDateTime(item["Apmokėjimo_laikas"]) : new DateTime(),
                    Nuolaida = item["Nuolaida"] != DBNull.Value ? Convert.ToDouble(item["Nuolaida"]) : 0,
                    BusenaToString = Convert.ToString(item["name"]),
                    Busena = Convert.ToInt32(item["ubid"]),
                    fk_NaudotojasToString = Convert.ToString(item["Vardas"]) + " " + Convert.ToString(item["Pavardė"]),
                    fk_ParduotuveToString = Convert.ToString(item["Pavadinimas"]),
                    fk_Naudotojas = Convert.ToInt32(item["fk_Naudotojas"]),
                    fk_Parduotuve = Convert.ToInt32(item["fk_Parduotuvė"]),
                    uzsakymoPrekes = up
				});
			}



			return uzsakymai;
		}



        /*
		public static List<uzsakymai> ListForMiestas(string miestas)
		{
			var result = new List<uzsakymai>();

			var query = $@"SELECT * FROM `{Config.TblPrefix}uzsakymai` WHERE fk_MIESTASpavadinimas=?miestas ORDER BY fk_MIESTASpavadinimas ASC";

			var dt =
				Sql.Query(query, args => {
					args.Add("?miestas", MySqlDbType.VarChar).Value = miestas;
				});

			foreach( DataRow item in dt )
			{
				result.Add(new uzsakymai
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

		public static List<uzsakymai> ListForLytis(int lytis)
		{
			var result = new List<uzsakymai>();

			var query = $@"SELECT * FROM `{Config.TblPrefix}uzsakymai` WHERE lytis=?lytis ORDER BY lytis ASC";

			var dt =
				Sql.Query(query, args => {
					args.Add("?lytis", MySqlDbType.Int32).Value = lytis;
				});

			foreach( DataRow item in dt )
			{
				result.Add(new uzsakymai
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

		public static Uzsakymai Find(int id)
		{
			var a = new Uzsakymai();

			var query =
				$@"SELECT
                    uz.id,
                    uz.Užsakymo_laikas,
                    uz.Užsakymo_kaina,
                    uz.Apmokėjimo_laikas,
                    uz.Nuolaida,
                    uz.Būsena,
                    uz.fk_Naudotojas,
                    uz.fk_Parduotuvė,
                    nau.Vardas,
                    nau.Pavardė,
                    par.Pavadinimas,
                    ub.name,
                    ub.id AS ubid
				FROM
					`{Config.TblPrefix}užsakymai` uz
					LEFT JOIN `{Config.TblPrefix}naudotojai` nau ON uz.fk_Naudotojas=nau.id
                    LEFT JOIN `{Config.TblPrefix}parduotuvės` par ON uz.fk_Parduotuvė=par.id
                    LEFT JOIN `{Config.TblPrefix}užsakymo_būsenos` ub ON uz.Būsena=ub.id
				WHERE uz.id=?id";

			var dt =
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});

			foreach( DataRow item in dt )
			{
				a.pk_Id = Convert.ToInt32(item["id"]);
                a.UzsakymoLaikas = Convert.ToDateTime(item["Užsakymo_laikas"]);
                a.UzsakymoKaina = Convert.ToDouble(item["Užsakymo_kaina"]);
                a.ApmokejimoLaikas = item["Apmokėjimo_laikas"] != DBNull.Value ? Convert.ToDateTime(item["Apmokėjimo_laikas"]) : new DateTime();
                a.Nuolaida = item["Nuolaida"] != DBNull.Value ? Convert.ToDouble(item["Nuolaida"]) : 0;
                a.BusenaToString = Convert.ToString(item["name"]);
                a.Busena = Convert.ToInt32(item["ubid"]);
                a.fk_NaudotojasToString = Convert.ToString(item["Vardas"]) + " " + Convert.ToString(item["Pavardė"]);
                a.fk_ParduotuveToString = Convert.ToString(item["Pavadinimas"]);
                a.fk_Naudotojas = Convert.ToInt32(item["fk_Naudotojas"]);
                a.fk_Parduotuve = Convert.ToInt32(item["fk_Parduotuvė"]);
			}

			return a;
		}

		/*public static void Insert(uzsakymaiEditVM uzsakymaiEVM)
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
				args.Add("?pavadinimas", MySqlDbType.VarChar).Value = uzsakymaiEVM.uzsakymai.Pavadinimas;
				args.Add("?israsymo_data", MySqlDbType.Date).Value = uzsakymaiEVM.uzsakymai.NustatymoData;
                args.Add("?kaina", MySqlDbType.Float).Value = uzsakymaiEVM.uzsakymai.Kaina;
				args.Add("?darb", MySqlDbType.VarChar).Value = uzsakymaiEVM.uzsakymai.FkDarbuotojas;
				args.Add("?gyv", MySqlDbType.VarChar).Value = uzsakymaiEVM.uzsakymai.FkGyvunas;
			});
		}

		public static void Insert(uzsakymaiEditVM.uzsakymaiM uzsakymaiM)
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
				args.Add("?pavadinimas", MySqlDbType.VarChar).Value = uzsakymaiM.Pavadinimas;
				args.Add("?israsymo_data", MySqlDbType.Date).Value = uzsakymaiM.NustatymoData;
                args.Add("?kaina", MySqlDbType.Float).Value = uzsakymaiM.Kaina;
				args.Add("?darb", MySqlDbType.VarChar).Value = uzsakymaiM.FkDarbuotojas;
				args.Add("?gyv", MySqlDbType.VarChar).Value = uzsakymaiM.FkGyvunas;
			});
		}*/

		public static void UpdatePurchase(UzsakymaiBuyVM uzsakymas)
		{
			var query =
				$@"UPDATE `užsakymai`
				SET
                    Apmokėjimo_laikas=?laikas,
                    Būsena=?busena,
					fk_Parduotuvė=?pard
				WHERE
					id=?id";

			Sql.Update(query, args => {
                args.Add("?laikas", MySqlDbType.DateTime).Value = DateTime.Now;
				args.Add("?busena", MySqlDbType.Int32).Value = 2;
				args.Add("?id", MySqlDbType.Int32).Value = uzsakymas.Uzsakymai.pk_Id;
				args.Add("?pard", MySqlDbType.Int32).Value = uzsakymas.Uzsakymai.fk_Parduotuve;
			});
		}

		public static void Update(Uzsakymai uzsakymas)
		{
			var query =
				$@"UPDATE `{Config.TblPrefix}užsakymai`
				SET
					Užsakymo_laikas=?ulaikas,
                    Užsakymo_kaina=?kaina,
                    Apmokėjimo_laikas=?alaikas,
                    Nuolaida=?nuolaida,
                    Būsena=?busena
				WHERE
					id=?id";

			Sql.Update(query, args => {
				args.Add("?ulaikas", MySqlDbType.DateTime).Value = uzsakymas.UzsakymoLaikas;
				args.Add("?kaina", MySqlDbType.Double).Value = uzsakymas.UzsakymoKaina;
                args.Add("?alaikas", MySqlDbType.DateTime).Value = uzsakymas.ApmokejimoLaikas;
				args.Add("?nuolaida", MySqlDbType.Double).Value = uzsakymas.Nuolaida;
				args.Add("?busena", MySqlDbType.Int32).Value = uzsakymas.Busena;
				args.Add("?id", MySqlDbType.Int32).Value = uzsakymas.pk_Id;
			});
		}

		public static void Insert(Uzsakymai uzsakymas, string curr1, string curr2, string curr3, int userid)
		{
			var query =
				$@"INSERT INTO `užsakymai` 
				(`Užsakymo_laikas`, `Užsakymo_kaina`, `Apmokėjimo_laikas`, 
				`Nuolaida`, `Būsena`, `fk_Naudotojas`, `fk_Parduotuvė`, `id`) 
				VALUES (
					NOW(), 
					'0', 
					NULL, 
					'0', 
					'1', 
					?naud, 
					?pard, 
					NULL)";

			var id = Sql.Insert(query, args => {
				//args.Add("?naud", MySqlDbType.Int32).Value = uzsakymas.fk_Naudotojas;
				args.Add("?naud", MySqlDbType.Int32).Value = userid;
				//args.Add("?pard", MySqlDbType.Int32).Value = uzsakymas.fk_Parduotuve;
				args.Add("?pard", MySqlDbType.Int32).Value = 1;
			});

			var arr1 = curr1.Split('\n').SkipLast(1).ToArray();
			var arr2 = curr2.Split('\n').SkipLast(1).ToArray();
			var arr3 = curr3.Split('\n').SkipLast(1).ToArray();

			for(int i = 0; i < arr1.Length; i++)
			{
				var q =
					$@"INSERT INTO `užsakymo_prekės` 
					(`Prekės_kiekis`, `fk_Prekė`, `fk_Užsakymas`) 
					VALUES (
						?prekes_kiekis,
						?preke,
						?uzsakymas)";

				Sql.Insert(q, args => {
					args.Add("?prekes_kiekis", MySqlDbType.Int32).Value = arr2[i];
					args.Add("?preke", MySqlDbType.Int32).Value = arr1[i];
					args.Add("?uzsakymas", MySqlDbType.Int32).Value = id;
				});
			}
			
		}

		public static void Delete(int id)
		{
			var query = $@"DELETE FROM `{Config.TblPrefix}užsakymai` WHERE id=?id";
			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.Int32).Value = id;
			});
		}
	}
}