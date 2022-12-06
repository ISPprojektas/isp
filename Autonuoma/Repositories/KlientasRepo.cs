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
	/// Database operations related to 'Klientas' entity.
	/// </summary>
	public class KlientasRepo
	{
		public static List<KlientasListVM> List()
		{
			var klientai = new List<KlientasListVM>();

			var query =
				$@"SELECT
					klnt.asmens_kodas,
					klnt.vardas,
                    klnt.pavarde,
                    klnt.telefonas,
                    klnt.e_pastas,
					klnt.adresas,
					klnt.gimimo_data,
					lyt.name AS lytis,
					mst.pavadinimas AS miestas
				FROM
					`{Config.TblPrefix}klientai` klnt
					LEFT JOIN `{Config.TblPrefix}miestai` mst ON klnt.fk_MIESTASpavadinimas=mst.pavadinimas
					LEFT JOIN `{Config.TblPrefix}lytys` lyt ON klnt.lytis=lyt.id_lytys
				ORDER BY klnt.asmens_kodas ASC";

			var dt = Sql.Query(query);

			foreach( DataRow item in dt )
			{
				klientai.Add(new KlientasListVM
				{
					AsmensKodas = Convert.ToString(item["asmens_kodas"]),
					Vardas = Convert.ToString(item["vardas"]),
					Pavarde = Convert.ToString(item["pavarde"]),
					GimimoData = Convert.ToDateTime(item["gimimo_data"]),
					Telefonas = Convert.ToString(item["telefonas"]),
					EPastas = Convert.ToString(item["e_pastas"]),
					Adresas = Convert.ToString(item["adresas"]),
					Lytis = Convert.ToString(item["lytis"]),
					FkMiestas = Convert.ToString(item["miestas"])
				});
			}

			return klientai;
		}

		public static List<Klientas> ListForMiestas(string miestas)
		{
			var result = new List<Klientas>();

			var query = $@"SELECT * FROM `{Config.TblPrefix}klientai` WHERE fk_MIESTASpavadinimas=?miestas ORDER BY fk_MIESTASpavadinimas ASC";

			var dt =
				Sql.Query(query, args => {
					args.Add("?miestas", MySqlDbType.VarChar).Value = miestas;
				});

			foreach( DataRow item in dt )
			{
				result.Add(new Klientas
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

		public static List<Klientas> ListForLytis(int lytis)
		{
			var result = new List<Klientas>();

			var query = $@"SELECT * FROM `{Config.TblPrefix}klientai` WHERE lytis=?lytis ORDER BY lytis ASC";

			var dt =
				Sql.Query(query, args => {
					args.Add("?lytis", MySqlDbType.Int32).Value = lytis;
				});

			foreach( DataRow item in dt )
			{
				result.Add(new Klientas
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

		public static KlientasEditVM Find(string asmkodas)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}klientai` WHERE asmens_kodas=?asmkodas";

			var dt =
				Sql.Query(query, args => {
					args.Add("?asmkodas", MySqlDbType.VarChar).Value = asmkodas;
				});

			if( dt.Count > 0 )
			{
				var kevm = new KlientasEditVM();

				foreach( DataRow item in dt )
				{
					kevm.Klientas.AsmensKodas = Convert.ToString(item["asmens_kodas"]);
					kevm.Klientas.Vardas = Convert.ToString(item["vardas"]);
					kevm.Klientas.Pavarde = Convert.ToString(item["pavarde"]);
					kevm.Klientas.GimimoData = Convert.ToDateTime(item["gimimo_data"]);
					kevm.Klientas.Telefonas = Convert.ToString(item["telefonas"]);
					kevm.Klientas.EPastas = Convert.ToString(item["e_pastas"]);
					kevm.Klientas.Adresas = Convert.ToString(item["adresas"]);
					kevm.Klientas.Lytis = Convert.ToInt32(item["lytis"]);
					kevm.Klientas.FkMiestas = Convert.ToString(item["fk_MIESTASpavadinimas"]);
				}

				return kevm;
			}

			return null;
		}

		public static KlientasListVM FindForDeletion(string id)
		{
			var k = new KlientasListVM();

			var query =
				$@"SELECT
					klnt.asmens_kodas,
					klnt.vardas,
                    klnt.pavarde,
                    klnt.telefonas,
                    klnt.e_pastas,
					klnt.adresas,
					klnt.gimimo_data,
					lyt.name AS lytis,
					mst.pavadinimas AS miestas
				FROM
					`{Config.TblPrefix}klientai` klnt
					LEFT JOIN `{Config.TblPrefix}miestai` mst ON klnt.fk_MIESTASpavadinimas=mst.pavadinimas
					LEFT JOIN `{Config.TblPrefix}lytys` lyt ON klnt.lytis=lyt.id_lytys
				WHERE
					klnt.asmens_kodas=?kodas";

			var dt =
				Sql.Query(query, args => {
					args.Add("?kodas", MySqlDbType.VarChar).Value = id;
				});

			foreach( DataRow item in dt )
			{
				k.AsmensKodas = Convert.ToString(item["asmens_kodas"]);
				k.Vardas = Convert.ToString(item["vardas"]);
				k.Pavarde = Convert.ToString(item["pavarde"]);
				k.GimimoData = Convert.ToDateTime(item["gimimo_data"]);
				k.Telefonas = Convert.ToString(item["telefonas"]);
				k.EPastas = Convert.ToString(item["e_pastas"]);
				k.Adresas = Convert.ToString(item["adresas"]);
				k.Lytis = Convert.ToString(item["lytis"]);
				k.FkMiestas = Convert.ToString(item["miestas"]);
			}

			return k;
		}

		public static void Insert(KlientasEditVM klientasEVM)
		{
			var query =
				$@"INSERT INTO `{Config.TblPrefix}klientai`
				(
					asmens_kodas,
					vardas,
					pavarde,
					gimimo_data,
					telefonas,
					e_pastas,
					adresas,
					lytis,
					fk_MIESTASpavadinimas
				)
				VALUES(
					?asmkod,
					?vardas,
					?pavarde,
					?gimdata,
					?tel,
					?email,
					?adresas,
					?lytis,
					?miestas
				)";

			Sql.Insert(query, args => {
				args.Add("?asmkod", MySqlDbType.VarChar).Value = klientasEVM.Klientas.AsmensKodas;
				args.Add("?vardas", MySqlDbType.VarChar).Value = klientasEVM.Klientas.Vardas;
				args.Add("?pavarde", MySqlDbType.VarChar).Value = klientasEVM.Klientas.Pavarde;
				args.Add("?gimdata", MySqlDbType.Date).Value = klientasEVM.Klientas.GimimoData;
				args.Add("?tel", MySqlDbType.VarChar).Value = klientasEVM.Klientas.Telefonas;
				args.Add("?email", MySqlDbType.VarChar).Value = klientasEVM.Klientas.EPastas;
				args.Add("?adresas", MySqlDbType.VarChar).Value = klientasEVM.Klientas.Adresas;
				args.Add("?lytis", MySqlDbType.Int32).Value = klientasEVM.Klientas.Lytis;
				args.Add("?miestas", MySqlDbType.VarChar).Value = klientasEVM.Klientas.FkMiestas;
			});
		}

		public static void Update(KlientasEditVM klientasEVM)
		{
			var query =
				$@"UPDATE `{Config.TblPrefix}klientai`
				SET
					vardas=?vardas,
					pavarde=?pavarde,
					gimimo_data=?gimdata,
					telefonas=?tel,
					e_pastas=?email,
					adresas=?adresas,
					lytis=?lytis,
					fk_MIESTASpavadinimas=?miestas
				WHERE
					asmens_kodas=?asmkod";

			Sql.Update(query, args => {
				args.Add("?asmkod", MySqlDbType.VarChar).Value = klientasEVM.Klientas.AsmensKodas;
				args.Add("?vardas", MySqlDbType.VarChar).Value = klientasEVM.Klientas.Vardas;
				args.Add("?pavarde", MySqlDbType.VarChar).Value = klientasEVM.Klientas.Pavarde;
				args.Add("?gimdata", MySqlDbType.Date).Value = klientasEVM.Klientas.GimimoData;
				args.Add("?tel", MySqlDbType.VarChar).Value = klientasEVM.Klientas.Telefonas;
				args.Add("?email", MySqlDbType.VarChar).Value = klientasEVM.Klientas.EPastas;
				args.Add("?adresas", MySqlDbType.VarChar).Value = klientasEVM.Klientas.Adresas;
				args.Add("?lytis", MySqlDbType.Int32).Value = klientasEVM.Klientas.Lytis;
				args.Add("?miestas", MySqlDbType.VarChar).Value = klientasEVM.Klientas.FkMiestas;
			});
		}

		public static void Delete(string id)
		{
			var query = $@"DELETE FROM `{Config.TblPrefix}klientai` WHERE asmens_kodas=?id";
			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.VarChar).Value = id;
			});
		}

		public static bool Exists(string id)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}klientai` WHERE asmens_kodas=?id";
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